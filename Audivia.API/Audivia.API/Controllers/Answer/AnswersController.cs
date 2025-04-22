using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Answer;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Answer
{
    [Route("api/v1/answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswersController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswer()
        {
            var rs = await _answerService.GetAllAnswer();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerRequest request)
        {
            var rs = await _answerService.CreateAnswerAsync(request);
            return Ok(rs);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(string id, [FromBody] UpdateAnswerRequest request)
        {
            var rs = await _answerService.UpdateAnswerAsync(id, request);
            return Ok(rs);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(string id)
        {
            var rs = await _answerService.DeleteAnswerAsync(id);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var rs = await _answerService.GetAnswerByIdAsync(id);
            return Ok(rs);
        }
    }
}
