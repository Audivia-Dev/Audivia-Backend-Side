using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Question;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Question
{
    [Route("api/v1/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuestion()
        {
            var rs = await _questionService.GetAllQuestionsAsync();
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest req)
        {
            var rs = await _questionService.CreateQuestionAsync(req);
            return Ok(rs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var rs = await _questionService.GetQuestionByIdAsync(id);
            return Ok(rs);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, UpdateQuestionRequest req)
        {
            var rs = await _questionService.UpdateQuestionAsync(id, req);
            return Ok(rs);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var rs = await _questionService.DeleteQuestionAsync(id);
            return Ok(rs);
        }
    }
}
