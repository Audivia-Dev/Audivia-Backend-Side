using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.ModelResponses.Payment;
using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IPayOSService
    {
        Task<object> CreateVietQR(CreatePaymentTransactionRequest transaction, string cancelUrl, string returnUrl);
        Task ConfirmWebhookAsync();

        //Task<PaymentResponse> CheckPaymentStatusAsync(string id);
        bool VerifyBankWebhook(JsonElement payload);
    }
}
