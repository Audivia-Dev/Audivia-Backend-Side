using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Reaction;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Community
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly IReactionService _reactionService;

        public ReactionsController(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReactionRequest request)
        {
            var result = await _reactionService.CreateReaction(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reactionService.GetAllReactions();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _reactionService.GetReactionById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateReactionRequest request)
        {
            await _reactionService.UpdateReaction(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _reactionService.DeleteReaction(id);
            return NoContent();
        }
    }
}
