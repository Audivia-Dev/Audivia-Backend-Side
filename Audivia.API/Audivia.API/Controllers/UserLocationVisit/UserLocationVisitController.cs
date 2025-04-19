using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserLocationVisit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserLocationVisit
{
    [Route("api/v1/UserLocationVisits")]
    [ApiController]
    public class UserLocationVisitController : ControllerBase
    {
        private readonly IUserLocationVisitService _userLocationVisitService;

        public UserLocationVisitController(IUserLocationVisitService userLocationVisitService)
        {
            _userLocationVisitService = userLocationVisitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserLocationVisits()
        {
            var rs = await _userLocationVisitService.GetAllUserLocationVisitAsync();
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserLocationVisitById(string id)
        {
            var rs = await _userLocationVisitService.GetUserLocationVisitByIdAsync(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserLocationVisit([FromBody] CreateUserLocationVisitRequest request)
        {
            var rs = await _userLocationVisitService.CreateUserLocationVisitAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserLocationVisit(string id, [FromBody] UpdateUserLocationVisitRequest request)
        {
            var rs = await _userLocationVisitService.UpdateUserLocationVisitAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLocationVisit(string id)
        {
            var rs = await _userLocationVisitService.DeleteUserLocationVisitAsync(id);
            return Ok(rs);
        }
    }
}
