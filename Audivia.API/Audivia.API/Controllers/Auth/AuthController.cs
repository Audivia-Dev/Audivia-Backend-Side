using Audivia.Application.Services.Implemetation;
using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Auth;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Audivia.API.Controllers.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request);
            return Ok(result);
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] ConfirmEmailRequest request)
        {
            var result = await _authService.VerifyEmail(request);
            return Ok(result);
        }

        // login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginWithEmailAndPassword(request);
            return Ok(result);
        }

        // get current user profile

    }
}
