using HotChocolate.Execution;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TrainReservation.Infrastructure.GraphQL.Services
{
    public class SchemaExporter
    {
        private readonly IRequestExecutorResolver _executorResolver;

        public SchemaExporter(IRequestExecutorResolver executorResolver)
        {
            _executorResolver = executorResolver;
        }

        public async Task ExportAsync(string schemaName = "default", string filePath = "schema.graphql")
        {
            var executor = await _executorResolver.GetRequestExecutorAsync(schemaName);
            var newSchema = executor.Schema.ToString();

            // Check if the file exists and the content is the same
            if (File.Exists(filePath))
            {
                var existingSchema = await File.ReadAllTextAsync(filePath);

                if (string.Equals(existingSchema, newSchema, StringComparison.Ordinal))
                {
                    return;
                }
            }

            // Write the new schema if it's different or file doesn't exist
            await File.WriteAllTextAsync(filePath, newSchema);
        }
    }
}
