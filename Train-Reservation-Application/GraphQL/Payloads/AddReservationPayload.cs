using System.Collections.Generic;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Payloads
{
    public sealed class AddReservationPayload
    {
        public AddReservationPayload(Customer response, IEnumerable<Seat> alternativeResponse, string message)
        {
            Response = response;
            AlternativeResponse = alternativeResponse;
            Message = message;
        }

        public Customer Response { get; }
        public IEnumerable<Seat> AlternativeResponse { get; }
        public string Message { get; }
    }
}
