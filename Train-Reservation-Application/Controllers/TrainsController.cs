using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Reservation_Application.Data;
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

        [HttpGet("filter-cars/{idTrain}/{carType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    }
}
