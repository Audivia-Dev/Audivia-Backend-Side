using Audivia.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Payment
{
    [Route("api/v1/payment-transaction")]
    [ApiController]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionService _paymentTransactionService;
        public PaymentTransactionController(IPaymentTransactionService paymentTransactionService)
        {
            _paymentTransactionService = paymentTransactionService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPaymentTransactionByUser(string userId)
        {
            var rs = await _paymentTransactionService.GetPaymentTransactionByUser(userId);
            return Ok(rs);
        }
    }
}
