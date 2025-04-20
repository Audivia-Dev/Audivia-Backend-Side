using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.PlaySession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.PlaySession
{
    [Route("api/v1/PlaySessions")]
    [ApiController]
    public class PlaySessionController : ControllerBase
    {
        private readonly IPlaySessionService _playSessionService;

        public PlaySessionController(IPlaySessionService playSessionService)
        {
            _playSessionService = playSessionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _playSessionService.GetAllPlaySessionsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _playSessionService.GetPlaySessionByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlaySessionRequest request) =>
            Ok(await _playSessionService.CreatePlaySessionAsync(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePlaySessionRequest request) =>
            Ok(await _playSessionService.UpdatePlaySessionAsync(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            Ok(await _playSessionService.DeletePlaySessionAsync(id));
    }
}
