using Microsoft.AspNetCore.Mvc;
using ModelA.DTO.Request;
using Service.Interface;
using System.Net;

namespace Assignment1_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _userService.AuthorizeUser(loginRequest);
            if (result.Token != null)
            {
                return Ok(new { result.Token, LoginResponse = result.loginResponse });
            }
            else
            {
                return Ok(HttpStatusCode.Unauthorized);
            }
        }
    }
}
