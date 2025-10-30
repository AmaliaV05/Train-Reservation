using TrainReservation.Application.ViewModels.Trains;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.ViewModels.Reservations.Ticket
{
    public class CarInTrainViewModel
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public CarType Type { get; set; }
        public TrainViewModel Train { get; set; }
    }
}
