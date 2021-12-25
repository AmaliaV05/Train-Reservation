using System.Collections.Generic;

namespace Train_Reservation_Application.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Car Car { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Calendar> Calendars { get; set; }
        public List<SeatCalendar> SeatCalendars { get; set; }
    }
}
