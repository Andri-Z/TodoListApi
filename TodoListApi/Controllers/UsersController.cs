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
        public UsersController(TodoListContext context,JwtServices jwtServices) =>
            (_jwtServices) = (jwtServices);

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            var result = await _jwtServices.Authenticate(request);
            if (result is null)
                return Unauthorized();

            return result;
        }
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseModel?>> Register(RegisterRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _jwtServices.RegisterUser(request);

            return result;
        }
    }
}
