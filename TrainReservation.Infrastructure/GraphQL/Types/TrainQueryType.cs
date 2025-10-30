using HotChocolate.Types;
using TrainReservation.Application.GraphQL.Filters;
using TrainReservation.Application.GraphQL.Interfaces;
using TrainReservation.Application.GraphQL.Types;
using TrainReservation.Core.GraphQL.Types;
using TrainReservation.Infrastructure.GraphQL.Queries;

namespace TrainReservation.Infrastructure.GraphQL.Types
{
    public class TrainQueryType : ObjectType<TrainQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<TrainQuery> descriptor)
        {
            descriptor
                .Field("trainsByDate")
                .Type<ListType<TrainType>>()
                .Argument("filter", a => a.Type<NonNullType<DateFilterInputType>>())
                .Resolve(async context =>
                {
                    var trainsService = context.Service<IGraphQLTrainsService>();
                    var filter = context.ArgumentValue<DateFilterInput>("filter");
                    return await trainsService.GetTrainsByDateAsync(filter);
                })
                .Description("A customer views the train schedule for a chosen day.");

            descriptor
                .Field("carsByType")
                .Type<TrainType>()
                .Argument("filter", a => a.Type<NonNullType<CarTypeFilterInputType>>())
                .Resolve(async context =>
                {
                    var trainsService = context.Service<IGraphQLTrainsService>();
                    var filter = context.ArgumentValue<CarTypeFilterInput>("filter");
                    return await trainsService.GetCarsByTypeAsync(filter);
                })
                .Description("A customer can filter the cars by their type");

            descriptor
                .Field("seatList")
                .Type<ListType<IntType>>()
                .Argument("filter", a => a.Type<NonNullType<SeatListFilterInputType>>())
                .Resolve(async context =>
                {
                    var trainsService = context.Service<IGraphQLTrainsService>();
                    var filter = context.ArgumentValue<SeatListFilterInput>("filter");
                    return await trainsService.GetSeatListAsync(filter);
                })
                .Description("A customer can filter the cars by the number of available seats next to each other");
        }
    }
}
