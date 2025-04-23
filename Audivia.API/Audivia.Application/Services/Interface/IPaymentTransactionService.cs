using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.ModelResponses.PaymentTransaction;
using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IPaymentTransactionService
    {

        Task<PaymentTransactionResponse> CreateTransactionAsync(CreatePaymentTransactionRequest request);
        Task<PaymentTransactionResponse> UpdateTransactionAsync(string id, UpdatePaymentTransactionRequest request);
        Task<PaymentTransaction> GetByOrderCode(int orderCode);

        Task ConfirmPayment(string orderCode, DateTime paidAt);
    }
}
