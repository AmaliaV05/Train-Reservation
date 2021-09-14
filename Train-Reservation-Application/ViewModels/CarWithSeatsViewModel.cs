using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.ViewModels
{
    public class CarWithSeatsViewModel
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public Models.Type Type { get; set; }
        public IEnumerable<SeatViewModel> Seats { get; set; }
    }
}
