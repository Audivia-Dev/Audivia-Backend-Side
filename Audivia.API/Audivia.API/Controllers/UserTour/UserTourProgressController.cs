using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserTourProgress;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.UserTour
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserTourProgressController : ControllerBase
    {
        private readonly IUserTourProgressService _audioTourService;

        public UserTourProgressController(IUserTourProgressService audioTourService)
        {
            _audioTourService = audioTourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserTourProgressRequest request)
        {
            var result = await _audioTourService.CreateUserTourProgress(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _audioTourService.GetAllUserTourProgresss();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _audioTourService.GetUserTourProgressById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserTourProgressRequest request)
        {
            await _audioTourService.UpdateUserTourProgress(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _audioTourService.DeleteUserTourProgress(id);
            return NoContent();
        }
    }
}
