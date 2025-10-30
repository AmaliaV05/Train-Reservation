namespace TrainReservation.Application.ViewModels.Reservations
{
    public class NewReservationRequest
    {
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ReservedSeatsViewModel ReservationWithSeatsViewModel { get; set; }
    }
}
