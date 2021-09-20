using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Reservation_Application.Data;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels;

namespace Train_Reservation_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TrainsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("filter-trains/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TrainViewModel>> FilterTrainsByDate(DateTime date)
        {
            var trainViewModelList = _context.Trains
                .Select(train => _mapper.Map<TrainViewModel>(train))
                .ToList();

            var trainListFiltered = trainViewModelList
                .Where(train => train.DayOfWeek == date.DayOfWeek)
                .ToList();

            return trainListFiltered;
        }

        [HttpGet("{idTrain}/filter-cars/{carType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TrainWithCarsViewModel>> FilterCarsByType(int idTrain, CarType carType)
        {
            if (carType == 0)
            {
                var trainWithCarsViewModel = _context.Trains
                .Where(train => train.Id == idTrain)
                .Include(train => train.Cars)
                .ThenInclude(car => car.Seats)
                .AsSplitQuery()
                .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
                .ToList();

                return trainWithCarsViewModel;
            } 
            
            var carListFiltered = _context.Trains
            .Where(train => train.Id == idTrain)
            .Include(train => train.Cars.Where(car => car.Type == carType))
            .ThenInclude(car => car.Seats)
            .AsSplitQuery()
            .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
            .ToList();

            return carListFiltered;
        }

        [HttpGet("{idTrain}/{date}/filter-cars/{carType}/COPY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TrainWithCarsViewModel>> FilterCarsByType2(int idTrain, DateTime date, CarType carType)
        {
            var trainWithCarsViewModel = _context.Trains
             .Where(train => train.Id == idTrain)
             .Include(train => train.Cars)
             .ThenInclude(car => car.Seats.Where(seat => seat.Reservation.ReservationDate.Date ==))
             .AsSplitQuery()
             .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
             .ToList();

            return trainWithCarsViewModel;
        }

        [HttpGet("{idTrain}/filter-cars-by-available-seats/{N}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<int>> SeatsList(int idTrain, int N)
        {
            List<int> occupiedSeatsList = new();

            var train = _context.Trains
                .Where(train => train.Id == idTrain)
                .Include(train => train.Cars)
                .ThenInclude(car => car.Seats)
                .AsSplitQuery()
                .ToList();

            foreach (Train checkTrain in train)
            {
                foreach (Car checkCar in checkTrain.Cars)
                {
                    foreach (Seat checkSeat in checkCar.Seats)
                    {
                        if(checkSeat.Number == 1)
                        {
                            occupiedSeatsList.Add(0);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                        }
                        if (checkSeat.Available == false)
                        {
                            occupiedSeatsList.Add(checkSeat.Number);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                        }
                        if (checkSeat.Number == checkCar.NumberOfSeats)
                        {
                            occupiedSeatsList.Add(checkCar.NumberOfSeats + 1);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                        }
                    }
                }
            }

            List<int> availableSeatsList = new();

            for (int i = 0; i < occupiedSeatsList.Count-2; i+=2)
            {
                if (occupiedSeatsList[i + 2] - occupiedSeatsList[i] - 1 >= N && occupiedSeatsList[i+1] == occupiedSeatsList[i + 3])   //din acelasi vagon
                {
                    for (int t = occupiedSeatsList[i] + 1; t < occupiedSeatsList[i + 2]; t++)
                    {
                        availableSeatsList.Add(t);
                        availableSeatsList.Add(occupiedSeatsList[i + 1]);
                    }
                }
            }

            return availableSeatsList;            
        }

        [HttpGet("{idTrain}/filter-cars-by-available-COPY/{N}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<int>> SeatsListID(int idTrain, int N)
        {
            List<int> occupiedSeatsList = new();

            var train = _context.Trains
                .Where(train => train.Id == idTrain)
                .Include(train => train.Cars)
                .ThenInclude(car => car.Seats)
                .AsSplitQuery()
                .ToList();

            foreach (Train checkTrain in train)
            {
                foreach (Car checkCar in checkTrain.Cars)
                {
                    foreach (Seat checkSeat in checkCar.Seats)
                    {
                        if (checkSeat.Number == 1)
                        {
                            occupiedSeatsList.Add(0);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                            occupiedSeatsList.Add(checkSeat.Id - 1);
                        }
                        if (checkSeat.Available == false)
                        {
                            occupiedSeatsList.Add(checkSeat.Number);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                            occupiedSeatsList.Add(checkSeat.Id);
                        }
                        if (checkSeat.Number == checkCar.NumberOfSeats)
                        {
                            occupiedSeatsList.Add(checkCar.NumberOfSeats + 1);
                            occupiedSeatsList.Add(checkCar.CarNumber);
                            occupiedSeatsList.Add(checkSeat.Id + 1);
                        }
                    }
                }
            }

            List<int> availableSeatsListIds = new();
            List<int> availableSeatsInCarsList = new();

            for (int i = 0; i <= occupiedSeatsList.Count - 6; i += 3)
            {
                if (occupiedSeatsList[i + 3] - occupiedSeatsList[i] - 1 >= N && occupiedSeatsList[i + 1] == occupiedSeatsList[i + 4])   //din acelasi vagon
                {
                    for (int t = occupiedSeatsList[i + 2] + 1; t < occupiedSeatsList[i + 5]; t++)
                    {
                        availableSeatsListIds.Add(t);
                    }
                    for (int t = occupiedSeatsList[i] + 1; t < occupiedSeatsList[i + 3]; t++)
                    {
                        availableSeatsInCarsList.Add(t);
                        availableSeatsInCarsList.Add(occupiedSeatsList[i] + 1);
                    }
                }
            }

            var result = string.Join(",", availableSeatsListIds.ToArray());

            Console.WriteLine(result);

            var result2 = string.Join(",", availableSeatsInCarsList.ToArray());

            Console.WriteLine(result2);

            return availableSeatsListIds;
        }
    }
}
