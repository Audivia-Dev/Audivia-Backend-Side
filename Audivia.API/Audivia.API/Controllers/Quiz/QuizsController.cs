using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Quiz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Quiz
{
    [Route("api/v1/quizs")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizsController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuiz()
        {
            var rs = await _quizService.GetAllQuizsAsync();
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest req)
        {
            var rs = await _quizService.CreateQuizAsync(req);
            return Ok(rs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(string id)
        {
            var rs = await _quizService.GetQuizByIdAsync(id);
            return Ok(rs);
        }
        [HttpGet("tours/{tourId}")]
        public async Task<IActionResult> GetQuizByTourId(string tourId)
        {
            var rs = await _quizService.GetQuizByTourIdAsync(tourId);
            return Ok(rs);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(string id, UpdateQuizRequest req)
        {
            var rs = await _quizService.UpdateQuizAsync(id, req);
            return Ok(rs);  
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(string id)
        {
            var rs = await _quizService.DeleteQuizByIdAsync(id);
            return Ok(rs);
        }
    }
}
