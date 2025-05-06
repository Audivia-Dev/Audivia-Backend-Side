using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.TourCheckpoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.TourCheckpoint
{
    [Route("api/v1/tour-checkpoints")]
    [ApiController]
    public class TourCheckpointsController : ControllerBase
    {
        private readonly ITourCheckpointService _service;

        public TourCheckpointsController(ITourCheckpointService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTourCheckpoints()
        {
            var rs = await _service.GetAllTourCheckpointsAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTourCheckpoint([FromBody] CreateTourCheckpointRequest request)
        {
            var rs = await _service.CreateTourCheckpointAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTourCheckpoint(string id, [FromBody] UpdateTourCheckpointRequest request)
        {
            var rs = await _service.UpdateTourCheckpointAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourCheckpoint(string id)
        {
            var rs = await _service.DeleteTourCheckpointAsync(id);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourCheckpointById(string id)
        {
            var rs = await _service.GetTourCheckpointByIdAsync(id);
            return Ok(rs);
        }

        [HttpGet("tourId/{tourId}")]
        public async Task<IActionResult> GetTourCheckpointByTourId(string tourId)
        {
            var rs = await _service.GetTourCheckpointsByTourId(tourId);
            return Ok(rs);
        }
    }
}
