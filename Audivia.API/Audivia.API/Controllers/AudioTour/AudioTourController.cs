using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.AudioTour;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.AudioTour
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AudioTourController : ControllerBase
    {
        private readonly IAudioTourService _audioTourService;

        public AudioTourController(IAudioTourService audioTourService)
        {
            _audioTourService = audioTourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAudioTourRequest request)
        {
            var result = await _audioTourService.CreateAudioTour(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _audioTourService.GetAllAudioTours();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _audioTourService.GetAudioTourById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAudioTourRequest request)
        {
            await _audioTourService.UpdateAudioTour(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _audioTourService.DeleteAudioTour(id);
            return NoContent();
        }
    }
}
