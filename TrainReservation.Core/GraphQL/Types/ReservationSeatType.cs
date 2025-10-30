using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class ReservationSeatType : ObjectType<ReservationSeat>
    {
        protected override void Configure(IObjectTypeDescriptor<ReservationSeat> descriptor)
        {
            descriptor.Name("ReservationSeat");
            descriptor.Description("Represents the association between a reservation and a seat.");

            descriptor.Field(f => f.Reservation).Type<ReservationType>().Description("The reservation associated with this seat.");
            descriptor.Field(f => f.Seat).Type<SeatType>().Description("The seat associated with this reservation.");
        }
    }

}
