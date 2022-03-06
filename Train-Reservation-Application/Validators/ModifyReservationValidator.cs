using FluentValidation;
using System;
using Train_Reservation_Application.ViewModels.Reservations;

namespace Train_Reservation_Application.Validators
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
