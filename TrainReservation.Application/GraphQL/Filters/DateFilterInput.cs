using System;

namespace TrainReservation.Application.GraphQL.Filters
{
    public sealed record DateFilterInput(
        DayOfWeek DayOfWeek
    );
}
