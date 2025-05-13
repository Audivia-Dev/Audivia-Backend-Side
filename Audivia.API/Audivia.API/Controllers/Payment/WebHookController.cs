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
        public WebHookController(IPaymentService paymentService)
        {
            _paymentService = paymentService;   
        }
        [HttpPost]
        [AllowAnonymous]
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
    }
}
