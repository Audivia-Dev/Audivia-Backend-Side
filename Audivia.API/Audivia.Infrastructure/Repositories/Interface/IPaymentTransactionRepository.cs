using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IPaymentTransactionRepository : IBaseRepository<PaymentTransaction>, IDisposable
    {
        Task<PaymentTransaction> GetByOrderCodeAsync(int orderCode);
        Task<List<PaymentTransaction>> GetPaymentTransactionByUser(string userId);

    }
}
