using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // register
        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] UserCreateRequest request)
        //{
        //    var result = await _userService.CreateUser(request);
        //    return Ok(result);
        //}

        // login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginWithEmailAndPassword(request);
            return Ok(result);
        }

        // get current user profile

    }
}
