using System.Collections.Generic;
using System;

namespace Train_Reservation_Application.GraphQL.Inputs
{
    public sealed record AddReservationInput(
        string SocialSecurityNumber,
        string Name,
        string Email,
        DateTime ReservationDate,
        List<int> ReservedSeatsIds
    );
}
