using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Reservation_Application.Data;
using Train_Reservation_Application.Models;
using Train_Reservation_Application.ViewModels;

namespace Train_Reservation_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPut("{idReservation}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutReservation(int idReservation, ModifyReservationViewModel modifyReservedSeats)
        {
            var oldReservation = _context.Reservations
                .Where(reservation => reservation.Id == idReservation)
                .Include(reservation => reservation.Seats)
                .First();

            if (oldReservation.Code != modifyReservedSeats.Code)
            {
                return BadRequest("Incorrect code");
            }

            if (modifyReservedSeats.ReservedSeatsIds.Count == 0)
            {
                return BadRequest("No seat is selected");
            }

            foreach (Seat seat in oldReservation.Seats)
            {
                foreach (SeatCalendar seatCalendar in seat.SeatCalendars)
                {
                    seatCalendar.SeatAvailability = false;
                    _context.Entry(seat).State = EntityState.Modified;
                }
            }

            modifyReservedSeats.ReservedSeatsIds.ForEach(sid =>
            {
                var seatWithId = _context.Seats.Find(sid);

                if (seatWithId != null)
                {
                    var availableSeat = seatWithId.SeatCalendars
                    .Where(seat => seat.SeatAvailability == false)
                    .Where(seat => seat.Calendar.CalendarDate.Date == modifyReservedSeats.ReservationDate.Date)
                    .First();

                    if (availableSeat != null)
                    {
                        //seatWithId.
                    }

                    availableSeat.SeatAvailability = true;
                    _context.Entry(availableSeat).State = EntityState.Modified;
                }
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(idReservation))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostReservation(NewReservationRequestViewModel newReservationRequest)
        {
            var bookingCustomerSSN = newReservationRequest.SocialSecurityNumber;

            var checkSSN = _context.Customers
                .Where(customer => customer.SocialSecurityNumber == bookingCustomerSSN)
                .FirstOrDefault();

            var newCustomer = new Customer();

            if (checkSSN == null)
            {
                newCustomer = AddCustomer(newReservationRequest);

                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();
            }            

            var checkTrainBySeatId = CheckExistingReservation(newReservationRequest);

            if (checkTrainBySeatId)
            {
                return BadRequest("There is already a reservation on the given CNP on the same date and train");
            }

            List<Seat> reservedSeats = AddSeatsToReservation(newReservationRequest);

            if (reservedSeats.Count == 0)
            {
                return BadRequest("No seat is selected");
            }

            var newReservation = new Reservation();

            if (checkSSN == null)
            {
                newReservation = AddReservation(newReservationRequest, reservedSeats, newCustomer);
            }
            else
            {
                newReservation = AddReservation(newReservationRequest, reservedSeats, checkSSN);
            }

            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();

            return Ok(newReservation.Code);
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        private static Customer AddCustomer(NewReservationRequestViewModel newReservationRequest)
        {
            var newCustomer = new Customer
            {
                SocialSecurityNumber = newReservationRequest.SocialSecurityNumber,
                Name = newReservationRequest.Name,
                Email = newReservationRequest.Email
            };

            return newCustomer;
        }

        private bool CheckExistingReservation(NewReservationRequestViewModel newReservationRequest)
        {
            var bookingCustomerSSN = newReservationRequest.SocialSecurityNumber;

            var bookingCustomerDate = newReservationRequest.ReservationWithSeatsViewModel.ReservationDate;

            var currentReservationFirstSeatId = newReservationRequest.ReservationWithSeatsViewModel.ReservedSeatsIds.First();

            var currentReservationSeat = _context.Seats
                .Where(seat => seat.Id == currentReservationFirstSeatId)
                .Include(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .First();

            var currentReservationTrainId = currentReservationSeat.Car.Train.Id;

            var checkOldReservationTrains = _context.Customers
                .Where(customer => customer.SocialSecurityNumber == bookingCustomerSSN)
                .Include(customer => customer.Reservations
                    .Where(reservation => reservation.ReservationDate.Date == bookingCustomerDate.Date))
                .ThenInclude(reservation => reservation.Seats.Where(seat => seat.Id == currentReservationFirstSeatId))
                .ThenInclude(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .First();

            foreach (Reservation reservation in checkOldReservationTrains.Reservations)
            {
                var reservationExists = reservation.Seats.Exists(seat => seat.Car.Train.Id == currentReservationTrainId);

                if (reservationExists)
                {
                    return true;
                }
            }

            return false;
        }

        private List<Seat> AddSeatsToReservation(NewReservationRequestViewModel newReservationRequest)
        {
            List<Seat> reservedSeats = new();

            newReservationRequest.ReservationWithSeatsViewModel.ReservedSeatsIds.ForEach(sid =>
            {
                var seatWithId = _context.Seats.Find(sid);

                if (seatWithId != null)
                {
                    var availableSeat = _context.Seats.Where(seat => seat.Id == sid)
                    .Include(seat => seat.SeatCalendars
                    .Where(seat => seat.Calendar.CalendarDate.Date == newReservationRequest.ReservationWithSeatsViewModel.ReservationDate.Date))
                    .First();

                    foreach (SeatCalendar seat in availableSeat.SeatCalendars)
                    {
                        if (seat.SeatAvailability == false)
                        {
                            reservedSeats.Add(seatWithId);
                            seat.SeatAvailability = true;
                            _context.Entry(seat).State = EntityState.Modified;
                        }
                    }
                }
            });

            return reservedSeats;
        }

        private static Reservation AddReservation(NewReservationRequestViewModel newReservationRequest, List<Seat> reservedSeats, Customer customer)
        {
            Random rd = new();

            var reservation = new Reservation
            {
                Code = rd.Next(10000, 99999).ToString(),
                ReservationDate = newReservationRequest.ReservationWithSeatsViewModel.ReservationDate,
                Seats = reservedSeats,
                Customer = customer
            };

            return reservation;
        }       
    }
}
