using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainReservation.Application.DTOs;
using TrainReservation.Application.ViewModels.Trains;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.Interfaces
{
    public interface IRestTrainsService
    {
        Task<IEnumerable<TrainViewModel>> FilterTrainsByDate(TrainSearchRequest request);
        Task<TrainWithCarsViewModel> FilterCarsByType(int idTrain, DateTime date, CarType carType);
        Task<IEnumerable<int>> SeatsList(int idTrain, DateTime date, int N);
    }
}
