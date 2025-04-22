using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.SavedTour;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.SavedTour
{
    [Route("api/v1/save-tours")]
    [ApiController]
    public class SavedToursController : ControllerBase
    {
        private readonly ISavedTourService _savedTourService;

        public SavedToursController(ISavedTourService savedTourService)
        {
            _savedTourService = savedTourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSavedTourRequest request)
        {
            var result = await _savedTourService.CreateSavedTour(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _savedTourService.GetAllSavedTours();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _savedTourService.GetSavedTourById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSavedTourRequest request)
        {
            await _savedTourService.UpdateSavedTour(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _savedTourService.DeleteSavedTour(id);
            return NoContent();
        }
    }
}
