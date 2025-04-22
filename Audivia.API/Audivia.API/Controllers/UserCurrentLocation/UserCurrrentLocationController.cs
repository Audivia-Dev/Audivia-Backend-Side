using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserCurrentLocation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserCurrentLocation
{
    [Route("api/v1/user-current-locations")]
    [ApiController]
    public class UserCurrentLocationsController : ControllerBase
    {
        private readonly IUserCurrentLocationService _userCurrentLocationService;

        public UserCurrentLocationsController(IUserCurrentLocationService userCurrentLocationService)
        {
            _userCurrentLocationService = userCurrentLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserCurrentLocations()
        {
            var result = await _userCurrentLocationService.GetAllUserCurrentLocationAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCurrentLocationById(string id)
        {
            var result = await _userCurrentLocationService.GetUserCurrentLocationByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserCurrentLocation([FromBody] CreateUserCurrentLocationRequest request)
        {
            var result = await _userCurrentLocationService.CreateUserCurrentLocationAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserCurrentLocation(string id, [FromBody] UpdateUserCurrentLocationRequest request)
        {
            var result = await _userCurrentLocationService.UpdateUserCurrentLocationAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCurrentLocation(string id)
        {
            var result = await _userCurrentLocationService.DeleteUserCurrentLocationAsync(id);
            return Ok(result);
        }
    }
}
