using System.Collections.Generic;
using System;

namespace TrainReservation.Application.GraphQL.Inputs
{
    public sealed record UpdateReservationInput(
        int IdReservation,
        UpdateReservationDetailsInput Reservation
    );

    public sealed record UpdateReservationDetailsInput(
        int Id,
        string Code,
        DateTime ReservationDate,
        List<int> ReservedSeatsIds
    );
}
