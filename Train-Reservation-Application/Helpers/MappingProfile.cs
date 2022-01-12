using AutoMapper;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels.Reservations.Ticket;
using Train_Reservation_Application.ViewModels.Trains;

namespace Train_Reservation_Application.Helpers
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

            CreateMap<Customer, TicketViewModel>();
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<Seat, SeatInCarViewModel>();
            CreateMap<Car, CarInTrainViewModel>();
        }
    }
}
