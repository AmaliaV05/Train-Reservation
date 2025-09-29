using System;
using System.Threading.Tasks;
using Train_Reservation_Application.GraphQL.Inputs;
using Train_Reservation_Application.GraphQL.Payloads;

namespace Train_Reservation_Application.GraphQL.Interfaces
{
    public interface IGraphQLReservationsService : IAsyncDisposable
    {
        public Task<AddReservationPayload> CreateReservationAsync(AddReservationInput input);
        public Task<UpdateReservationPayload> UpdateReservationAsync(UpdateReservationInput input);
    }
}
