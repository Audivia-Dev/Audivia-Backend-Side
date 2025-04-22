using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.CheckpointImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.CheckpointImage
{
    [Route("api/v1/checkpoint-images")]
    [ApiController]
    public class CheckpointImagesController : ControllerBase
    {
        private readonly ICheckpointImageService _service;

        public CheckpointImagesController(ICheckpointImageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCheckpointImages()
        {
            var rs = await _service.GetAllCheckpointImagesAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckpointImage([FromBody] CreateCheckpointImageRequest request)
        {
            var rs = await _service.CreateCheckpointImageAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckpointImage(string id, [FromBody] UpdateCheckpointImageRequest request)
        {
            var rs = await _service.UpdateCheckpointImageAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckpointImage(string id)
        {
            var rs = await _service.DeleteCheckpointImageAsync(id);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckpointImageById(string id)
        {
            var rs = await _service.GetCheckpointImageByIdAsync(id);
            return Ok(rs);
        }
    }
}
