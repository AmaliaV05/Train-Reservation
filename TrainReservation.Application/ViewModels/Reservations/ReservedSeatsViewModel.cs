using System;
using System.Collections.Generic;

namespace TrainReservation.Application.ViewModels.Reservations
{
    public class ReservedSeatsViewModel
    {
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
