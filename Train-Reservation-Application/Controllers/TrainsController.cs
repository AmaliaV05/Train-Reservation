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
        public ActionResult<IEnumerable<TrainWithCarsViewModel>> FilterCarsByType(int idTrain, Models.Type carType)
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

        [HttpGet("{idTrain}/filter-cars-by-available-seats/{numberOfSeats}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TrainWithCarsViewModel>> FilterCarsByNumberOfNeighbouringSeatsAvailable(int idTrain, int numberOfSeats)
        {
            var query = _context.Trains
            .Where(train => train.Id == idTrain)
            .Include(train => train.Cars)
            .ThenInclude(car => car.Seats
                        .SkipWhile(s => s.Available == false)
                        .TakeWhile(s => s.Available == false)
                        .Count());
            
            if (query.Count() >= numberOfSeats)
            {
                return _context.Trains
                .Where(train => train.Id == idTrain)
                .Include(train => train.Cars)
                .ThenInclude(car => car.Seats)
                .AsSplitQuery()
                .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
                .ToList();
            }

            return Ok();
        }

        [HttpGet("check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<int>> SeatsList(int idTrain, int N)
        { 
            List<int> SeatsList = new();

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
                        if ((checkSeat.Number == 1 && checkSeat.Available == true) ||
                            (checkSeat.Number == checkCar.NumberOfSeats && checkSeat.Available == true) ||
                            (checkSeat.Available == false))
                        {
                            SeatsList.Add(checkSeat.Number);
                            SeatsList.Add(checkCar.CarNumber);
                        }
                    }
                }
            }

            List<int> AvailableSeatsList = new();

            for (int i = 0; i < SeatsList.Count-2; i+=2)
            {
                if (SeatsList[i + 2] - SeatsList[i] - 1 >= N && SeatsList[i+1] == SeatsList[i + 3])   //din acelasi vagon
                {
                    for (int t = SeatsList[i] + 1; t < SeatsList[i + 2]; t++)
                    {
                        AvailableSeatsList.Add(t);
                        AvailableSeatsList.Add(SeatsList[i + 1]);
                    }
                    /*AvailableSeatsList.Add(SeatsList[i]);
                    AvailableSeatsList.Add(SeatsList[i + 1]);
                    AvailableSeatsList.Add(SeatsList[i + 2]);
                    AvailableSeatsList.Add(SeatsList[i + 3]);*/
                    AvailableSeatsList.Add(1000);
                }
            }

            foreach (int a in AvailableSeatsList)
            {
                Console.WriteLine(a);
                Console.WriteLine(" ");
            }

            return AvailableSeatsList;
        }        
    }
}
