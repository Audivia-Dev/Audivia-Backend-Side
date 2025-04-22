using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.PlayResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.PlayResult
{
    [Route("api/v1/play-results")]
    [ApiController]
    public class PlayResultsController : ControllerBase
    {
        private readonly IPlayResultService _playResultService;

        public PlayResultsController(IPlayResultService playResultService)
        {
            _playResultService = playResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _playResultService.GetAllPlayResultsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _playResultService.GetPlayResultByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlayResultRequest request) =>
            Ok(await _playResultService.CreatePlayResultAsync(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePlayResultRequest request) =>
            Ok(await _playResultService.UpdatePlayResultAsync(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            Ok(await _playResultService.DeletePlayResultAsync(id));
    }
}
