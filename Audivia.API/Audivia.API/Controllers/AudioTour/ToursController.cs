using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.AudioTour;
using Audivia.Domain.ModelRequests.Tour;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateTourRequest request)
        {
            var result = await _tourService.CreateAudioTour(request);
            return Ok(result);
        }

        // them model request cho get all request (filter, sort, top, page size, page num) va pagination response
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetToursRequest request)
        {
            var result = await _tourService.GetAllAudioTours(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tourService.GetAudioTourById(id);
            return Ok(result);
        }

        [HttpGet("suggested")]
        public async Task<IActionResult> GetSuggestedTours([FromQuery] GetSuggestedTourRequest request)
        {
            var result = await _tourService.GetSuggestedTour(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTourRequest request)
        {
            await _tourService.UpdateAudioTour(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _tourService.DeleteAudioTour(id);
            return NoContent();
        }
    }
}
