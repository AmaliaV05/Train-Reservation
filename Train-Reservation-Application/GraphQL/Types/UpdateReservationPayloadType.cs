using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Payloads;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class UpdateReservationPayloadType : ObjectType<UpdateReservationPayload>
    {
        protected override void Configure(
            IObjectTypeDescriptor<UpdateReservationPayload> descriptor)
        {
            descriptor.Field(f => f.Response).Type<CustomerType>();
            descriptor.Field(f => f.AlternativeResponse).Type<ListType<SeatType>>();
            descriptor.Field(f => f.Message).Type<StringType>();
        }
    }
}
