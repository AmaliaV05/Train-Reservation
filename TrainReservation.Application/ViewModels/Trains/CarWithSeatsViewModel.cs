using System.Collections.Generic;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.ViewModels.Trains
{
    public class CarWithSeatsViewModel
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public CarType Type { get; set; }
        public IEnumerable<SeatViewModel> Seats { get; set; }
    }
}
