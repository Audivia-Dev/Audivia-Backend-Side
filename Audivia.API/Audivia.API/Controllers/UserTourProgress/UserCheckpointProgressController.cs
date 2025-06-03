using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserCheckpointProgress;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserCheckpointProgress
{
    [Route("api/v1/user-checkpoint-progress")]
    [ApiController]
    public class UserCheckpointProgressController : ControllerBase
    {
        private readonly IUserCheckpointProgressService _userCheckpointProgressService;

        public UserCheckpointProgressController(IUserCheckpointProgressService userCheckpointProgressService)
        {
            _userCheckpointProgressService = userCheckpointProgressService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCheckpointProgressRequest request)
        {
            var result = await _userCheckpointProgressService.CreateUserCheckpointProgress(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userCheckpointProgressService.GetAllUserCheckpointProgresss();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userCheckpointProgressService.GetUserCheckpointProgressById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCheckpointProgressRequest request)
        {
            await _userCheckpointProgressService.UpdateUserCheckpointProgress(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userCheckpointProgressService.DeleteUserCheckpointProgress(id);
            return NoContent();
        }
    }
}
