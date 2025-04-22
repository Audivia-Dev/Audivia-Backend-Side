using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.UserVoucher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Voucher
{
    [Route("api/v1/user-vouchers")]
    [ApiController]
    public class UserVouchersController : ControllerBase
    {
        private readonly IUserVoucherService _userVoucherService;

        public UserVouchersController(IUserVoucherService userVoucherService)
        {
            _userVoucherService = userVoucherService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserVoucher([FromBody] CreateUserVoucherRequest request)
        {
            var result = await _userVoucherService.CreateUserVoucherAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserVoucher(string id, [FromBody] UpdateUserVoucherRequest request)
        {
            var result = await _userVoucherService.UpdateUserVoucherAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserVoucher(string id)
        {
            var result = await _userVoucherService.DeleteUserVoucherAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserVouchers()
        {
            var result = await _userVoucherService.GetAllUserVoucherAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserVoucherById(string id)
        {
            var result = await _userVoucherService.GetUserVoucherByIdAsync(id);
            return Ok(result);
        }
    }
}
