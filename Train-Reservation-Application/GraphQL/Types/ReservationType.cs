using HotChocolate.Types;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class ReservationType : ObjectType<Reservation>
    {
        protected override void Configure(IObjectTypeDescriptor<Reservation> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.Code).Type<StringType>();
            descriptor.Field(f => f.ReservationDate).Type<DateTimeType>();
            descriptor.Field(f => f.Seats).Type<ListType<SeatType>>();
            descriptor.Field(f => f.Customer).Type<CustomerType>();
        }
    }
}
