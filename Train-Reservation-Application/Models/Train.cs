using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Models
{
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class Train
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
