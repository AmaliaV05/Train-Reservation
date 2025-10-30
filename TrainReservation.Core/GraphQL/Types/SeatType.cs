using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class SeatType : ObjectType<Seat>
    {
        protected override void Configure(IObjectTypeDescriptor<Seat> descriptor)
        {
            descriptor.Name("Seat");
            descriptor.Description("Represents a seat within a train car, including its reservations and availability.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The unique identifier of the seat.");
            descriptor.Field(f => f.Number).Type<IntType>().Description("The seat number within the car.");
            descriptor.Field(f => f.Car).Type<CarType>().Description("The car to which this seat belongs.");
            descriptor.Field(f => f.Reservations).Type<ListType<ReservationType>>().Description("The list of reservations associated with this seat.");
            descriptor.Field(f => f.Calendars).Type<ListType<CalendarType>>().Description("The list of calendar entries relevant to this seat.");
            descriptor.Field(f => f.SeatCalendars).Type<ListType<SeatCalendarType>>().Description("The list of seat-calendar availability mappings.");
            descriptor.Field(f => f.ReservationSeats).Type<ListType<ReservationSeatType>>().Description("The list of reservation-seat mappings for this seat.");
        }
    }
}
