using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Leaderboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Leaderboard
{
    [Route("api/v1/leaderboards")]
    [ApiController]
    public class LeaderboardsController : ControllerBase
    {
        private readonly ILeaderboardService _service;

        public LeaderboardsController(ILeaderboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaderboards()
        {
            var rs = await _service.GetAllLeaderboardsAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaderboard([FromBody] CreateLeaderboardRequest request)
        {
            var rs = await _service.CreateLeaderboardAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaderboard(string id, [FromBody] UpdateLeaderboardRequest request)
        {
            var rs = await _service.UpdateLeaderboardAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaderboard(string id)
        {
            var rs = await _service.DeleteLeaderboardAsync(id);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaderboardById(string id)
        {
            var rs = await _service.GetLeaderboardByIdAsync(id);
            return Ok(rs);
        }
    }
}
