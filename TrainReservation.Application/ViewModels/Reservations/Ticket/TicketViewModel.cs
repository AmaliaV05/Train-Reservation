using System.Collections.Generic;

namespace TrainReservation.Application.ViewModels.Reservations.Ticket
{
    public class TicketViewModel
    {
        public string Name { get; set; }
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}
