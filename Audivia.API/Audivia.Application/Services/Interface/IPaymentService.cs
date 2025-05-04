using Audivia.Domain.ModelRequests.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IPaymentService
    {
        Task<object> CreateVietQRTransactionAsync(CreatePaymentRequest req);
        Task ProcessPayOSWebHookAsync(JsonElement payload);
        Task<object> HandlePaymentStatus(string id, int orderCode);
    }
}
