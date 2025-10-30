using System.Collections.Generic;
using System;

namespace TrainReservation.Application.DTOs
{
    public class UpdateReservationRequest
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
