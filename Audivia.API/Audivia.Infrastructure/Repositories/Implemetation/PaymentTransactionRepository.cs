using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class PaymentTransactionRepository : BaseRepository<PaymentTransaction>, IPaymentTransactionRepository
    {
        public PaymentTransactionRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<PaymentTransaction> GetByOrderCodeAsync(int orderCode)
        {
            var filter = Builders<PaymentTransaction>.Filter.Eq(x => x.OrderCode, orderCode);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<PaymentTransaction>> GetPaymentTransactionByUser(string userId)
        {
            var builder = Builders<PaymentTransaction>.Filter;
            var filter = builder.Eq(x => x.UserId, userId) & builder.Eq(x => x.Status, "PAID");
            return await _collection.Find(filter).SortByDescending(x => x.PaymentTime).ToListAsync();
        }
    }
}
