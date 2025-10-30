using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class SeatCalendarType : ObjectType<SeatCalendar>
    {
        protected override void Configure(IObjectTypeDescriptor<SeatCalendar> descriptor)
        {
            descriptor.Name("SeatCalendar");
            descriptor.Description("Represents the availability of a specific seat on a specific calendar date.");

            descriptor.Field(f => f.Seat).Type<SeatType>().Description("The seat associated with this calendar entry.");
            descriptor.Field(f => f.Calendar).Type<CalendarType>().Description("The calendar entry indicating a specific date.");
            descriptor.Field(f => f.SeatAvailability).Type<BooleanType>().Description("Indicates whether the seat is available on the given calendar date.");
        }
    }
}
