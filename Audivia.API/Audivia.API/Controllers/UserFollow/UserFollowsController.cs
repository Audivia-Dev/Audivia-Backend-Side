using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserFollow;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserFollow
{
    [Route("api/v1/user-follows")]
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

        [HttpGet("users")]
        public async Task<IActionResult> GetAllByUserId([FromQuery] GetAllUserFollowersByUserIdRequest request)
        {
            var result = await _userFollowService.GetAllUserFollowsByUserId(request);
            return Ok(result);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriendList([FromQuery] string userId)
        {
            var result = await _userFollowService.GetFriendsList(userId);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] CreateUserFollowRequest request)
        {
            await _userFollowService.DeleteUserFollow(request);
            return NoContent();
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetFollowStatus ([FromQuery] GetFollowRequest request)
        {
            var result = await _userFollowService.GetUserFollowStatus(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userFollowService.GetUserFollowById(id);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userFollowService.DeleteUserFollow(id);
            return NoContent();
        }
    }
}
