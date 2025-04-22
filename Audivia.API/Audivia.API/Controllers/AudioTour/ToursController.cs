using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.AudioTour;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.AudioTour
{
    [Route("api/v1/tours")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourRequest request)
        {
            var result = await _tourService.CreateAudioTour(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourService.GetAllAudioTours();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tourService.GetAudioTourById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTourRequest request)
        {
            await _tourService.UpdateAudioTour(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _tourService.DeleteAudioTour(id);
            return NoContent();
        }
    }
}
