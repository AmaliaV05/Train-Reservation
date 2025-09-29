using HotChocolate.Types;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class SeatType : ObjectType<Seat>
    {
        protected override void Configure(IObjectTypeDescriptor<Seat> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.Number).Type<IntType>();
            descriptor.Field(f => f.Car).Type<CarType>();
            descriptor.Field(f => f.Reservations).Type<ListType<ReservationType>>();
            descriptor.Field(f => f.Calendars).Type<ListType<CalendarType>>();
        }
    }
}
