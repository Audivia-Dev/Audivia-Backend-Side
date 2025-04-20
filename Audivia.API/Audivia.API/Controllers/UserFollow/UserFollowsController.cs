using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserFollow;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserFollow
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserFollowsController : ControllerBase
    {
        private readonly IUserFollowService _userFollowService;

        public UserFollowsController(IUserFollowService userFollowService)
        {
            _userFollowService = userFollowService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserFollowRequest request)
        {
            var result = await _userFollowService.CreateUserFollow(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userFollowService.GetAllUserFollows();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userFollowService.GetUserFollowById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserFollowRequest request)
        {
            await _userFollowService.UpdateUserFollow(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userFollowService.DeleteUserFollow(id);
            return NoContent();
        }
    }
}
