using HotChocolate.Types;
using TrainReservation.Core.Models;

namespace TrainReservation.Core.GraphQL.Types
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor.Name("Customer");
            descriptor.Description("Represents a customer who books train reservations.");

            descriptor.Field(f => f.Id).Type<IntType>().Description("The unique identifier of the customer.");
            descriptor.Field(f => f.SocialSecurityNumber).Type<StringType>().Description("The customer's Social Security Number (sensitive data).");
            descriptor.Field(f => f.Name).Type<StringType>().Description("The full name of the customer.");
            descriptor.Field(f => f.Email).Type<StringType>().Description("The email address of the customer.");
            descriptor.Field(f => f.Reservations).Type<ListType<ReservationType>>().Description("The list of reservations made by the customer.");
        }
    }
}
