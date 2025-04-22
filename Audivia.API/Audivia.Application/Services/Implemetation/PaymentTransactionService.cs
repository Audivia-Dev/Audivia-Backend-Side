using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.ModelResponses.PaymentTransaction;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
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
        public PaymentTransactionService(IPaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
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
            Random random = new Random();
            var transaction = new PaymentTransaction
            {

                OrderCode = random.Next(1, 100000000),
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

    }
}
