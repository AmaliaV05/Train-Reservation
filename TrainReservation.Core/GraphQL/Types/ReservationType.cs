using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class ReservationType : ObjectType<Reservation>
    {
        protected override void Configure(IObjectTypeDescriptor<Reservation> descriptor)
        {
            descriptor.Name("Reservation");
            descriptor.Description("Represents a train reservation made by a customer, including one or more seats.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The unique identifier of the reservation.");
            descriptor.Field(f => f.Code).Type<StringType>().Description("A unique code used when updating a reservation.");
            descriptor.Field(f => f.ReservationDate).Type<DateTimeType>().Description("The date and time when the reservation was made.");
            descriptor.Field(f => f.Seats).Type<ListType<SeatType>>().Description("The list of seats included in this reservation.");
            descriptor.Field(f => f.Customer).Type<CustomerType>().Description("The customer who made the reservation.");
            descriptor.Field(f => f.ReservationSeats).Type<ReservationSeatType>().Description("The join entity linking this reservation to specific seat assignments.");
        }
    }
}
