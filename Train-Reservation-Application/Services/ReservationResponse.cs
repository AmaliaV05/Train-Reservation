using System.Collections.Generic;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.Services
{
    public class ReservationResponse
    {
        public Customer Response { get; set; }
        public IEnumerable<Seat> AlternativeResponse { get; set; }
        public string Message { get; set; }
    }
}
