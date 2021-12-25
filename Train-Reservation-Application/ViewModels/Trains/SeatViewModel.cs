using System.Collections.Generic;

namespace Train_Reservation_Application.ViewModels.Trains
{
    public class SeatViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public IEnumerable<SeatCalendarViewModel> SeatCalendars { get; set; }
    }
}
