using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public DateTime CalendarDate { get; set; }
        public bool AvailableSeat { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
