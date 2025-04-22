using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserResponse
{
    [Route("api/v1/user-responses")]
    [ApiController]
    public class UserResponsesController : ControllerBase
    {
        private readonly IUserResponseService _userResponseService;

        public UserResponsesController(IUserResponseService userResponseService)
        {
            _userResponseService = userResponseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserResponses()
        {
            var rs = await _userResponseService.GetAllUserResponseAsync();
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserResponseById(string id)
        {
            var rs = await _userResponseService.GetUserResponseByIdAsync(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserResponse([FromBody] CreateUserResponseRequest request)
        {
            var rs = await _userResponseService.CreateUserResponseAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserResponse(string id, [FromBody] UpdateUserResponseRequest request)
        {
            var rs = await _userResponseService.UpdateUserResponseAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserResponse(string id)
        {
            var rs = await _userResponseService.DeleteUserResponseAsync(id);
            return Ok(rs);
        }
    }
}
