using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Application.DTOs;
using TrainReservation.Application.Interfaces;
using TrainReservation.Application.ViewModels.Trains;
using TrainReservation.Core.Models;

namespace TrainReservation.Infrastructure.Services
{
    public class RestTrainsService : IRestTrainsService
    {
        private readonly TrainsService _trainsService;
        private readonly IMapper _mapper;

        public RestTrainsService(TrainsService trainsService, IMapper mapper)
        {
            _trainsService = trainsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainViewModel>> FilterTrainsByDate(TrainSearchRequest request)
        {
            IQueryable<Train> train = _trainsService.GetTrainsByDate(request);
            return await train
                .Select(train => _mapper.Map<TrainViewModel>(train))
                .ToListAsync();
        }

        public async Task<TrainWithCarsViewModel> FilterCarsByType(int idTrain, DateTime selectedDate, CarType carType)
        {
            IQueryable<Train> train = _trainsService.GetCarsByType(idTrain, selectedDate, carType);

            return await train
                .Select(train => _mapper.Map<TrainWithCarsViewModel>(train))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<int>> SeatsList(int idTrain, DateTime date, int N)
        {
            return await _trainsService.GetSeatListAsync(idTrain, date, N);
        }
    }
}
