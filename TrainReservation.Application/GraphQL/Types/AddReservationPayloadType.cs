using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Payloads;
using TrainReservation.Core.GraphQL.Types;

namespace TrainReservation.Application.GraphQL.Types
{
    public class AddReservationPayloadType : ObjectType<AddReservationPayload>
    {
        protected override void Configure(
            IObjectTypeDescriptor<AddReservationPayload> descriptor)
        {
            descriptor.Name("AddReservationPayload");
            descriptor.Description("Payload returned after attempting to add a new reservation. Includes the customer information, any suggested alternative seats, and a result message.");
            
            descriptor.Field(f => f.Response).Type<CustomerType>().Description("The customer for whom the reservation was created.");
            descriptor.Field(f => f.AlternativeResponse).Type<ListType<SeatType>>().Description("Suggested alternative seats if the requested ones were not available.");
            descriptor.Field(f => f.Message).Type<StringType>().Description("A message indicating the result of the reservation attempt.");
        }
    }
}
