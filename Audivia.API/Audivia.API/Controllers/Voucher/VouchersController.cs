using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Voucher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Voucher
{
    [Route("api/v1/vouchers")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VouchersController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucherRequest request)
        {
            var result = await _voucherService.CreateVoucherAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoucher(string id, [FromBody] UpdateVoucherRequest request)
        {
            var result = await _voucherService.UpdateVoucherAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(string id)
        {
            var result = await _voucherService.DeleteVoucherAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVouchers()
        {
            var result = await _voucherService.GetAllVoucherAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucherById(string id)
        {
            var result = await _voucherService.GetVoucherByIdAsync(id);
            return Ok(result);
        }
    }
}
