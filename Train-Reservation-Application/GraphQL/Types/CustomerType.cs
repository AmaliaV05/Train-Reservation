using HotChocolate.Types;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Types
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor.Field(f => f.Id).Type<IntType>();
            descriptor.Field(f => f.SocialSecurityNumber).Type<StringType>();
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.Email).Type<StringType>();
            descriptor.Field(f => f.Reservations).Type<ListType<ReservationType>>();
        }
    }
}
