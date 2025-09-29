using System.Collections.Generic;
using System;

namespace Train_Reservation_Application.Services
{
    public class UpdateReservationRequest
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
