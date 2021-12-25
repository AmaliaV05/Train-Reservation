using System;
using System.Collections.Generic;

namespace Train_Reservation_Application.ViewModels
{
    public class ReservedSeatsViewModel
    {
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
