﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Car Car { get; set; }
        public List<Reservation> Reservation { get; set; }
        public List<Calendar> Calendars { get; set; }
        public List<SeatCalendar> SeatCalendars { get; set; }
    }
}
