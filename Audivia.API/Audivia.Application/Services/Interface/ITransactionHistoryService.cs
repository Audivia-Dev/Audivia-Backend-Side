using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TransactionHistory;
using Audivia.Domain.ModelResponses.TransactionHistory;

namespace Audivia.Application.Services.Interface
{
    public interface ITransactionHistoryService
    {
        Task<TransactionHistoryResponse> CreateTransactionHistory(CreateTransactionHistoryRequest request);

        Task<List<TransactionHistoryDTO>> GetAllTransactionHistorys();

        Task<TransactionHistoryResponse> GetTransactionHistoryById(string id);

        Task UpdateTransactionHistory(string id, UpdateTransactionHistoryRequest request);

        Task DeleteTransactionHistory(string id);
    }
}
