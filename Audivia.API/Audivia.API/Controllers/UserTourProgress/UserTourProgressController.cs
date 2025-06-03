using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserTourProgress;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserTourProgress
{
    [Route("api/v1/user-tour-progress")]
    [ApiController]
    public class UserTourProgressController : ControllerBase
    {
        private readonly IUserTourProgressService _userTourProgressService;

        public UserTourProgressController(IUserTourProgressService userTourProgressService)
        {
            _userTourProgressService = userTourProgressService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserTourProgressRequest request)
        {
            var result = await _userTourProgressService.CreateUserTourProgress(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userTourProgressService.GetAllUserTourProgresss();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userTourProgressService.GetUserTourProgressById(id);
            return Ok(result);
        }

        [HttpGet("users/{userId}/tours/{tourId}")]
        public async Task<IActionResult> GetByUserIdAndTourId(string userId, string tourId)
        {
            var result = await _userTourProgressService.GetUserTourProgressByUserIdAndTourId(userId, tourId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserTourProgressRequest request)
        {
            await _userTourProgressService.UpdateUserTourProgress(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userTourProgressService.DeleteUserTourProgress(id);
            return NoContent();
        }
    }
}
