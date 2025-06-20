using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatBot;
using Audivia.Domain.ModelResponses.ChatBot;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Audivia.Domain.Models.ChatBotHistory;
using System.Collections.Generic;

namespace Audivia.API.Controllers.Chatbot
{
    [Route("api/v1/chat-bot")]
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
            var response = await _chatBotService.DetectIntentAsync(request);
            return Ok(response);
        }

        [HttpGet("history/{clientSessionId}")]
        public async Task<ActionResult<IEnumerable<ChatBotMessage>>> GetHistory(
            string clientSessionId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var messages = await _chatBotService.GetChatHistoryAsync(clientSessionId, pageNumber, pageSize);
            return Ok(messages);
        }
    }
}
