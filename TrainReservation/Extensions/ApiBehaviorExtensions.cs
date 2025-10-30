using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainReservation.Application.ViewModels.Error;

namespace TrainReservation.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IServiceCollection ConfigureCustomApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    Dictionary<string, string[]> errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    ModelError response = new ModelError
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        ErrorMessage = "Validation failed",
                        Errors = errors,
                        Path = context.HttpContext.Request.Path,
                        TraceId = context.HttpContext.TraceIdentifier,
                        TimeStamp = DateTime.UtcNow.ToString("o")
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
