using System.Collections.Generic;
using System;

namespace TrainReservation.Application.DTOs
{
    public class ReservationRequest
    {
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
