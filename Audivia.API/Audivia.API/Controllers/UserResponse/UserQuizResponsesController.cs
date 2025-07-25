using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserQuizResponse;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserResponse
{
    [Route("api/v1/user-quiz-responses")]
    [ApiController]
    public class UserQuizResponsesController : ControllerBase
    {
        private readonly IUserQuizResponseService _userQuizResponseService;

        public UserQuizResponsesController(IUserQuizResponseService userResponseService)
        {
            _userQuizResponseService = userResponseService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserResponse([FromBody] CreateUserQuizResponseRequest request)
        {
            var rs = await _userQuizResponseService.CreateUserQuizResponseAsync(request);
            return Ok(rs);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUserResponses()
        {
            var rs = await _userQuizResponseService.GetAllUserQuizResponseAsync();
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserResponseById(string id)
        {
            var rs = await _userQuizResponseService.GetUserQuizResponseByIdAsync(id);
            return Ok(rs);
        }

        [HttpGet("quizs/{quizId}/users/{userId}")]
        public async Task<IActionResult> GetUserResponseById(string quizId, string userId)
        {
            var rs = await _userQuizResponseService.GetUserQuizResponseByQuizIdAndUserIdAsync(quizId, userId);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserResponse(string id, [FromBody] UpdateUserQuizResponseRequest request)
        {
            var rs = await _userQuizResponseService.UpdateUserQuizResponseAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserResponse(string id)
        {
            var rs = await _userQuizResponseService.DeleteUserQuizResponseAsync(id);
            return Ok(rs);
        }
    }
}
