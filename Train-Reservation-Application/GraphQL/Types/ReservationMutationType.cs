using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Inputs;
using Train_Reservation_Application.GraphQL.Interfaces;
using Train_Reservation_Application.GraphQL.Mutations;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class ReservationMutationType : ObjectType<ReservationMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<ReservationMutation> descriptor)
        {
            descriptor
                .Field("addReservation")
                .Type<AddReservationPayloadType>()                
                .Argument("input", a => a.Type<NonNullType<AddReservationInputType>>())
                .Resolve(async context =>
                {
                    var reservationsService = context.Service<IGraphQLReservationsService>();                    
                    var input = context.ArgumentValue<AddReservationInput>("input");
                    return await reservationsService.CreateReservationAsync(input);
                })
                .Description("A customer adds a new reservation.");

            descriptor
                .Field("updateReservation")
                .Type<UpdateReservationPayloadType>()
                .Argument("input", a => a.Type<NonNullType<UpdateReservationInputType>>())
                .Resolve(async context =>
                {
                    var reservationsService = context.Service<IGraphQLReservationsService>();
                    var input = context.ArgumentValue<UpdateReservationInput>("input");
                    return await reservationsService.UpdateReservationAsync(input);
                })
                .Description("A customer modifies a reservation.");
        }
    }
}
