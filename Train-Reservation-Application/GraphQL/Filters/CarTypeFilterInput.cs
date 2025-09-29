using System;

namespace Train_Reservation_Application.GraphQL.Filters
{
    public sealed record CarTypeFilterInput(
        int Id,
        DateTime CalendarDate,
        Models.CarType Type
    );
}
