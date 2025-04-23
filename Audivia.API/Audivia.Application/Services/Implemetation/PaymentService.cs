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

        public async Task<string> CreateVietQRTransactionAsync(CreatePaymentRequest req)
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

            var qrUrl = await _payOSService.CreateVietQR(transaction, req.CancelUrl, req.ReturnUrl);
            return qrUrl;
        }



        public async Task<object> HandlePaymentStatus(string id, int orderCode)
        {

            var paymentResponse = await _payOSService.CheckPaymentStatusAsync(id);
            if (paymentResponse == null)
            {
                return new { Message = "Can not found payment!" };
            }
            var transaction = await _transactionService.GetByOrderCode(orderCode);
            transaction.Status = paymentResponse.Status;
            transaction.PaymentTime = DateTime.UtcNow;
            await _transactionService.UpdateTransaction(transaction);
            if (paymentResponse.Status == "PAID")
            {
                var user = await _userService.GetById(transaction.UserId);
                user.BalanceWallet += transaction.Amount;
                await _userService.UpdateUser(user);
            }
            return new
            {
                Message = "Update payment successfully!"
            };
        }











        public async Task ProcessPayOSWebHookAsync(JsonElement payload)
        {
            var orderCode = payload.GetProperty("data").GetProperty("orderCode").GetInt32();
            var amount = payload.GetProperty("data").GetProperty("amount").GetInt32();
            var status = payload.GetProperty("data").GetProperty("status").GetString();
            var transaction = await _transactionService.GetByOrderCode(orderCode);
            if (transaction == null || transaction.Status == "PAID" || transaction.Status == "CANCELLED")
                return;
            if (status == "PAID")
            {
                // Thanh toán thành công
                transaction.Status = "PAID";
                transaction.PaymentTime = DateTime.UtcNow;
                await _transactionRepository.Update(transaction);
               // await _walletService.IncreaseBalanceAsync(transaction.UserId, amount);
            }
            else if (status == "CANCELLED")
            {
                transaction.Status = "CANCELLED";
                await _transactionRepository.Update(transaction);
            }    
        }


    }
}
