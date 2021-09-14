using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Reservation_Application.Data;
using Train_Reservation_Application.ViewModels;
using Z.EntityFramework.Plus;

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

        [HttpGet("filterTrains/{date}")]
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

        [HttpGet("filterCars/{carType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TrainWithCarsViewModel>> FilterCarsByType(int id, Models.Type carType)
        {
            var trainWithCarsViewModel = _context.Trains
                .Where(train => train.Id == id)
                .Include(train => train.Cars.Where(car => car.Type == carType))
                .ThenInclude(car => car.Seats)                
                .AsSplitQuery()
                .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
                .ToList();
            
            return trainWithCarsViewModel;
        }
    }
}
