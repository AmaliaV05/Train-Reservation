using HotChocolate.Types;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class CarType : ObjectType<Car>
    {
        protected override void Configure(IObjectTypeDescriptor<Car> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.CarNumber).Type<IntType>();
            descriptor.Field(f => f.NumberOfSeats).Type<IntType>();
            descriptor.Field(f => f.Type).Type<EnumType<Models.CarType>>();
            descriptor.Field(f => f.Train).Type<TrainType>();
            descriptor.Field(f => f.Seats).Type<ListType<SeatType>>();
        }
    }
}
