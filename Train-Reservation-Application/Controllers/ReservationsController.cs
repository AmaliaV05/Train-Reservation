using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Train_Reservation_Application.Interfaces;
using Train_Reservation_Application.ViewModels.Reservations;

namespace Train_Reservation_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IRestReservationsService _reservationService;

        public ReservationsController(IRestReservationsService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPut("{idReservation}")]
        public async Task<IActionResult> PutReservation(int idReservation, ModifyReservationViewModel modifyReservedSeats)
        {
            return Ok(await _reservationService.UpdateReservation(idReservation, modifyReservedSeats));
        }

        [HttpPost]
        public async Task<IActionResult> PostReservation(NewReservationRequest newReservationRequest)
        {
            return Ok(await _reservationService.NewReservation(newReservationRequest));
        }   
    }
}
