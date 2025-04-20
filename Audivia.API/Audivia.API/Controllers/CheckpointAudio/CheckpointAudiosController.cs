using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.CheckpointAudio;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.CheckpointAudio
{
    public class CheckpointAudiosController
    {
        [Route("api/v1/[controller]")]
        [ApiController]
        public class CheckpointAudioController : ControllerBase
        {
            private readonly ICheckpointAudioService _checkpointAudioService;

            public CheckpointAudioController(ICheckpointAudioService checkpointAudioService)
            {
                _checkpointAudioService = checkpointAudioService;
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateCheckpointAudioRequest request)
            {
                var result = await _checkpointAudioService.CreateCheckpointAudio(request);
                return Ok(result);
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var result = await _checkpointAudioService.GetAllCheckpointAudios();
                return Ok(result);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(string id)
            {
                var result = await _checkpointAudioService.GetCheckpointAudioById(id);
                return Ok(result);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(string id, [FromBody] UpdateCheckpointAudioRequest request)
            {
                await _checkpointAudioService.UpdateCheckpointAudio(id, request);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(string id)
            {
                await _checkpointAudioService.DeleteCheckpointAudio(id);
                return NoContent();
            }
        }
    }
}
