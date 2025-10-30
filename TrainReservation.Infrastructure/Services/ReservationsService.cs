using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Application.DTOs;
using TrainReservation.Core.Models;
using TrainReservation.Infrastructure.Data;
using TrainReservation.Infrastructure.Exceptions;

namespace TrainReservation.Infrastructure.Services
{
    public class ReservationsService
    {
        private readonly ApplicationDbContext _context;

        public ReservationsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationResponse> CreateReservationAsync(ReservationRequest reservationRequest)
        {
            var response = new ReservationResponse();
            var bookingCustomerSSN = reservationRequest.SocialSecurityNumber;
            var reservedSeatsIds = reservationRequest.ReservedSeatsIds;
            var reservationDate = reservationRequest.ReservationDate;
            var checkSSN = await _context.Customers
                .Where(customer => customer.SocialSecurityNumber == bookingCustomerSSN)
                .FirstOrDefaultAsync();
            var checkReservation = await CheckExistingReservationAsync(bookingCustomerSSN, reservationDate, reservedSeatsIds);
            if (checkReservation)
            {
                response.Response = await GetTicketAsync(bookingCustomerSSN, reservedSeatsIds, reservationDate);
                response.Message = "A reservation for this train already exists!";
                return response;
            }
            else
            {
                var checkOccupiedSeats = await AddSeatsToReservationAsync(reservedSeatsIds, reservationDate);
                if (checkOccupiedSeats.AlternativeResponse != null)
                {
                    response.AlternativeResponse = checkOccupiedSeats.AlternativeResponse;
                    response.Message = checkOccupiedSeats.Message;
                    return response;
                }
                if (checkSSN != null)
                {
                    List<Seat> reservedSeats = checkOccupiedSeats.Response;
                    await CreateReservationAsync(checkSSN, reservedSeats, reservationDate);
                    response.Response = await GetTicketAsync(bookingCustomerSSN, reservedSeatsIds, reservationDate);
                    response.Message = checkOccupiedSeats.Message;
                    return response;
                }
                else
                {
                    List<Seat> reservedSeats = checkOccupiedSeats.Response;
                    Customer newCustomer = await CreateCustomerAsync(reservationRequest);
                    await CreateReservationAsync(newCustomer, reservedSeats, reservationDate);
                    response.Response = await GetTicketAsync(bookingCustomerSSN, reservedSeatsIds, reservationDate);
                    response.Message = "Customer registration completed & " + checkOccupiedSeats.Message;
                    return response;
                }
            }
        }

        public async Task<ReservationResponse> UpdateReservationAsync(int idReservation, UpdateReservationRequest updateReservationRequest)
        {
            var reservedSeatsIds = updateReservationRequest.ReservedSeatsIds;
            var reservationDate = updateReservationRequest.ReservationDate;
            var oldReservation = await _context.Reservations
                 .Where(reservation => reservation.Id == idReservation)
                 .OrderBy(reservation => reservation.Id)
                 .Include(reservation => reservation.Customer)
                 .Include(reservation => reservation.Seats)
                 .ThenInclude(seat => seat.SeatCalendars)
                 .AsSplitQuery()
                 .FirstOrDefaultAsync();
            if (oldReservation == null)
            {
                throw new IdNotFoundException(nameof(Reservation), idReservation);
            }
            if (idReservation != updateReservationRequest.Id)
            {
                throw new NoMatchException(idReservation, updateReservationRequest.Id, nameof(Reservation));
            }
            var response = new ReservationResponse();
            if (oldReservation.Code != updateReservationRequest.Code)
            {
                response.Message = "Incorrect code";
                return response;
            }
            var seats = CheckOccupiedSeats(reservedSeatsIds, reservationDate);
            if (seats.Message == "occupied")
            {
                List<Seat> occupiedSeatsView = new();
                foreach (var sid in seats.Response)
                {
                    var seatWithId = _context.Seats
                        .Where(seat => seat.Id == sid.Id)
                        .Include(seat => seat.Car)
                        .ThenInclude(car => car.Train)
                        .Include(seat => seat.SeatCalendars.Where(seat => seat.Calendar.CalendarDate.Date == reservationDate.Date))
                        .FirstOrDefault();
                    occupiedSeatsView.Add(seatWithId);
                }
                response.AlternativeResponse = occupiedSeatsView;
                response.Message = "These seats have been previously reserved";
                return response;
            }
            else
            {
                foreach (Seat seat in oldReservation.Seats)
                {
                    seat.SeatCalendars.First().SeatAvailability = false;
                    _context.Entry(seat).State = EntityState.Modified;
                }
                oldReservation.Seats.Clear();
                foreach (Seat seat in seats.Response)
                {
                    oldReservation.Seats.Add(seat);
                    seat.SeatCalendars.First().SeatAvailability = true;
                }
                if (!await SaveChangesAsync())
                {
                    throw new DbUpdateConcurrencyException();
                }
                response.Response = await GetTicketAsync(oldReservation.Customer.SocialSecurityNumber, reservedSeatsIds, reservationDate);
                response.Message = "Your reservation has been updated";
                return response;
            }
        }

        private async Task<Customer> GetTicketAsync(string socialSecurityNumber, List<int> reservedSeatsIds, DateTime reservationDate)
        {
            int trainId = await FindTrainIdAsync(reservedSeatsIds);
            return await _context.Customers
                .Where(customer => customer.SocialSecurityNumber == socialSecurityNumber)
                .OrderBy(customer => customer.Id)
                .Include(customer => customer.Reservations.Where(reservation => reservation.ReservationDate.Date == reservationDate.Date))
                .ThenInclude(reservation => reservation.Seats.Where(seat => seat.Car.Train.Id == trainId))
                .ThenInclude(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        private async Task<int> FindTrainIdAsync(List<int> reservedSeatsIds)
        {
            var currentReservationFirstSeatId = reservedSeatsIds.First();
            var currentReservationSeat = await _context.Seats
                .Where(seat => seat.Id == currentReservationFirstSeatId)
                .OrderBy(seat => seat.Id)
                .Include(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            return currentReservationSeat.Car.Train.Id;
        }

        private async Task<ResponseService<List<Seat>, IEnumerable<Seat>, string>> AddSeatsToReservationAsync(List<int> reservedSeatsIds, DateTime reservationDate)
        {
            var response = new ResponseService<List<Seat>, IEnumerable<Seat>, string>();
            var seats = CheckOccupiedSeats(reservedSeatsIds, reservationDate);
            if (seats.Message == "reserved")
            {
                foreach (var sid in seats.Response)
                {
                    sid.SeatCalendars.First().SeatAvailability = true;
                    _context.Entry(sid).State = EntityState.Modified;
                    await SaveChangesAsync();
                }
                response.Response = seats.Response;
                response.Message = "All selected seats have been reserved";
                return response;
            }
            else
            {
                List<Seat> occupiedSeatsView = new();
                foreach (var sid in seats.Response)
                {
                    var seatWithId = _context.Seats
                        .Where(seat => seat.Id == sid.Id)
                        .Include(seat => seat.Car)
                        .ThenInclude(car => car.Train)
                        .Include(seat => seat.SeatCalendars.Where(seat => seat.Calendar.CalendarDate.Date == reservationDate.Date))
                        .FirstOrDefault();
                    occupiedSeatsView.Add(seatWithId);
                }
                response.AlternativeResponse = occupiedSeatsView;
                response.Message = "These seats have been previously reserved";
                return response;
            }
        }

        private async Task<bool> CheckExistingReservationAsync(string socialSecurityNumber, DateTime reservationDate, List<int> reservedSeatsIds)
        {
            var currentReservationTrainId = await FindTrainIdAsync(reservedSeatsIds);

            var customer = await _context.Customers
                .Where(customer => customer.SocialSecurityNumber == socialSecurityNumber)
                .OrderBy(customer => customer.Id)
                .Include(customer => customer.Reservations.Where(reservation => reservation.ReservationDate.Date == reservationDate.Date))
                .ThenInclude(reservation => reservation.Seats.Where(seat => seat.Car.Train.Id == currentReservationTrainId))
                .ThenInclude(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            if (customer == null || customer.Reservations == null || customer.Reservations.FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }

        private ResponseService<List<Seat>, string> CheckOccupiedSeats(List<int> reservedSeatsIds, DateTime reservationDate)
        {
            ResponseService<List<Seat>, string> response = new();
            List<Seat> reservedSeats = new();
            List<Seat> occupiedSeats = new();
            var count = reservedSeatsIds.Count;
            reservedSeatsIds.ForEach(sid =>
            {
                var seatWithId = _context.Seats
                    .Where(seat => seat.Id == sid)
                    .Include(seat => seat.SeatCalendars.Where(seat => seat.Calendar.CalendarDate.Date == reservationDate.Date))
                    .FirstOrDefault();
                if (seatWithId != null)
                {
                    if (seatWithId.SeatCalendars.First().SeatAvailability == false)
                    {
                        reservedSeats.Add(seatWithId);
                        count--;
                    }
                    else occupiedSeats.Add(seatWithId);
                }
            });
            if (count == 0)
            {
                response.Response = reservedSeats;
                response.Message = "reserved";
                return response;
            }
            else
            {
                response.Response = occupiedSeats;
                response.Message = "occupied";
                return response;
            }
        }

        private async Task<Customer> CreateCustomerAsync(ReservationRequest reservationRequest)
        {
            var newCustomer = new Customer
            {
                SocialSecurityNumber = reservationRequest.SocialSecurityNumber,
                Name = reservationRequest.Name,
                Email = reservationRequest.Email
            };
            _context.Customers.Add(newCustomer);
            await SaveChangesAsync();
            return newCustomer;
        }

        private async Task CreateReservationAsync(Customer customer, List<Seat> reservedSeats, DateTime reservationDate)
        {
            Random rd = new();
            var reservation = new Reservation
            {
                Code = rd.Next(1000000, 9999999).ToString(),
                ReservationDate = reservationDate,
                Seats = reservedSeats,
                Customer = customer
            };
            _context.Reservations.Add(reservation);
            await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
