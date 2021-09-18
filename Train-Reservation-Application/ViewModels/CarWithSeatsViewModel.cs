using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.ViewModels
{
    public class CarWithSeatsViewModel
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public CarType Type { get; set; }
        public List<SeatViewModel> Seats { get; set; }
    }
}
