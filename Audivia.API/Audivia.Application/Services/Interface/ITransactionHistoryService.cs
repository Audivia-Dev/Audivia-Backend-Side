using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TransactionHistory;
using Audivia.Domain.ModelResponses.TransactionHistory;
using Audivia.Domain.Models;

namespace Audivia.Application.Services.Interface
{
    public interface ITransactionHistoryService
    {
        Task<TransactionHistoryResponse> CreateTransactionHistory(CreateTransactionHistoryRequest request);

        Task<List<TransactionHistoryDTO>> GetAllTransactionHistorys();
        Task<List<TransactionWithUserTourDTO>> GetAllTransactionsWithUserAndTour();

        Task<TransactionHistoryResponse> GetTransactionHistoryById(string id);

        Task UpdateTransactionHistory(string id, UpdateTransactionHistoryRequest request);

        Task UpdateCharacterSelection(string id, UpdateCharacterIdRequest request);

        Task DeleteTransactionHistory(string id);
        Task<List<TransactionHistoryDTO>> GetTransactionHistoryByUserId(string userId);

        Task<TransactionHistoryDTO> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId);
    }
}
