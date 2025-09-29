using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Inputs;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class UpdateReservationInputType : InputObjectType<UpdateReservationInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<UpdateReservationInput> descriptor)
        {
            descriptor.Field(f => f.IdReservation).Type<IntType>();
            descriptor.Field(f => f.Reservation).Type<UpdateReservationDetailsInputType>();
        }
    }
}
