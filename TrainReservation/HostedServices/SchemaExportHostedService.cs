using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using TrainReservation.Infrastructure.GraphQL.Services;
using TrainReservation.Core.GraphQL.Constants;

namespace TrainReservation.HostedServices
{
    public class SchemaExportHostedService : IHostedService
    {
        private readonly SchemaExporter _schemaExporter;
        private readonly IWebHostEnvironment _env;

        public SchemaExportHostedService(SchemaExporter schemaExporter, IWebHostEnvironment env)
        {
            _schemaExporter = schemaExporter;
            _env = env;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_env.IsDevelopment())
            {
                await _schemaExporter.ExportAsync(SchemaName.TrainReservation, "schema-train-reservation.graphql");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
