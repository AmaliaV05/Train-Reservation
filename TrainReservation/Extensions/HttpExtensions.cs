using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TrainReservation.Application.ViewModels.Error;

namespace TrainReservation.Extensions
{
    public static class HttpExtensions
    {
        public static async Task AddErrorMessage(this HttpResponse response, int statusCode, string errorMessage, string errorStack, string path, string traceId,Dictionary<string, string[]> errors = null)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var modelError = new ModelError
            {
                StatusCode = statusCode,
                ErrorMessage = errorMessage,
                Errors = errors,
                ErrorStack = errorStack,                
                Path = path,
                TraceId = traceId,
                TimeStamp = DateTime.UtcNow.ToString("o")
            };

            await response.WriteAsync(JsonSerializer.Serialize(modelError, options));
        }
    }
}
