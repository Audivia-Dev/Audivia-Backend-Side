using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatBot;
using Audivia.Domain.ModelResponses.ChatBot;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Chatbot
{
    [Route("api/chat-bot")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [HttpPost("send")]
        public async Task<ActionResult<MessageResponse>> SendMessage([FromBody] MessageRequest request)
        {
            if (string.IsNullOrEmpty(request.Text) || string.IsNullOrEmpty(request.SessionId))
            {
                return BadRequest("Text and SessionId are required.");
            }

            try
            {
                // Gọi phương thức từ service đã được inject
                var response = await _chatBotService.DetectIntentAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
