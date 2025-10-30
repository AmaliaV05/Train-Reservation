using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Application.DTOs;
using TrainReservation.Core.Models;
using TrainReservation.Infrastructure.Data;
using TrainReservation.Infrastructure.Exceptions;

namespace TrainReservation.Infrastructure.Services
{
    public class TrainsService
    {
        private readonly ApplicationDbContext _context;

        public TrainsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Train> GetTrainsByDate(TrainSearchRequest request)
        {
            return _context.Trains
                .Where(train => train.DayOfWeek == request.Date.DayOfWeek);
        }

        public IQueryable<Train> GetTrainsByDate(DayOfWeek selectedDay)
        {
            return _context.Trains
                .Where(train => train.DayOfWeek == selectedDay);
        }

        public IQueryable<Train> GetCarsByType(int idTrain, DateTime date, CarType carType)
        {
            IQueryable<Train> train = Enumerable.Empty<Train>().AsQueryable();

            if (carType == CarType.All)
            {
                train = _context.Trains
                    .OrderBy(train => train.Id)
                    .Where(train => train.Id == idTrain)
                    .Include(train => train.Cars)
                    .ThenInclude(car => car.Seats)
                    .ThenInclude(seat => seat.SeatCalendars.Where(sc => sc.Calendar.CalendarDate.Date == date.Date))
                    .AsSplitQuery();

                if (!train.Any())
                {                    
                    throw new IdNotFoundException(nameof(Train), idTrain);
                }
                
                return train;
            }

            train = _context.Trains
                 .OrderBy(train => train.Id)
                 .Where(train => train.Id == idTrain)
                 .Include(train => train.Cars.Where(car => car.Type == carType))
                 .ThenInclude(car => car.Seats)
                 .ThenInclude(seat => seat.SeatCalendars.Where(sc => sc.Calendar.CalendarDate.Date == date.Date))
                 .AsSplitQuery();

            if (!train.Any())
            {
                throw new IdNotFoundException(nameof(Train), idTrain);
            }

            return train;
        }

        public async Task<List<int>> GetSeatListAsync(int idTrain, DateTime date, int N)
        {
            Train train = await _context.Trains
                .OrderBy(train => train.Id)
                .Where(train => train.Id == idTrain)
                .Include(train => train.Cars)
                .ThenInclude(car => car.Seats)
                .ThenInclude(seat => seat.Calendars.Where(calendar => calendar.CalendarDate.Date == date.Date))
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (train is null)
            {
                throw new IdNotFoundException(nameof(Train), idTrain);
            }

            IReadOnlyList<int> occupiedSeatsList = GetOccupiedSeatList(train);
            List<int> availableSeatsList = GetAvailableSeatIdsList(occupiedSeatsList, N);
            return availableSeatsList;
        }

        private static IReadOnlyList<int> GetOccupiedSeatList(Train train)
        {
            List<int> occupiedSeatsList = new();

            foreach (Car checkCar in train.Cars)
            {
                foreach (Seat checkSeat in checkCar.Seats)
                {
                    if (checkSeat.Number == 1)
                    {
                        occupiedSeatsList.Add(0);
                        occupiedSeatsList.Add(checkCar.CarNumber);
                        occupiedSeatsList.Add(checkSeat.Id - 1);
                    }
                    foreach (SeatCalendar seatCalendar in checkSeat.SeatCalendars)
                    {
                        if (seatCalendar.SeatAvailability == true)
                        {
                            occupiedSeatsList.Add(checkSeat.Number);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                            occupiedSeatsList.Add(checkSeat.Id);
                        }
                    }
                    if (checkSeat.Number == checkCar.NumberOfSeats)
                    {
                        occupiedSeatsList.Add(checkCar.NumberOfSeats + 1);
                        occupiedSeatsList.Add(checkCar.CarNumber);
                        occupiedSeatsList.Add(checkSeat.Id + 1);
                    }
                }
            }

            return occupiedSeatsList;
        }

        private static List<int> GetAvailableSeatIdsList(IReadOnlyList<int> occupiedSeatsList, int N)
        {
            List<int> availableSeatsList = new();
            if (occupiedSeatsList.Count > 6)
            {
                for (int i = 0; i <= occupiedSeatsList.Count - 6; i += 3)
                {
                    if (occupiedSeatsList[i + 3] - occupiedSeatsList[i] - 1 >= N && occupiedSeatsList[i + 1] == occupiedSeatsList[i + 4])
                    {
                        for (int t = occupiedSeatsList[i + 2] + 1; t < occupiedSeatsList[i + 5]; t++)
                        {
                            availableSeatsList.Add(t);
                        }
                    }
                }
            }
            else
            {
                for (int t = occupiedSeatsList[2] + 1; t < occupiedSeatsList[5]; t++)
                {
                    availableSeatsList.Add(t);
                }
            }

            return availableSeatsList;
        }
    }
}
