using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Train_Reservation_Application.Models.Error;

namespace Train_Reservation_Application.Extensions
{
    public static class HttpExtensions
    {
        public static async Task AddErrorMessage(this HttpResponse response, int statusCode, string errorMessage, string errorStack)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var modelError = new ModelError
            {
                StatusCode = statusCode,
                ErrorMessage = errorMessage,
                ErrorStack = errorStack
            };
            await response.WriteAsync(JsonSerializer.Serialize(modelError, options));
        }
    }
}
