using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_Reservation_Application.Data;
using Train_Reservation_Application.Interfaces;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels.Trains;

namespace Train_Reservation_Application.Services
{
    public class TrainsService : ITrainsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TrainsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainViewModel>> FilterTrainsByDate(DateTime selectedDate)
        {
            return await _context.Trains
                .Where(train => train.DayOfWeek == selectedDate.DayOfWeek)
                .Select(train => _mapper.Map<TrainViewModel>(train))
                .ToListAsync();
        }

        public async Task<TrainWithCarsViewModel> FilterCarsByType(int idTrain, DateTime selectedDate, CarType carType)
        {
            if (carType == 0)
            {
                var trainWithCarsViewModel = await _context.Trains
                 .OrderBy(train => train.Id)
                 .Where(train => train.Id == idTrain)                 
                 .Include(train => train.Cars)
                 .ThenInclude(car => car.Seats)
                 .ThenInclude(seat => seat.SeatCalendars.Where(sc => sc.Calendar.CalendarDate.Date == selectedDate.Date))
                 .AsSplitQuery()                 
                 .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
                 .FirstOrDefaultAsync();

                return trainWithCarsViewModel;
            }

            var trainWithCarsViewModelFiltered = await _context.Trains
                 .OrderBy(train => train.Id)
                 .Where(train => train.Id == idTrain)
                 .Include(train => train.Cars.Where(car => car.Type == carType))
                 .ThenInclude(car => car.Seats)
                 .ThenInclude(seat => seat.SeatCalendars.Where(sc => sc.Calendar.CalendarDate.Date == selectedDate.Date))
                 .AsSplitQuery()
                 .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
                 .FirstOrDefaultAsync();

            return trainWithCarsViewModelFiltered;
        }

        public async Task<IEnumerable<int>> SeatsList(int idTrain, DateTime date, int N)
        {
            var train = await _context.Trains
                .OrderBy(train => train.Id)
                .Where(train => train.Id == idTrain)
                .Include(train => train.Cars)
                .ThenInclude(car => car.Seats)
                .ThenInclude(seat => seat.Calendars.Where(calendar => calendar.CalendarDate.Date == date.Date))
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            var occupiedSeatsList = OccupiedSeatsList(train);
            var availableSeatsList = AvailableSeatIdsList(occupiedSeatsList, N);
            return availableSeatsList;
        }

        private static List<int> OccupiedSeatsList(Train train)
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

        private static List<int> AvailableSeatIdsList(List<int> occupiedSeatsList, int N)
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
                for (int t = occupiedSeatsList[2] + 1; t < occupiedSeatsList[5]; t++)
                {
                    availableSeatsList.Add(t);
                }
            return availableSeatsList;
        }
    }
}
