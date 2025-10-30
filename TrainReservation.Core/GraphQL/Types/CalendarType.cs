using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class CalendarType : ObjectType<Calendar>
    {
        protected override void Configure(IObjectTypeDescriptor<Calendar> descriptor)
        {
            descriptor.Name("Calendar");
            descriptor.Description("Represents a calendar where seats are assigned to specific dates.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("Unique identifier of the calendar.");
            descriptor.Field(f => f.CalendarDate).Type<DateTimeType>().Description("A specific date within the calendar.");
            descriptor.Field(f => f.Seats).Type<ListType<SeatType>>().Description("List of all seats associated with a specific date, regardless of their availability.");
            descriptor.Field(f => f.SeatCalendars).Type<ListType<SeatCalendarType>>().Description("List of seats reserved or available for a specific date.");
        }
    }
}
