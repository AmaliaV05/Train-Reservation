using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Inputs;

namespace TrainReservation.Application.GraphQL.Types
{
    public class UpdateReservationDetailsInputType : InputObjectType<UpdateReservationDetailsInput>
    {        
        protected override void Configure(
           IInputObjectTypeDescriptor<UpdateReservationDetailsInput> descriptor)
        {
            descriptor.Name("UpdateReservationDetailsInput");
            descriptor.Description("Input type for updating reservation details.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The ID of the reservation to update.");
            descriptor.Field(f => f.Code).Type<StringType>().Description("The reservation code used as a security measure to update a reservation.");
            descriptor.Field(f => f.ReservationDate).Type<DateTimeType>().Description("The new date for the reservation.");
            descriptor.Field(f => f.ReservedSeatsIds).Type<ListType<IntType>>().Description("The updated list of reserved seat IDs.");
        }
    }
}
