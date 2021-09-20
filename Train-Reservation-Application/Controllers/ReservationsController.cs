/*using System;
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
            var oldReservedSeats = _context.Reservations
                .Where(reservation => reservation.Id == idReservation)
                .Include(reservation => reservation.Seats)
                .FirstOrDefault();

            if (oldReservedSeats.Code != modifyReservedSeats.Code)
            {
                return BadRequest("Incorrect code");
            }

            if (modifyReservedSeats.ReservedSeatsIds.Count == 0)
            {
                return BadRequest("No seat is selected");
            }

            var seatsLocation = _context.Reservations
                .Where(reservation => reservation.Id == idReservation)
                .Include(reservation => reservation.Seats)
                .ToList();

            foreach (Reservation reservation in seatsLocation)
            {
                foreach(Seat seat in reservation.Seats)
                {
                    seat.Available = true;
                    _context.Entry(seat).State = EntityState.Modified;
                }
            }

            modifyReservedSeats.ReservedSeatsIds.ForEach(sid =>
            {
                var seatWithId = _context.Seats.Find(sid);
                if (seatWithId != null && seatWithId.Available == true)
                {
                    seatWithId.Available = false;
                    _context.Entry(seatWithId).State = EntityState.Modified;
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
                newCustomer = new Customer
                {
                    SocialSecurityNumber = newReservationRequest.SocialSecurityNumber,
                    Name = newReservationRequest.Name,
                    Email = newReservationRequest.Email
                };

                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();
            }
            //----------------
            var bookingCustomerDate = newReservationRequest.ReservationWithSeatsViewModel.ReservationDate;

            var oldReservedSeats = _context.Customers
                .Where(customer => customer.SocialSecurityNumber == bookingCustomerSSN)
                .Include(customer => customer.Reservations
                    .Where(reservation => reservation.ReservationDate.Date == bookingCustomerDate))
                .ThenInclude(reservation => reservation.Seats)
                .ThenInclude(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .Select(p => _mapper.Map<OldCustomerReservationViewModel>(p))
                .ToList();//aflu lista id tren de la rezervarile deja facute



            //-------
            List<Seat> reservedSeats = AddSeatsToReservation(newReservationRequest);

            if (reservedSeats.Count == 0)
            {
                return BadRequest("No seat is selected");
            }

            var reservedTrainId = _context.Seats
                .Include(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .Select(c => _mapper.Map<SeatsInCarViewModel>(c));//aflu id tren de la rezervarea curenta

            var reservation = new Reservation();

            if (checkSSN == null)
            {
                reservation = AddReservation(newReservationRequest, reservedSeats, newCustomer);
            }
            else
            {
                reservation = AddReservation(newReservationRequest, reservedSeats, checkSSN);
            }

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(reservation.Code);
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        private List<Seat> AddSeatsToReservation(NewReservationRequestViewModel newReservationRequest)
        {
            List<Seat> reservedSeats = new();

            newReservationRequest.ReservationWithSeatsViewModel.ReservedSeatsIds.ForEach(sid =>
            {
                var seatWithId = _context.Seats.Find(sid);
                if (seatWithId != null && seatWithId.Available == true)
                {
                    reservedSeats.Add(seatWithId);
                    seatWithId.Available = false;
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
*/