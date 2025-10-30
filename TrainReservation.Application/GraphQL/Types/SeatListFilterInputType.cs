using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Filters;

namespace TrainReservation.Application.GraphQL.Types
{
    public class SeatListFilterInputType : InputObjectType<SeatListFilterInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<SeatListFilterInput> descriptor)
        {
            descriptor.Name("SeatListFilterInput");
            descriptor.Description("Input type for filtering seat lists by car ID and date.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The ID of the seat to filter.");
            descriptor.Field(f => f.CalendarDate).Type<DateTimeType>().Description("The calendar date to filter seat availability.");
            descriptor.Field(f => f.N).Type<IntType>().Description("The number of seats to return.");
        }
    }
}
