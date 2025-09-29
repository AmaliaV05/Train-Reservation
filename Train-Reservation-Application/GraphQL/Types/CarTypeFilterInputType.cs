using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Filters;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class CarTypeFilterInputType : InputObjectType<CarTypeFilterInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<CarTypeFilterInput> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.CalendarDate).Type<DateTimeType>();
            descriptor.Field(f => f.Type).Type<EnumType<Models.CarType>>();
        }
    }
}
