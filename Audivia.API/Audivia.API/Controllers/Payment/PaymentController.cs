using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Payment
{
    [Route("api/v1/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayOSService _payOSService;
        private readonly IPaymentTransactionService _transactionService;
        public PaymentController(IPaymentTransactionService transactionService, IPayOSService payOSService)
        {
            _payOSService = payOSService;
            _transactionService = transactionService;
        }
        [HttpPost("vietqr")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentTransactionRequest req)
        {
            var tx = await _transactionService.CreateTransactionAsync(req);
            var qr = await _payOSService.CreateVietQR(req, "google.com", "abc");
            return Ok(new { qrCode = qr });
        }
    }
}
