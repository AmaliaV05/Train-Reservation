using AutoMapper;
using TrainReservation.Application.ViewModels.Reservations.Ticket;
using TrainReservation.Application.ViewModels.Trains;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.Helpers
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
