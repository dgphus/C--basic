using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelA.DTO.Request;
using Service.Interface;
using System.Net;
using System.Security.Claims;

namespace Assignment1_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllCustomers()
        {
            var customers = _userService.GetAllCustomers();
            return Ok(customers);
        }
        [HttpGet("getcustomerById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                var userRole = User.FindFirstValue("Role"); 

                if (userRole != "Admin" && userId != id.ToString())
                {
                    return Forbid();
                }
                var customer = await _userService.GetCustomerById(id);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound("Not found customer.");
            }
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateCustomer(customerRequest);

            if (result.EmailAddress == "EmailAddress already exists.")
            {
                return BadRequest(new { message = "Email address already exists." });
            }

            return Ok(result);

        }

        [HttpPut("updateCustomer/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerRequest customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userRole = User.FindFirstValue("Role");

                if (userRole != "Admin" && userId != id.ToString())
                {
                    return Forbid();
                }
                var result = await _userService.UpdateCustomer(id, customerRequest);
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


        [HttpDelete("DeleteCustomer/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var result = await _userService.DeleteCustomer(id);
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

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public IActionResult SearchCustomers([FromQuery] string keyword)
        {
            var customers = _userService.SearchCustomers(keyword);
            return Ok(customers);
        }
    }
}
