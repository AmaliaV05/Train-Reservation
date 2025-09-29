using System;

namespace Train_Reservation_Application.GraphQL.Filters
{
    public sealed record DateFilterInput(
        DayOfWeek DayOfWeek
    );
}
