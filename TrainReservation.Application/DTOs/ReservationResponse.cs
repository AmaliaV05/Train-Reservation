using System.Collections.Generic;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.DTOs
{
    public class ReservationResponse
    {
        public Customer Response { get; set; }
        public IEnumerable<Seat> AlternativeResponse { get; set; }
        public string Message { get; set; }
    }
}
