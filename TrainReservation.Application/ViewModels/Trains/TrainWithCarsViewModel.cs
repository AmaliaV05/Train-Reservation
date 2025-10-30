using System;
using System.Collections.Generic;

namespace TrainReservation.Application.ViewModels.Trains
{
    public class TrainWithCarsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public IEnumerable<CarWithSeatsViewModel> Cars { get; set; }
    }
}
