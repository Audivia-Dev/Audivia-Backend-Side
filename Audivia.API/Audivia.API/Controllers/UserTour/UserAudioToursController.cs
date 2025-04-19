using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserAudioTour;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserTour
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAudioToursController : ControllerBase
    {
        private readonly IUserAudioTourService _audioTourService;

        public UserAudioToursController(IUserAudioTourService audioTourService)
        {
            _audioTourService = audioTourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserAudioTourRequest request)
        {
            var result = await _audioTourService.CreateUserAudioTour(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _audioTourService.GetAllUserAudioTours();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _audioTourService.GetUserAudioTourById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserAudioTourRequest request)
        {
            await _audioTourService.UpdateUserAudioTour(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _audioTourService.DeleteUserAudioTour(id);
            return NoContent();
        }
    }
}
