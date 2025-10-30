using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainReservation.Application.Interfaces;
using TrainReservation.Infrastructure.Data;
using TrainReservation.Infrastructure.Services;

namespace TrainReservation.Extensions
{
    public static class ServiceInjectionExtension
    {
        public static IServiceCollection AddServicesExtension(this IServiceCollection services)
        {
            services.AddScoped<ReservationsService>();
            services.AddScoped<TrainsService>();
            services.AddScoped<IRestTrainsService, RestTrainsService>();
            services.AddScoped<IRestReservationsService, RestReservationsService>();

            return services;
        }

        public static IServiceCollection AddDatabaseServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });                

            return services;
        }
    }
}
