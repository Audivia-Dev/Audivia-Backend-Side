using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Group;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Group
{
    [Route("api/v1/Groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var rs = await _groupService.GetAllGroupsAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var rs = await _groupService.CreateGroupAsync(request);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(string id, [FromBody] UpdateGroupRequest request)
        {
            var rs = await _groupService.UpdateGroupAsync(id, request);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            var rs = await _groupService.DeleteGroupAsync(id);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(string id)
        {
            var rs = await _groupService.GetGroupByIdAsync(id);
            return Ok(rs);
        }
    }
}
