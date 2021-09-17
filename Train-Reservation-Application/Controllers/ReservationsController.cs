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

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(NewReservationRequestViewModel newReservationRequest)
        {    
            var bookingCustomerSSN = newReservationRequest.SocialSecurityNumber;       

            var checkSSN = _context.Customers
                .Where(customer => customer.SocialSecurityNumber == bookingCustomerSSN)
                .FirstOrDefault();

            var oldReservedSeats = _context.Customers
                .Where(customer => customer.SocialSecurityNumber == "444444")
                .Include(customer => customer.Reservations
                    .Where(reservation => reservation.ReservationDate.Date == new DateTime(2021, 09, 15, 00, 00, 00).Date))
                .ThenInclude(reservation => reservation.Seats)
                .ThenInclude(seat => seat.Car)
                .ThenInclude(car => car.Train)
                .AsSplitQuery()
                .Select(p => _mapper.Map<OldCustomerReservationViewModel>(p))
                .ToList();

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

            if (reservedSeats.Count == 0)
            {
                return BadRequest();
            }

            //var reservedTrainId = 

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
