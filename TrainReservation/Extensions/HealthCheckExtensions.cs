using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TrainReservation.HealthChecks;
using TrainReservation.Infrastructure.Data;

namespace TrainReservation.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecksService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(
                    name: "SQLServer",
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "db", "sql", "sqlserver", "connection" })
                .AddDbContextCheck<ApplicationDbContext>(
                    name: "ApplicationDbContext",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "db", "sql", "available" })
                .AddCheck<GraphQLSchemaHealthCheck>(
                    name: "GraphQL_Schema",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "graphql", "schema", "initialization" });
            
            return services;
        }
    }
}
