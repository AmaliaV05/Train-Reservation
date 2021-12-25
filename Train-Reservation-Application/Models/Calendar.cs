using System;
using System.Collections.Generic;

namespace Train_Reservation_Application.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public DateTime CalendarDate { get; set; }
        public List<Seat> Seats { get; set; }
        public List<SeatCalendar> SeatCalendars { get; set; }
    }
}
