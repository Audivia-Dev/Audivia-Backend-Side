using Audivia.Application.Services.Implemetation;
using Audivia.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Audivia.API.Controllers.Payment
{
    [Route("webhook-url")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        private readonly IPayOSService _payOSService;
        public WebHookController(IPaymentService paymentService, IPayOSService payOSService)
        {
            _paymentService = paymentService; 
            _payOSService = payOSService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> HandleWebhook([FromBody] JsonElement payload)
        {
            try
            {
                if (!_payOSService.VerifyBankWebhook(payload))
                {
                    return BadRequest(new { error = "Invalid signature" });
                }

                await _paymentService.ProcessPayOSWebHookAsync(payload);
                return Ok(new { message = "Webhook processed." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
