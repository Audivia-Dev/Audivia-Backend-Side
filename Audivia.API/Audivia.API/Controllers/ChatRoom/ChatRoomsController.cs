using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatRoom;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.ChatRoom
{
    [Route("api/v1/chat-rooms")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomsController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChatRoomRequest request)
        {
            var result = await _chatRoomService.CreateChatRoom(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _chatRoomService.GetAllChatRooms();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _chatRoomService.GetChatRoomById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateChatRoomRequest request)
        {
            await _chatRoomService.UpdateChatRoom(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _chatRoomService.DeleteChatRoom(id);
            return NoContent();
        }
    }
}
