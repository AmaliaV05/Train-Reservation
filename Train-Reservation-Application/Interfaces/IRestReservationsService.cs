using System.Collections.Generic;
using System.Threading.Tasks;
using Train_Reservation_Application.Services;
using Train_Reservation_Application.ViewModels.Reservations;
using Train_Reservation_Application.ViewModels.Reservations.Ticket;

namespace Train_Reservation_Application.Interfaces
{
    public interface IRestReservationsService
    {
        Task<ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>> NewReservation(NewReservationRequest newReservationRequest);
        Task<ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>> UpdateReservation(int idReservation, ModifyReservationViewModel modifyReservedSeats);
    }
}
