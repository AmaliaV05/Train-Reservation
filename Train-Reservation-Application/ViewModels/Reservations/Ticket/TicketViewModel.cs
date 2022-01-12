using System.Collections.Generic;

namespace Train_Reservation_Application.ViewModels.Reservations.Ticket
{
    public class TicketViewModel
    {
        public string Name { get; set; }
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}
