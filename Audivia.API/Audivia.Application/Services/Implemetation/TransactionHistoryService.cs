using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TransactionHistory;
using Audivia.Domain.ModelResponses.TransactionHistory;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public TransactionHistoryService(ITransactionHistoryRepository transactionHistoryRepository)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public async Task<TransactionHistoryResponse> CreateTransactionHistory(CreateTransactionHistoryRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _) || !ObjectId.TryParse(request.TourId, out _))
            {
                return new TransactionHistoryResponse
                {
                    Success = false,
                    Message = "Invalid UserId or TourId format",
                    Response = null
                };
            }
            var transactionHistory = new TransactionHistory
            {
                UserId = request.UserId,
                TourId = request.TourId,
                Amount = request.Amount,
                Description = request.Description,
                Type = request.Type,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _transactionHistoryRepository.Create(transactionHistory);

            return new TransactionHistoryResponse
            {
                Success = true,
                Message = "Audio transaction created successfully",
                Response = ModelMapper.MapTransactionHistoryToDTO(transactionHistory)
            };
        }

        public async Task<List<TransactionHistoryDTO>> GetAllTransactionHistorys()
        {
            var transactions = await _transactionHistoryRepository.GetAll();
            return transactions
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapTransactionHistoryToDTO)
                .ToList();
        }

        public async Task<TransactionHistoryResponse> GetTransactionHistoryById(string id)
        {
            var transaction = await _transactionHistoryRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (transaction == null)
            {
                return new TransactionHistoryResponse
                {
                    Success = false,
                    Message = "Transaction not found",
                    Response = null
                };
            }

            return new TransactionHistoryResponse
            {
                Success = true,
                Message = "Transaction retrieved successfully",
                Response = ModelMapper.MapTransactionHistoryToDTO(transaction)
            };
        }

        public async Task UpdateTransactionHistory(string id, UpdateTransactionHistoryRequest request)
        {
            var transaction = await _transactionHistoryRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (transaction == null) return;

            transaction.Status = request.Status ?? transaction.Status;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _transactionHistoryRepository.Update(transaction);
        }

        public async Task DeleteTransactionHistory(string id)
        {
            var transaction = await _transactionHistoryRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (transaction == null) return;

            transaction.IsDeleted = true;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _transactionHistoryRepository.Update(transaction);
        }

        public async Task<List<TransactionHistoryDTO>> GetTransactionHistoryByUserId(string userId)
        {
            var transactions = await _transactionHistoryRepository.GetTransactionHistoryByUserId(userId);
            return transactions
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapTransactionHistoryToDTO)
                .ToList();
        }
    }
}
