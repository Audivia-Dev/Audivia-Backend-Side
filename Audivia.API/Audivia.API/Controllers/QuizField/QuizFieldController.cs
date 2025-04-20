using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.QuizField;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.QuizField
{
    [Route("api/v1/QuizFields")]
    [ApiController]
    public class QuizFieldController : ControllerBase
    {
        private IQuizFieldService _quizFieldService;
        public QuizFieldController(IQuizFieldService quizFieldService)
        {
            _quizFieldService = quizFieldService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuizField()
        {
            var rs = await _quizFieldService.GetQuizFieldListAsync();
            return Ok(rs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizFieldById(string id)
        {
            var rs = await _quizFieldService.GetQuizFieldByIdAsync(id);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuizField([FromBody] CreateQuizFieldRequest request)
        {
            var rs = await _quizFieldService.CreateQuizFieldAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuizField(string id, [FromBody] UpdateQuizFieldRequest request)
        {
            var rs = await _quizFieldService.UpdateQuizFieldAsync(id, request);
            return Ok(rs);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizField(string id)
        {
            var rs = await _quizFieldService.DeleteQuizFieldAsync(id);
            return Ok(rs);
        }
    }
}
