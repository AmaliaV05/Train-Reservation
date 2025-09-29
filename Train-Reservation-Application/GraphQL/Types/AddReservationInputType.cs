using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Inputs;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class AddReservationInputType : InputObjectType<AddReservationInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<AddReservationInput> descriptor)
        {
            descriptor.Field(f => f.SocialSecurityNumber).Type<StringType>();
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.Email).Type<StringType>();
            descriptor.Field(f => f.ReservationDate).Type<DateTimeType>();
            descriptor.Field(f => f.ReservedSeatsIds).Type<ListType<IntType>>();
        }
    }
}
