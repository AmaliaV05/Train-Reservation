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
            CreateMap<Train, TrainWithCarsViewModel>();
            CreateMap<Train, TrainViewModel>();
            CreateMap<Car, CarWithSeatsViewModel>();
            CreateMap<Seat, SeatViewModel>();
            CreateMap<Reservation, ReservedSeatsViewModel>();
            CreateMap<Customer, NewReservationRequestViewModel>();

            CreateMap<Customer, OldCustomerReservationViewModel>();
            CreateMap<Reservation, ReservationWithSeatsViewModel>();
            CreateMap<Reservation, ModifyReservationViewModel>();
            CreateMap<Seat, SeatsInCarViewModel>();
            CreateMap<Car, CarInTrainViewModel>();
            CreateMap<Calendar, CalendarViewModel>();
            CreateMap<SeatCalendar, SeatCalendarViewModel>();
        }
    }
}
