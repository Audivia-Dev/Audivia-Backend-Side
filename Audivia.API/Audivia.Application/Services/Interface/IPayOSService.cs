using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IPayOSService
    {
        Task<string> CreateVietQR(CreatePaymentTransactionRequest transaction, string cancelUrl, string returnUrl);
        bool VerifyWebhook(PayOSWebhookRequest requestPayOSWebhookRequest);
    }
}
