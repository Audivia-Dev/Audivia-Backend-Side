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
            return Content(result, "text/html");
        }

        // login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginWithEmailAndPassword(request);
            return Ok(result);
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Google token is required.");
            }

            try
            {
                // Gọi service để xử lý Google login
                var result = await _authService.LoginWithGoogle(request.Token);
                return Ok(result);
            }
            catch (HttpRequestException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) 
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during Google login.");
            }
        }


        // get current user profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var profile = await _authService.GetCurrentUserAsync(User);
            if (profile == null)
                return Unauthorized(new { Message = "Invalid or expired token." });

            return Ok(profile);
        }

    }
}
