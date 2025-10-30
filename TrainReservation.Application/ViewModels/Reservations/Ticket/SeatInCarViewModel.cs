namespace TrainReservation.Application.ViewModels.Reservations.Ticket
{
    public class SeatInCarViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public CarInTrainViewModel Car { get; set; }
    }
}
