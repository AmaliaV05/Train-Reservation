﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Models
{
    public class SeatCalendar
    {
        public Seat Seat { get; set; }
        public Calendar Calendar { get; set; }
        public bool SeatAvailability { get; set; }
    }
}
