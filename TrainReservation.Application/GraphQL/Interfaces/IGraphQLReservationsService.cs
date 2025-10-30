using System;
using System.Threading.Tasks;
using TrainReservation.Application.GraphQL.Inputs;
using TrainReservation.Application.GraphQL.Payloads;

namespace TrainReservation.Application.GraphQL.Interfaces
{
    public interface IGraphQLReservationsService : IAsyncDisposable
    {
        public Task<AddReservationPayload> CreateReservationAsync(AddReservationInput input);
        public Task<UpdateReservationPayload> UpdateReservationAsync(UpdateReservationInput input);
    }
}
