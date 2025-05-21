using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.ModelResponses.GroupMember;
using Audivia.Domain.ModelResponses.PaymentTransaction;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Implemetation;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Audivia.Application.Services.Implemetation
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly IAuthService _authService;
        public PaymentTransactionService(IPaymentTransactionRepository paymentTransactionRepository, IAuthService authService)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
            _authService = authService;
        }
        public async Task<PaymentTransaction> GetByOrderCodeAsync(int orderCode)
        {
            return  await _paymentTransactionRepository.GetByOrderCodeAsync(orderCode);
            
        }

        public async Task<PaymentTransactionResponse> CreateTransactionAsync(CreatePaymentTransactionRequest request)
        {
            if (request == null)
            {
                return new PaymentTransactionResponse
                {
                    Success = false,
                    Message = "Create transaction failed!",
                    Response = null
                };
            }
            var transaction = new PaymentTransaction
            {

                OrderCode = request.OrderCode,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow,
                Description = request.Description,
                Status = "PENDING",
                UserId = request.UserId,
            };
            await _paymentTransactionRepository.Create(transaction);
            return new PaymentTransactionResponse
            {
                Success = true,
                Message = "Created successfully!",
                Response = ModelMapper.MapPaymentTransactioToDTO(transaction)
            };
        }

        public async Task<PaymentTransactionResponse> UpdateTransactionAsync(string id, UpdatePaymentTransactionRequest request)
        {
            if (request == null)
            {
                return new PaymentTransactionResponse
                {
                    Success = false,
                    Message = "Create transaction failed!",
                    Response = null
                };
            }
            var transaction = await _paymentTransactionRepository.GetById(new ObjectId(id.ToString()));
            if (transaction == null)
            {
                return new PaymentTransactionResponse
                {
                    Success = false,
                    Message = "Not found transaction!",
                    Response = null
                };
            }
            transaction.Status = "PAID";
            transaction.PaymentTime = request.PaymentTime;
            await _paymentTransactionRepository.Update(transaction);
            return new PaymentTransactionResponse
            {
                Success = true,
                Message = "Updated successfully!",
                Response = ModelMapper.MapPaymentTransactioToDTO(transaction)
            };
        }

        public async Task<PaymentTransaction> GetByOrderCode(int orderCode)
        {
            return await _paymentTransactionRepository.GetByOrderCodeAsync(orderCode);
        }

        public Task ConfirmPayment(string orderCode, DateTime paidAt)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTransaction(PaymentTransaction transaction)
        {
            await _paymentTransactionRepository.Update(transaction);
        }


        public async Task<PaymentTransactionListResponse> GetPaymentTransactionByUser(string userId)
        {
            var list = await _paymentTransactionRepository.GetPaymentTransactionByUser(userId);
            return new PaymentTransactionListResponse
            {
                Success = true,
                Message = "Fetched all payment transaction successfully!",
                Response = list.Select(ModelMapper.MapPaymentTransactioToDTO).ToList(),
            };
        }
    }
}
