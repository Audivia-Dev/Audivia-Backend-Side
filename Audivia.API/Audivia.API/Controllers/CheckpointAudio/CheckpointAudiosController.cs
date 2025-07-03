using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.CheckpointAudio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.CheckpointAudio
{
    [Route("api/v1/checkpoint-audios")]
    [ApiController]
    public class CheckpointAudiosController : ControllerBase
    {
        private readonly ICheckpointAudioService _checkpointAudioService;

        public CheckpointAudiosController(ICheckpointAudioService checkpointAudioService)
        {
            _checkpointAudioService = checkpointAudioService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
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

        [HttpGet("tour")]
        public async Task<IActionResult> HasCheckpointAudioForTour([FromQuery] string tour)
        {
            var result = await _checkpointAudioService.HasCheckpointAudioForTour(tour);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _checkpointAudioService.GetCheckpointAudioById(id);
            return Ok(result);
        }

        [HttpGet("checkpoint/{checkpointId}/character/{characterId}")]
        public async Task<IActionResult> GetByTourCheckpointId(string checkpointId, string characterId)
        {
            var result = await _checkpointAudioService.GetCheckpointAudioByTourCheckpointId(checkpointId, characterId);
            return Ok(result);
        }
        [HttpGet("next/{checkpointId}")]
        public async Task<IActionResult> GetNextAudioByTourCheckpointId(string checkpointId)
        {
            var result = await _checkpointAudioService.GetNextAudioByTourCheckpointId(checkpointId);
            return Ok(result);
        }
        [HttpGet("prev/{checkpointId}")]
        public async Task<IActionResult> GetPrevAudioByTourCheckpointId(string checkpointId)
        {
            var result = await _checkpointAudioService.GetPrevAudioAudioByTourCheckpointId(checkpointId);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCheckpointAudioRequest request)
        {
            await _checkpointAudioService.UpdateCheckpointAudio(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _checkpointAudioService.DeleteCheckpointAudio(id);
            return NoContent();
        }
    }
}
