using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.ViewModels
{
    public class TrainViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
