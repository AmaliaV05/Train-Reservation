using System;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.GraphQL.Filters
{
    public sealed record CarTypeFilterInput(
        int Id,
        DateTime CalendarDate,
        CarType Type
    );
}
