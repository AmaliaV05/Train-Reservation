using AutoMapper;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels;
using Train_Reservation_Application.ViewModels.Trains;

namespace Train_Reservation_Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Train, TrainViewModel>();

            CreateMap<Train, TrainWithCarsViewModel>();            
            CreateMap<Car, CarWithSeatsViewModel>();
            CreateMap<Seat, SeatViewModel>();
            CreateMap<SeatCalendar, SeatCalendarViewModel>();
            CreateMap<Calendar, CalendarViewModel>();

            CreateMap<Reservation, ReservedSeatsViewModel>();
            CreateMap<Customer, NewReservationRequestViewModel>();

            CreateMap<Customer, OldCustomerReservationViewModel>();
            CreateMap<Reservation, ReservationWithSeatsViewModel>();
            CreateMap<Reservation, ModifyReservationViewModel>();           
        }
    }
}
