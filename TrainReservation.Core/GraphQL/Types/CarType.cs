using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class CarType : ObjectType<Car>
    {
        protected override void Configure(IObjectTypeDescriptor<Car> descriptor)
        {
            descriptor.Name("Car");
            descriptor.Description("Represents a train car containing multiple seats.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("Unique identifier of the car.");
            descriptor.Field(f => f.CarNumber).Type<IntType>().Description("Indicates the position of the car in the train.");
            descriptor.Field(f => f.NumberOfSeats).Type<IntType>().Description("The total number of seats in the car.");
            descriptor.Field(f => f.Type).Type<CarTypeEnumType>().Description("The category of this train car, such as first class, second class, or sleeper.");
            descriptor.Field(f => f.Train).Type<TrainType>().Description("The train to which this car belongs.");
            descriptor.Field(f => f.Seats).Type<ListType<SeatType>>().Description("The list of seats contained within this car.");
        }
    }
}
