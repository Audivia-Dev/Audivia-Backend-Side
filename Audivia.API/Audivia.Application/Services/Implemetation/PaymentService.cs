using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentTransactionService _transactionService;
        private readonly IPayOSService _payOSService;
        private readonly IAuthService _authService;
        private readonly IPaymentTransactionRepository _transactionRepository;
        private readonly IUserService _userService;
        public PaymentService(
            IPaymentTransactionService transactionService,
            IPayOSService payOSService,
            IAuthService authService, IPaymentTransactionRepository transactionRepository, IUserService userService)
        {
            _transactionService = transactionService;
            _payOSService = payOSService;
            _authService = authService;
            _transactionRepository = transactionRepository;
            _userService = userService;
        }

        public async Task<object> CreateVietQRTransactionAsync(CreatePaymentRequest req)
        {
            var user = await _authService.GetCurrentUserAsync();
            var orderCode = new Random().Next(1, 100000000);

            var transaction = new CreatePaymentTransactionRequest
            {
                OrderCode = orderCode,
                UserId = user.Id,
                Amount = req.Amount,
                Description = req.Description,
            };

            await _transactionService.CreateTransactionAsync(transaction);

            var data = await _payOSService.CreateVietQR(transaction, req.CancelUrl, req.ReturnUrl);
            return data;
        }



        public async Task ProcessPayOSWebHookAsync(JsonElement payload)
        {
            var data = payload.GetProperty("data");

            var orderCode = data.GetProperty("orderCode").GetInt32();
            var amount = data.GetProperty("amount").GetInt32();
            var code = data.GetProperty("code").GetString(); 
            var desc = data.GetProperty("desc").GetString(); 
            var paymentTimeString = data.GetProperty("transactionDateTime").GetString();

            var transaction = await _transactionService.GetByOrderCode(orderCode);

            if (transaction == null || transaction.Status == "PAID" || transaction.Status == "CANCELLED")
                return;

            if (code == "00")
            {
                transaction.Status = "PAID";
                transaction.PaymentTime = DateTime.TryParse(paymentTimeString, out var time)
                    ? time.ToUniversalTime()
                    : DateTime.UtcNow;

                await _transactionRepository.Update(transaction);

                //cập nhật ví tiền
                await _userService.IncreaseBalanceAsync(transaction.UserId, amount);
            }
            else
            {
                transaction.Status = "CANCELLED"; 
                await _transactionRepository.Update(transaction);
            }
        }


    }
}
