using System;

namespace TrainReservation.Application.GraphQL.Filters
{
    public sealed record SeatListFilterInput(
        int Id,
        DateTime CalendarDate,
        int N
    );
}
