using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserAudioTour;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserAudioTour
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAudioToursController : ControllerBase
    {
        private readonly IUserAudioTourService _userAudioTourService;

        public UserAudioToursController(IUserAudioTourService userAudioTourService)
        {
            _userAudioTourService = userAudioTourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserAudioTourRequest request)
        {
            var result = await _userAudioTourService.CreateUserAudioTour(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userAudioTourService.GetAllUserAudioTours();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userAudioTourService.GetUserAudioTourById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserAudioTourRequest request)
        {
            await _userAudioTourService.UpdateUserAudioTour(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userAudioTourService.DeleteUserAudioTour(id);
            return NoContent();
        }
    }
}
