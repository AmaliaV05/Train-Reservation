using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Application.DTOs;
using TrainReservation.Application.Interfaces;
using TrainReservation.Application.ViewModels.Reservations;
using TrainReservation.Application.ViewModels.Reservations.Ticket;
using TrainReservation.Core.Models;

namespace TrainReservation.Infrastructure.Services
{
    public class RestReservationsService : IRestReservationsService
    {
        private readonly ReservationsService _reservationsService;
        private readonly IMapper _mapper;

        public RestReservationsService(ReservationsService reservationsService, IMapper mapper)
        {
            _reservationsService = reservationsService;
            _mapper = mapper;
        }

        public async Task<ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>> NewReservation(NewReservationRequest newReservationRequest)
        {
            var reservationRequest = new ReservationRequest
            {
                SocialSecurityNumber = newReservationRequest.SocialSecurityNumber,
                Name = newReservationRequest.Name,
                Email = newReservationRequest.Email,
                ReservationDate = newReservationRequest.ReservationWithSeatsViewModel.ReservationDate,
                ReservedSeatsIds = newReservationRequest.ReservationWithSeatsViewModel.ReservedSeatsIds                
            };

            var response = await _reservationsService.CreateReservationAsync(reservationRequest);

            return new ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>
            {
                Response = _mapper.Map<Customer, TicketViewModel>(response?.Response),
                AlternativeResponse = response.AlternativeResponse?.Select(seat => _mapper.Map<SeatInCarViewModel>(seat)),
                Message = response.Message
            };
        }

        public async Task<ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>> UpdateReservation(int idReservation, ModifyReservationViewModel modifyReservedSeats)
        {
            var reservationRequest = new UpdateReservationRequest
            {
                Id = idReservation,
                Code = modifyReservedSeats.Code,
                ReservationDate = modifyReservedSeats.ReservationDate,
                ReservedSeatsIds = modifyReservedSeats.ReservedSeatsIds
            };

            var response = await _reservationsService.UpdateReservationAsync(idReservation, reservationRequest);

            return new ResponseService<TicketViewModel, IEnumerable<SeatInCarViewModel>, string>
            {
                Response = _mapper.Map<Customer, TicketViewModel>(response?.Response),
                AlternativeResponse = response.AlternativeResponse?.Select(seat => _mapper.Map<SeatInCarViewModel>(seat)),
                Message = response.Message
            };
        }
    }
}
