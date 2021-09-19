using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.ViewModels
{
    public class ReservedSeatsViewModel
    {
        public DateTime ReservationDate { get; set; }
        public List<int> ReservedSeatsIds { get; set; }
    }
}
