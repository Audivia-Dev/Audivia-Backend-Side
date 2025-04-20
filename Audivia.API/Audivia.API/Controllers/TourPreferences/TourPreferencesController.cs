using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.TourPreference;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.TourPreferences
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TourPreferencesController : ControllerBase
    {
        private readonly ITourPreferenceService _tourPreferenceService;

        public TourPreferencesController(ITourPreferenceService tourPreferenceService)
        {
            _tourPreferenceService = tourPreferenceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourPreferenceRequest request)
        {
            var result = await _tourPreferenceService.CreateTourPreference(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourPreferenceService.GetAllTourPreferences();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tourPreferenceService.GetTourPreferenceById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTourPreferenceRequest request)
        {
            await _tourPreferenceService.UpdateTourPreference(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _tourPreferenceService.DeleteTourPreference(id);
            return NoContent();
        }
    }
}
