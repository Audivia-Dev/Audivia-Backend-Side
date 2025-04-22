using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatRoomMember;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.ChatRoomMember
{
    [Route("api/v1/chat-room-members")]
    [ApiController]
    public class ChatRoomMembersController : ControllerBase
    {
        private readonly IChatRoomMemberService _chatRoomMemberService;

        public ChatRoomMembersController(IChatRoomMemberService chatRoomMemberService)
        {
            _chatRoomMemberService = chatRoomMemberService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChatRoomMemberRequest request)
        {
            var result = await _chatRoomMemberService.CreateChatRoomMember(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _chatRoomMemberService.GetAllChatRoomMembers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _chatRoomMemberService.GetChatRoomMemberById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateChatRoomMemberRequest request)
        {
            await _chatRoomMemberService.UpdateChatRoomMember(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _chatRoomMemberService.DeleteChatRoomMember(id);
            return NoContent();
        }
    }
}
