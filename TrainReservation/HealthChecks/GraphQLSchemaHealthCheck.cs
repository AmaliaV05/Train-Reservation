using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrainReservation.Core.GraphQL.Constants;

namespace TrainReservation.HealthChecks
{
    public class GraphQLSchemaHealthCheck : IHealthCheck
    {
        private readonly IServiceProvider _services;
        private readonly IFeatureManager _featureManager;

        public GraphQLSchemaHealthCheck(IServiceProvider services, IFeatureManager featureManager)
        {
            _services = services;
            _featureManager = featureManager;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (!await _featureManager.IsEnabledAsync("UseGraphQL"))
            {
                // Feature off, consider healthy because GraphQL not active
                return HealthCheckResult.Healthy("GraphQL feature disabled; schema not registered.", new Dictionary<string, object>
                {
                    { "schema", SchemaName.TrainReservation },
                    { "feature-flag", "off" }
                });
            }

            var executorResolver = _services.GetService<IRequestExecutorResolver>();
            if (executorResolver is null)
            {
                return HealthCheckResult.Unhealthy("GraphQL schema service is missing.");
            }

            try
            {
                // Try to resolve the request executor (this builds the schema if needed)
                var executor = await executorResolver.GetRequestExecutorAsync(SchemaName.TrainReservation, cancellationToken);

                return HealthCheckResult.Healthy("GraphQL schema is ready.", new Dictionary<string, object>
                {
                    { "schema", SchemaName.TrainReservation },
                    { "feature-flag", "on" }
                });
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("GraphQL schema failed to initialize.", ex, new Dictionary<string, object>
                {
                    { "schema", SchemaName.TrainReservation }
                });
            }
        }
    }
}
