using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Comment
{
    [Route("api/v1/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
        {
            var result = await _commentService.CreateComment(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _commentService.GetAllComments();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _commentService.GetCommentById(id);
            return Ok(result);
        }

        [HttpGet("posts/{postId}")]
        public async Task<IActionResult> GetByPostId(string postId)
        {
            var result = await _commentService.GetByPost(postId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCommentRequest request)
        {
            await _commentService.UpdateComment(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            await _commentService.DeleteComment(id, userId);
            return NoContent();
        }
    }
}
