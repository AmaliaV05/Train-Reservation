﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.ViewModels
{
    public class ReservationWithSeatsViewModel
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<SeatsInCarViewModel> ReservedSeats { get; set; }
    }
}