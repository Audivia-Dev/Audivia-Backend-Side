using Audivia.Application.Services.Implemetation;
using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Audivia.API.Controllers.Payment
{
    [Route("api/v1/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IPayOSService _payOSService;
        public PaymentController(IPaymentService paymentService, IPayOSService payOSService)
        {
            _paymentService = paymentService;
            _payOSService = payOSService;
        }
       // [Authorize]
        [HttpPost("vietqr")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest req)
        {
            var qr = await _paymentService.CreateVietQRTransactionAsync(req);
            return Ok(new { qrCode = qr });
        }

      //  [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook([FromBody] JsonElement payload)
        {
            try
            {
                await _paymentService.ProcessPayOSWebHookAsync(payload);
                return Ok(new { message = "Webhook processed." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("init-webhook")]
        public async Task<IActionResult> InitWebhook()
        {
            await _payOSService.ConfirmWebhookAsync();
            return Ok("Webhook URL registered.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment(string id, int orderCode)
        {
            var rs = await _paymentService.HandlePaymentStatus(id, orderCode);
            return Ok(rs);
        }
    }
}
