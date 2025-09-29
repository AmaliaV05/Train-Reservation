using System;

namespace Train_Reservation_Application.GraphQL.Filters
{
    public sealed record SeatListFilterInput(
        int Id,
        DateTime CalendarDate,
        int N
    );
}
