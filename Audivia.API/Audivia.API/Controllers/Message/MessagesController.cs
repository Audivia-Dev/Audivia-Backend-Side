using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Message;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Message
{
    [Route("api/v1/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMessageRequest request)
        {
            var result = await _messageService.CreateMessage(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _messageService.GetAllMessages();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _messageService.GetMessageById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMessageRequest request)
        {
            await _messageService.UpdateMessage(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _messageService.DeleteMessage(id);
            return NoContent();
        }
    }
}
