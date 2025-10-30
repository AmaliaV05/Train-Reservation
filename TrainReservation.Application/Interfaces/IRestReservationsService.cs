using System.Collections.Generic;
using System.Threading.Tasks;
using TrainReservation.Application.DTOs;
using TrainReservation.Application.ViewModels.Reservations;
using TrainReservation.Application.ViewModels.Reservations.Ticket;

namespace TrainReservation.Application.Interfaces
{
    public interface IRestReservationsService
    {
        Task<ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>> NewReservation(NewReservationRequest newReservationRequest);
        Task<ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>> UpdateReservation(int idReservation, ModifyReservationViewModel modifyReservedSeats);
    }
}
