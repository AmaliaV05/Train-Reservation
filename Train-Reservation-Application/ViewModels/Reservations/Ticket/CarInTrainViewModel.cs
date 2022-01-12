using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels.Trains;

namespace Train_Reservation_Application.ViewModels.Reservations.Ticket
{
    public class CarInTrainViewModel
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public CarType Type { get; set; }
        public TrainViewModel Train { get; set; }
    }
}
