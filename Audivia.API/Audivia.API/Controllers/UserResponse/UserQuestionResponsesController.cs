using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserQuestionResponse;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserResponse
{
    [Route("api/v1/user-question-responses")]
    [ApiController]
    public class UserQuestionResponsesController : ControllerBase
    {
        private readonly IUserQuestionResponseService _userQuestionResponseService;

        public UserQuestionResponsesController(IUserQuestionResponseService userResponseService)
        {
            _userQuestionResponseService = userResponseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserQuestionResponse([FromBody] CreateUserQuestionResponseRequest request)
        {
            var rs = await _userQuestionResponseService.CreateUserQuestionResponseAsync(request);
            return Ok(rs);
        }
    }
}
