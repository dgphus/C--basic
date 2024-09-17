using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;

namespace Assignment1_PRN231.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingReportController : ControllerBase
    {
        private readonly IBookingReportService _bookingService;

        public BookingReportController(IBookingReportService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("report")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetBookingReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var report = _bookingService.GetBookingReport(startDate, endDate);
            return Ok(report);
        }
    }
}
