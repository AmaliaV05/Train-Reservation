using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainReservation.Application.GraphQL.Interfaces;
using TrainReservation.Application.GraphQL.Types;
using TrainReservation.Core.GraphQL.Constants;
using TrainReservation.Core.GraphQL.Types;
using TrainReservation.HostedServices;
using TrainReservation.Infrastructure.Data;
using TrainReservation.Infrastructure.GraphQL.Exceptions;
using TrainReservation.Infrastructure.GraphQL.Services;
using TrainReservation.Infrastructure.GraphQL.Types;
using CarType = TrainReservation.Core.GraphQL.Types.CarType;

namespace TrainReservation.Extensions
{
    public static class GraphQLServerExtensions
    {
        public static IServiceCollection AddGraphQLService(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("FeatureManagement:UseGraphQL"))
            {
                services.AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

                services.AddTransient<IGraphQLTrainsService, GraphQLTrainsService>();
                services.AddTransient<IGraphQLReservationsService, GraphQLReservationsService>();
                services.AddScoped<CustomErrorFilter>();

                services.AddGraphQLServer(SchemaName.TrainReservation)
                    .AddQueryType<TrainQueryType>()
                    .AddMutationType<ReservationMutationType>()
                    .AddType<TrainType>()
                    .AddType<CarType>()
                    .AddType<SeatType>()
                    .AddType<CustomerType>()
                    .AddType<ReservationType>()
                    .AddType<CalendarType>()
                    .AddType<SeatCalendarType>()
                    .AddType<ReservationSeatType>()
                    .AddType<DayOfWeekEnumType>()
                    .AddType<CarTypeEnumType>()
                    .AddType<AddReservationInputType>()
                    .AddType<AddReservationPayloadType>()
                    .AddType<UpdateReservationInputType>()
                    .AddType<UpdateReservationPayloadType>()
                    .AddType<CarTypeFilterInputType>()
                    .AddType<DateFilterInputType>()
                    .AddType<SeatListFilterInputType>()
                    .AddType<UpdateReservationDetailsInputType>()
                    .ModifyRequestOptions(opts =>
                    {
                        opts.IncludeExceptionDetails = configuration.GetValue<bool>("GraphQL:ShowDetailedErrors");
                    })
                    .AddErrorFilter<CustomErrorFilter>();

                services.AddSingleton<SchemaExporter>();
                services.AddHostedService<SchemaExportHostedService>();
            }            

            return services;
        }
    }
}
