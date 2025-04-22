using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Post;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Post
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var result = await _postService.CreatePost(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _postService.GetAllPosts();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _postService.GetPostById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePostRequest request)
        {
            await _postService.UpdatePost(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }
    }
}
