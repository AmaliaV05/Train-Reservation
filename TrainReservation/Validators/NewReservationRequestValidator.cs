using FluentValidation;
using System;
using TrainReservation.Application.ViewModels.Reservations;

namespace TrainReservation.Validators
{
    public class NewReservationRequestValidator : AbstractValidator<NewReservationRequest>
    {
        public NewReservationRequestValidator()
        {
            RuleFor(x => x.SocialSecurityNumber).NotNull().Length(13, 13);
            RuleFor(x => x.Name).NotNull().MinimumLength(10);
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.ReservationWithSeatsViewModel.ReservationDate).Must(BeAValidDate).WithMessage("Reservation date cannot be prior to today's date");
            RuleFor(x => x.ReservationWithSeatsViewModel.ReservedSeatsIds).NotEmpty();
        }

        private bool BeAValidDate(DateTime date)
        {
            return date >= DateTime.Today;
        }
    }
}
