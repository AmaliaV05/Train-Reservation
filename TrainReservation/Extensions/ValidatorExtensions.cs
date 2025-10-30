using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TrainReservation.Application.DTOs;
using TrainReservation.Application.ViewModels.Reservations;
using TrainReservation.Validators;

namespace TrainReservation.Extensions
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddCustomValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<NewReservationRequest>, NewReservationRequestValidator>();
            services.AddTransient<IValidator<ModifyReservationViewModel>, ModifyReservationValidator>();
            services.AddTransient<IValidator<TrainSearchRequest>, GetTrainsByDateValidator>();

            return services;
        }
    }
}
