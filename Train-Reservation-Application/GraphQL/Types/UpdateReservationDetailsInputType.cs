using HotChocolate.Types;
using Train_Reservation_Application.GraphQL.Inputs;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class UpdateReservationDetailsInputType : InputObjectType<UpdateReservationDetailsInput>
    {        
        protected override void Configure(
           IInputObjectTypeDescriptor<UpdateReservationDetailsInput> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.Code).Type<StringType>();
            descriptor.Field(f => f.ReservationDate).Type<DateTimeType>();
            descriptor.Field(f => f.ReservedSeatsIds).Type<ListType<IntType>>();
        }
    }
}
