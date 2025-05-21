using Audivia.Domain.ModelResponses.TransactionHistory;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TransactionHistoryRepository : BaseRepository<TransactionHistory>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(IMongoDatabase database) : base(database) { }
        public async Task<List<TransactionHistory>> GetTransactionHistoryByUserId(string userId)
        {
            return await _collection.Find(x => x.UserId == userId).ToListAsync();
        }
        public async Task<TransactionHistory> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId)
        {
            return await _collection.Find(x => x.UserId == userId && x.TourId == tourId).FirstOrDefaultAsync();
        }
    }
}
