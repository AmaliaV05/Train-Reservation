using System;
using System.Collections.Generic;

namespace Train_Reservation_Application.ViewModels.Reservations.Ticket
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ReservationDate { get; set; }
        public IEnumerable<SeatInCarViewModel> Seats { get; set; }
    }
}
