using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels.Trains;

namespace Train_Reservation_Application.Interfaces
{
    public interface IRestTrainsService
    {
        Task<IEnumerable<TrainViewModel>> FilterTrainsByDate(DateTime selectedDate);
        Task<TrainWithCarsViewModel> FilterCarsByType(int idTrain, DateTime date, CarType carType);
        Task<IEnumerable<int>> SeatsList(int idTrain, DateTime date, int N);
    }
}
