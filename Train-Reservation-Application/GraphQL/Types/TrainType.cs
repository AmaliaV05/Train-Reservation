using HotChocolate.Types;
using System;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class TrainType : ObjectType<Train>
    {
        protected override void Configure(IObjectTypeDescriptor<Train> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.DayOfWeek).Type<EnumType<DayOfWeek>>();
            descriptor.Field(f => f.Cars).Type<ListType<CarType>>();
        }
    }
}
