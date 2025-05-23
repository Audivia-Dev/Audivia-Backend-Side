﻿using Audivia.Domain.ModelResponses.TransactionHistory;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ITransactionHistoryRepository : IBaseRepository<TransactionHistory>, IDisposable
    {
        Task<List<TransactionHistory>> GetTransactionHistoryByUserId(string userId);
        Task<TransactionHistory> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId);
    }
}
