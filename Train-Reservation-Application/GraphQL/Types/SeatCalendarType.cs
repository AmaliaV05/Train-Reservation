using HotChocolate.Types;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class SeatCalendarType : ObjectType<SeatCalendar>
    {
        protected override void Configure(IObjectTypeDescriptor<SeatCalendar> descriptor)
        {
            descriptor.Field(f => f.Seat).Type<SeatType>();
            descriptor.Field(f => f.Calendar).Type<CalendarType>();
            descriptor.Field(f => f.SeatAvailability).Type<BooleanType>();
        }
    }
}
