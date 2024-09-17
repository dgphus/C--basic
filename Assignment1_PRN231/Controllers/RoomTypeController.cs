using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Assignment1_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet("getAllRoomType")]
        public IActionResult GetAllRoomType()
        {
            var customers = _roomTypeService.GetAllRoomType();
            return Ok(customers);
        }
        [HttpGet("getAllRoomType/{id}")]
        public async Task<IActionResult> GetRoomTypeById(int id)
        {
            try
            {
                var customer = await _roomTypeService.GetRoomTypeById(id);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound("Not found room.");
            }
        }
    }
}
