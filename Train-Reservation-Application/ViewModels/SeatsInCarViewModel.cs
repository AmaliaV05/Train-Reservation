﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_Reservation_Application.ViewModels
{
    public class SeatsInCarViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Available { get; set; }
        public CarInTrainViewModel Car { get; set; }
    }
}