using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Filters;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class SeatListFilterInputType : InputObjectType<SeatListFilterInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<SeatListFilterInput> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.CalendarDate).Type<DateTimeType>();
            descriptor.Field(f => f.N).Type<IntType>();
        }
    }
}
