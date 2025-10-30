using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Filters;
using TrainReservation.Core.GraphQL.Types;

namespace TrainReservation.Application.GraphQL.Types
{
    public class DateFilterInputType : InputObjectType<DateFilterInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<DateFilterInput> descriptor)
        {
            descriptor.Name("DateFilterInput");
            descriptor.Description("Input type for filtering by date and day of the week.");

            descriptor.Field(f => f.DayOfWeek).Type<DayOfWeekEnumType>().Description("The day of the week to filter by.");
        }
    }
}
