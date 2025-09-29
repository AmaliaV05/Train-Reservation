using System.Threading.Tasks;
using Train_Reservation_Application.GraphQL.Inputs;
using Train_Reservation_Application.GraphQL.Interfaces;
using Train_Reservation_Application.GraphQL.Payloads;

namespace Train_Reservation_Application.GraphQL.Mutations
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
