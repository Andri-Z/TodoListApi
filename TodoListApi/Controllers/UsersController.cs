using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Context;
using TodoListApi.Models.Jwt;
using TodoListApi.Services;

namespace TodoListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly JwtServices _jwtServices;
        public UsersController(JwtServices jwtServices) =>
            (_jwtServices) = (jwtServices);

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromQuery] LoginRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "El modelo de datos es invalido." });

            var result = await _jwtServices.Authenticate(request);
            if (result is null)
                return Unauthorized();

            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseModel?>> Register([FromQuery] RegisterRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "El modelo de datos es invalido."});

            var result = await _jwtServices.RegisterUser(request);

            if (result is null)
                return BadRequest(new { mensaje = "Ingrese datos validos." });

            return Ok(result);
        }
    }
}
