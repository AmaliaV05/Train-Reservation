using HotChocolate.Types;

namespace TrainReservation.Core.GraphQL.Types
{
    public class CarTypeEnumType : EnumType<Models.CarType>
    {
        protected override void Configure(IEnumTypeDescriptor<Models.CarType> descriptor)
        {
            descriptor.Name("CarType");
            descriptor.Description("The type of train car.");

            descriptor.Value(Models.CarType.All).Name("ALL");
            descriptor.Value(Models.CarType.FirstClass).Name("FIRST_CLASS");
            descriptor.Value(Models.CarType.SecondClass).Name("SECOND_CLASS");
            descriptor.Value(Models.CarType.Sleeping).Name("SLEEPING");
        }
    }
}
