using HotChocolate.Types;
using System;
using Train_Reservation_Application.GraphQL.Filters;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class DateFilterInputType : InputObjectType<DateFilterInput>
    {
        protected override void Configure(
            IInputObjectTypeDescriptor<DateFilterInput> descriptor)
        {
            descriptor.Field(f => f.DayOfWeek).Type<EnumType<DayOfWeek>>();
        }
    }
}
