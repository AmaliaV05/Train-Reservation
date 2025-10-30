using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Filters;
using TrainReservation.Core.GraphQL.Types;

namespace TrainReservation.Application.GraphQL.Types
{
    public class CarTypeFilterInputType : InputObjectType<CarTypeFilterInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<CarTypeFilterInput> descriptor)
        {
            descriptor.Name("CarTypeFilterInput");
            descriptor.Description("Input type for filtering cars by type.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The ID of the car to filter.");
            descriptor.Field(f => f.CalendarDate).Type<DateTimeType>().Description("The calendar date to apply the filter.");
            descriptor.Field(f => f.Type).Type<CarTypeEnumType>().Description("The type of car to filter by.");
        }
    }
}
