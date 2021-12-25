using System.Collections.Generic;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.ViewModels.Trains
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
