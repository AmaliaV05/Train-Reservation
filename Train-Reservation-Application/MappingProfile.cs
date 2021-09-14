using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels;

namespace Train_Reservation_Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Train, TrainViewModel>();
            CreateMap<Car, CarViewModel>();
        }
    }
}
