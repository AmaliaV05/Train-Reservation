using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainReservation.Application.DTOs;
using TrainReservation.Application.Interfaces;
using TrainReservation.Core.Models;

namespace TrainReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly IRestTrainsService _trainsService;

        public TrainsController(IRestTrainsService trainsService)
        {
            _trainsService = trainsService;
        }

        [HttpGet("filter-trains")]
        public async Task<IActionResult> FilterTrainsByDate([FromQuery] TrainSearchRequest request)
        {
            return Ok(await _trainsService.FilterTrainsByDate(request));
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
