using System.Collections.Generic;

namespace Train_Reservation_Application.Models
{
    public enum CarType
    {
        All = 0,
        FirstClass = 1,
        SecondClass = 2,
        Sleeping = 3
    }

    public class Car
    {
        public int Id { get; set; }
        public int CarNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public CarType Type { get; set; }
        public Train Train { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
