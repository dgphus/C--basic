using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Service;
using System.Net;
using System.Security.Claims;

namespace Assignment1_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingReservationsController : ControllerBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        public BookingReservationsController(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
        }

        [HttpGet("getAllBookingReservations")]
        [Authorize(Roles ="Admin")]
        public IActionResult GetAllbookingReservations()
        {
            var bookingReservations = _bookingReservationService.GetAllBookingReservations();
            return Ok(bookingReservations);
        }

        [HttpGet("getBookingReservationByCusomterId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetbookingReservationsByCustomerId(long id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userRole = User.FindFirstValue("Role");

                if (userRole != "Admin" && userId != id.ToString())
                {
                    return Forbid();
                }

                var bookingReservations = _bookingReservationService.GetbookingReservationsByCustomerId(id);

                if (bookingReservations == null || !bookingReservations.Any())
                {
                    return NotFound(new { Message = "Booking reservations not found for the given customer ID." });
                }

                return Ok(bookingReservations);
            }
            catch (Exception ex)
            {
                
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
