﻿using System.Collections.Generic;

namespace Train_Reservation_Application.ViewModels
{
    public class OldCustomerReservationViewModel
    {
        public int Id { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ReservationWithSeatsViewModel> ReservationsWithSeats { get; set; }
    }
}
