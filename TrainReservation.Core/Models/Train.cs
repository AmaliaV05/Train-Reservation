using System;
using System.Collections.Generic;

namespace TrainReservation.Core.Models
{
    public class Train
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<Car> Cars { get; set; }
    }
}
