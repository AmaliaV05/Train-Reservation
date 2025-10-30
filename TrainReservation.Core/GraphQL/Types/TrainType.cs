using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class TrainType : ObjectType<Train>
    {
        protected override void Configure(IObjectTypeDescriptor<Train> descriptor)
        {
            descriptor.Name("Train");
            descriptor.Description("Represents a train, including its schedule and cars.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The unique identifier of the train.");
            descriptor.Field(f => f.Name).Type<StringType>().Description("The name of the train.");
            descriptor.Field(f => f.DayOfWeek).Type<DayOfWeekEnumType>().Description("The day of the week the train operates.");
            descriptor.Field(f => f.Cars).Type<ListType<CarType>>().Description("The list of cars that make up this train.");
        }
    }
}
