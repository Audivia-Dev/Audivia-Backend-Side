using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.ModelResponses.TransactionHistory;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
using MongoDB.Bson;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ITransactionHistoryRepository : IBaseRepository<TransactionHistory>, IDisposable
    {
        Task<List<TransactionHistory>> GetTransactionHistoryByUserId(string userId);
        Task<TransactionHistory> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId);

        Task<Dictionary<string, int>> GetTopTourTypesByUserIdAsync(string userId, int topN);
        Task<List<string?>> GetBookedTourIdsByUserIdAsync(string userId);
        Task<List<RevenueStatItem>> GetRevenueStatisticsAsync(GetRevenueStatRequest request);
    }
}
