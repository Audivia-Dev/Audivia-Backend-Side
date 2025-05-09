using Audivia.API.Hubs;
using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatRoomMember;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Audivia.API.Controllers.ChatRoomMember
{
    [Route("api/v1/chat-room-members")]
    [ApiController]
    public class ChatRoomMembersController : ControllerBase
    {
        private readonly IChatRoomMemberService _chatRoomMemberService;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatRoomMembersController(IChatRoomMemberService chatRoomMemberService, IHubContext<ChatHub> hubContext)
        {
            _chatRoomMemberService = chatRoomMemberService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChatRoomMemberRequest request)
        {
            var result = await _chatRoomMemberService.CreateChatRoomMember(request);
            await _hubContext.Clients.Group(request.ChatRoomId)
            .SendAsync("UserJoined", request);
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
            await _hubContext.Clients.Group(id).SendAsync("UserUpdated", request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var member = await _chatRoomMemberService.GetMemberById(id);
            if (member is null)
            {
                return NotFound();
            }
            await _chatRoomMemberService.DeleteChatRoomMember(id);
            await _hubContext.Clients.Group(member.ChatRoomId).SendAsync("UserLeft", member
                );
            return NoContent();
        }
    }
}
