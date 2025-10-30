using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Payloads;
using TrainReservation.Core.GraphQL.Types;

namespace TrainReservation.Application.GraphQL.Types
{
    public class UpdateReservationPayloadType : ObjectType<UpdateReservationPayload>
    {
        protected override void Configure(
            IObjectTypeDescriptor<UpdateReservationPayload> descriptor)
        {
            descriptor.Name("UpdateReservationPayload");
            descriptor.Description("Payload returned after attempting to update a reservation. It contains the result of the update, alternative suggestions if applicable, and a status message.");

            descriptor.Field(f => f.Response).Type<CustomerType>().Description("The updated customer data if the reservation update was successful.");
            descriptor.Field(f => f.AlternativeResponse).Type<ListType<SeatType>>().Description("Suggested alternative seats if the requested ones were unavailable.");
            descriptor.Field(f => f.Message).Type<StringType>().Description("A message describing the outcome of the reservation update.");
        }
    }
}
