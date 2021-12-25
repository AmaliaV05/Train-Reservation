using System;

namespace Train_Reservation_Application.ViewModels.Trains
{
    public class TrainViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
