using FluentValidation;
using System;
using TrainReservation.Application.ViewModels.Reservations;

namespace TrainReservation.Validators
{
    public class ModifyReservationValidator : AbstractValidator<ModifyReservationViewModel>
    {
        public ModifyReservationValidator()
        {
            RuleFor(x => x.Code).NotNull().Length(7, 7);
            RuleFor(x => x.ReservationDate).Must(BeAValidDate).WithMessage("Reservation date cannot be prior to today's date");
            RuleFor(x => x.ReservedSeatsIds).NotEmpty();
        }
        private bool BeAValidDate(DateTime date)
        {
            return date >= DateTime.Today;
        }
    }
}
