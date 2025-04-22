using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.TourType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.TourType
{
    [Route("api/v1/tour-types")]
    [ApiController]
    public class TourTypesController : ControllerBase
    {
        private readonly ITourTypeService _tourTypeService;

        public TourTypesController(ITourTypeService tourTypeService)
        {
            _tourTypeService = tourTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourTypeRequest request)
        {
            var result = await _tourTypeService.CreateTourType(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourTypeService.GetAllActiveTourTypes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tourTypeService.GetTourTypeInformation(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTourTypeRequest request)
        {
            await _tourTypeService.UpdateTourType(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _tourTypeService.DeleteTourType(id);
            return NoContent();
        }
    }
}
