using FluentValidation;
using System;
using TrainReservation.Application.DTOs;

namespace TrainReservation.Validators
{
    public class GetTrainsByDateValidator : AbstractValidator<TrainSearchRequest>
    {
        public GetTrainsByDateValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Date is required.");
            RuleFor(x => x.Date).Must(date => date <= DateTime.Today.Date).WithMessage("Date must not be in the past.");
        }
    }
}
