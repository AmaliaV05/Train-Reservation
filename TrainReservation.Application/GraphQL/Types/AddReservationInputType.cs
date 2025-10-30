using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Inputs;

namespace TrainReservation.Application.GraphQL.Types
{
    public class AddReservationInputType : InputObjectType<AddReservationInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<AddReservationInput> descriptor)
        {
            descriptor.Name("AddReservationInput");
            descriptor.Description("Input type for adding a new reservation.");

            descriptor.Field(f => f.SocialSecurityNumber).Type<StringType>().Description("The social security number of the customer.");
            descriptor.Field(f => f.Name).Type<StringType>().Description("The full name of the customer.");
            descriptor.Field(f => f.Email).Type<StringType>().Description("The email address of the customer.");
            descriptor.Field(f => f.ReservationDate).Type<DateTimeType>().Description("The date and time of the reservation.");
            descriptor.Field(f => f.ReservedSeatsIds).Type<ListType<IntType>>().Description("The IDs of the seats the customer wants to reserve.");
        }
    }
}
