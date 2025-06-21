using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.User
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateUser(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }
        [HttpGet("admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllMembersByAdmin(int pageIndex = 1, int pageSize = 5)
        {
            var users = await _userService.GetAllMemberAdmin(
                filter: null,
                sortCondition: null,
                top: null,
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] UserUpdateRequest request)
        {
            await _userService.UpdateUser(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
