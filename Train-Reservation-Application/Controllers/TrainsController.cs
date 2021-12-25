using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Train_Reservation_Application.Interfaces;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly ITrainsService _trainsService;

        public TrainsController(ITrainsService trainsService)
        {
            _trainsService = trainsService;
        }

        [HttpGet("filter-trains/{selectedDate}")]
        public async Task<IActionResult> FilterTrainsByDate(DateTime selectedDate)
        {
            return Ok(await _trainsService.FilterTrainsByDate(selectedDate));
        }

        [HttpGet("{idTrain}/{date}/filter-cars/{carType}")]
        public async Task<IActionResult> FilterCarsByType(int idTrain, DateTime date, CarType carType)
        {
            return Ok(await _trainsService.FilterCarsByType(idTrain, date, carType));
        }

        [HttpGet("{idTrain}/{date}/filter-cars-by-available-seats/{N}")]
        public async Task<IActionResult> SeatsList(int idTrain, DateTime date, int N)
        {
            return Ok(await _trainsService.SeatsList(idTrain, date, N));
        }
    }
}
