using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Inputs;

namespace TrainReservation.Application.GraphQL.Types
{
    public class UpdateReservationInputType : InputObjectType<UpdateReservationInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<UpdateReservationInput> descriptor)
        {
            descriptor.Name("UpdateReservationInput");
            descriptor.Description("Input type for updating a reservation.");

            descriptor.Field(f => f.IdReservation).Type<IntType>().Description("The ID of the reservation to update.");
            descriptor.Field(f => f.Reservation).Type<UpdateReservationDetailsInputType>().Description("The updated reservation details.");
        }
    }
}
