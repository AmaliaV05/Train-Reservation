using System;
using System.Collections.Generic;
using System.Threading;

namespace Train_Reservation_Application.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<Seat> Seats { get; set; }
        public Customer Customer { get; set; }
        public List<ReservationSeat> ReservationSeats { get; set; }
    }
}
