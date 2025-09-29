using HotChocolate.Types;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class CalendarType : ObjectType<Calendar>
    {
        protected override void Configure(IObjectTypeDescriptor<Calendar> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.CalendarDate).Type<DateTimeType>();
            descriptor.Field(f => f.Seats).Type<ListType<SeatType>>();
            descriptor.Field(f => f.SeatCalendars).Type<ListType<SeatCalendarType>>();
        }
    }
}
