using System;
using System.Collections.Generic;

namespace Train_Reservation_Application.ViewModels.Reservations
{
    public class ModifyReservationViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
