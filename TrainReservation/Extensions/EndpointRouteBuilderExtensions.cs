using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainReservation.Core.GraphQL.Constants;

namespace TrainReservation.Extensions
{
    public static  class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapHealthEndpoint(this IEndpointRouteBuilder endpoints)
        {
            // TODO: Add authorization for production
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return endpoints;
        }

        public static IEndpointRouteBuilder MapGraphQLEndpoint(this IEndpointRouteBuilder endpoints, IConfiguration configuration)
        {
            // TODO: Remove access to bananacakepop playground in production
            if (configuration.GetValue<bool>("FeatureManagement:UseGraphQL"))
            {
                endpoints.MapGraphQL("/graphql", SchemaName.TrainReservation);
            }

            return endpoints;
        }

        private static bool IsDevelopment(IEndpointRouteBuilder endpoints)
        {
            var env = endpoints.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            return env.IsDevelopment();
        }
    }
}
