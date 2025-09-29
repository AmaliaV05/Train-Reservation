using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Train_Reservation_Application.Data;
using Train_Reservation_Application.GraphQL.Interfaces;
using Train_Reservation_Application.GraphQL.Services;
using Train_Reservation_Application.GraphQL.Types;
using CarType = Train_Reservation_Application.GraphQL.Types.CarType;

namespace Train_Reservation_Application.Extensions
{
    public static class GraphQLServerExtensions
    {
        public static IServiceCollection AddGraphQLService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IGraphQLTrainsService, GraphQLTrainsService>();
            services.AddTransient<IGraphQLReservationsService, GraphQLReservationsService>();

            services.AddGraphQLServer()
                .AddQueryType<TrainQueryType>()                
                .AddMutationType<ReservationMutationType>()
                .AddType<TrainType>()
                .AddType<CarType>()
                .AddType<SeatType>()
                .AddType<CustomerType>()
                .AddType<ReservationType>()
                .AddType<CalendarType>()
                .AddType<SeatCalendarType>()
                .AddEnumType<DayOfWeek>()
                .AddEnumType<Models.CarType>()
                .ModifyRequestOptions(opts =>
                {
                    opts.IncludeExceptionDetails = true;
                });

            return services;
        }
    }
}
