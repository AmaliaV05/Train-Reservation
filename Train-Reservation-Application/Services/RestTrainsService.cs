using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_Reservation_Application.Interfaces;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels.Trains;

namespace Train_Reservation_Application.Services
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

        public async Task<IEnumerable<TrainViewModel>> FilterTrainsByDate(DateTime selectedDate)
        {
            IQueryable<Train> train = _trainsService.GetTrainsByDate(selectedDate);
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
