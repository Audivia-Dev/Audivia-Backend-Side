using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.AudioCharacter;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.AudioCharacter
{
    [Route("api/v1/audio-characters")]
    [ApiController]
    public class AudioCharactersController : ControllerBase
    {
        private readonly IAudioCharacterService _audioCharacterService;

        public AudioCharactersController(IAudioCharacterService audioCharacterService)
        {
            _audioCharacterService = audioCharacterService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAudioCharacterRequest request)
        {
            var result = await _audioCharacterService.CreateAudioCharacter(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _audioCharacterService.GetAllAudioCharacters();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _audioCharacterService.GetAudioCharacterById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAudioCharacterRequest request)
        {
            await _audioCharacterService.UpdateAudioCharacter(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _audioCharacterService.DeleteAudioCharacter(id);
            return NoContent();
        }
    }
}
