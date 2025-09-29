using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Train_Reservation_Application.Data;
using Train_Reservation_Application.GraphQL.Inputs;
using Train_Reservation_Application.GraphQL.Interfaces;
using Train_Reservation_Application.GraphQL.Payloads;
using Train_Reservation_Application.Services;

namespace Train_Reservation_Application.GraphQL.Services
{
    public class GraphQLReservationsService : IGraphQLReservationsService
    {
        private readonly ReservationsService _reservationService;
        private readonly ApplicationDbContext _context;

        public GraphQLReservationsService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
            _reservationService = new ReservationsService(_context);
        }

        public async Task<AddReservationPayload> CreateReservationAsync(AddReservationInput input)
        {
            var request = new ReservationRequest
            {
                Email = input.Email,
                Name = input.Name,
                SocialSecurityNumber = input.SocialSecurityNumber,
                ReservationDate = input.ReservationDate,
                ReservedSeatsIds = input.ReservedSeatsIds
            };

            var response = await _reservationService.CreateReservationAsync(request);

            return new AddReservationPayload(response.Response, response.AlternativeResponse, response.Message);
        }

        public async Task<UpdateReservationPayload> UpdateReservationAsync(UpdateReservationInput input)
        {
            var request = new UpdateReservationRequest
            {
                Id = input.Reservation.Id,
                Code = input.Reservation.Code,
                ReservationDate = input.Reservation.ReservationDate,
                ReservedSeatsIds = input.Reservation.ReservedSeatsIds
            };

            var response  = await _reservationService.UpdateReservationAsync(input.IdReservation, request);

            return new UpdateReservationPayload(response.Response, response.AlternativeResponse, response.Message);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();

            GC.SuppressFinalize(this);
        }
    }
}
