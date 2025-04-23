using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.GroupMember;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Group
{
    [Route("api/v1/group-members")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly IGroupMemberService _groupMemberService;

        public GroupMembersController(IGroupMemberService groupMemberService)
        {
            _groupMemberService = groupMemberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _groupMemberService.GetAllGroupMembersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _groupMemberService.GetGroupMemberByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupMemberRequest request) =>
            Ok(await _groupMemberService.CreateGroupMemberAsync(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateGroupMemberRequest request) =>
            Ok(await _groupMemberService.UpdateGroupMemberAsync(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            Ok(await _groupMemberService.DeleteGroupMemberAsync(id));
    }
}
