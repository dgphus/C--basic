using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelA.DTO.Request;
using Service.Interface;
using Service.Service;

namespace Assignment1_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomInformationsController : ControllerBase
    {
        private readonly IRoomInformationsService _roomInformationsService;

        public RoomInformationsController(IRoomInformationsService roomInformationsService)
        {
            _roomInformationsService = roomInformationsService;
        }

        [HttpGet("getAllRoomInformation")]
        public IActionResult GetAllRoom()
        {
            var customers = _roomInformationsService.GetAllRoom();
            return Ok(customers);
        }
        [HttpGet("getRoomInformationById/{id}")]
        public async Task<IActionResult> GetRoomInformationById(int id)
        {
            try
            {
                var customer = await _roomInformationsService.GetRoomInformationById(id);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound("Not found room.");
            }
        }

        [HttpGet("getRoomInformationByRoomTypeId/{id}")]
        public async Task<IActionResult> GetRoomInformationByRoomTypeId(int id)
        {
            try
            {
                var customer = await _roomInformationsService.GetRoomInformationByRoomTypeId(id);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound("Not found room.");
            }
        }

        [HttpPost("createRoom")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomInformationsRequest roomInformationsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roomInformationsService.CreateRoom(roomInformationsRequest);


            return Ok(result);

        }

        [HttpPut("updateRoom/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomInformationsRequest roomInformationsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _roomInformationsService.UpdateRoom(id, roomInformationsRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Message = "An error occurred while updating the customer.",
                    Details = ex.Message
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpDelete("deleteRoom/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var result = await _roomInformationsService.DeleteRoom(id);
                if (result)
                {
                    return Ok("Delete Successful");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("sortedByPrice")]
        public IActionResult GetRoomsSortedByPrice([FromQuery] bool ascendingOrder = true)
        {
            try
            {
                var rooms = _roomInformationsService.GetRoomsSortedByPrice(ascendingOrder);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
