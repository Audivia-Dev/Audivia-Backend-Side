using Audivia.API.Hubs;
using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Audivia.API.Controllers.Message
{
    [Route("api/v1/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMessageRequest request)
        {
            var result = await _messageService.CreateMessage(request);
            await _hubContext.Clients.Group(request.ChatRoomId)
               .SendAsync("ReceiveMessage", result);
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
            var message = await _messageService.UpdateMessage(id, request);
            try
            {
                await _hubContext.Clients.Group(message.ChatRoomId).SendAsync("UpdateMessage", message);
            }
            catch (Exception ex)
            {

            }
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var message = await _messageService.DeleteMessage(id);
            try
            {
                await _hubContext.Clients.Group(message.ChatRoomId)
                    .SendAsync("DeleteMessage", message);
            }
            catch {  }
            return NoContent();
        }

        [HttpGet("chatroom/{id}")]
        public async Task<IActionResult> GetMessagesByChatRoomId(string id)
        {
            var rs = await _messageService.GetMessagesByChatRoomId(id);
            return Ok(rs);
        }
    }
}
