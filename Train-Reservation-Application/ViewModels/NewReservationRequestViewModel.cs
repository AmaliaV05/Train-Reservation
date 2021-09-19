using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.ViewModels
{
    public class NewReservationRequestViewModel
    {
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ReservedSeatsViewModel ReservationWithSeatsViewModel { get; set; }
    }
}
