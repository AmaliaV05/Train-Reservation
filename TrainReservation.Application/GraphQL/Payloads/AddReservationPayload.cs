using System.Collections.Generic;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.GraphQL.Payloads
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
