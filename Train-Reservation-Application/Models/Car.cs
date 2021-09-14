using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Models
{
    public enum Type
    {
        FirstClass,
        SecondClass,
        Sleeping
    }

    public class Car
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public Type Type { get; set; }
        public Train Train { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
