using System.Threading.Tasks;
using TrainReservation.Application.GraphQL.Inputs;
using TrainReservation.Application.GraphQL.Interfaces;
using TrainReservation.Application.GraphQL.Payloads;

namespace TrainReservation.Infrastructure.GraphQL.Mutations
{
    public class ReservationMutation
    {
        public static async Task<AddReservationPayload> NewReservation(IGraphQLReservationsService reservationsService, AddReservationInput input)
        {
            return await reservationsService.CreateReservationAsync(input);
        }

        public static async Task<UpdateReservationPayload> UpdateReservation(IGraphQLReservationsService reservationsService, UpdateReservationInput input)
        {
            return await reservationsService.UpdateReservationAsync(input);
        }
    }
}
