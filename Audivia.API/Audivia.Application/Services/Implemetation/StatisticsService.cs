using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public StatisticsService(ITransactionHistoryRepository transactionHistoryRepository,
            ITourRepository tourRepository,
            IPostRepository postRepository,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _transactionHistoryRepository = transactionHistoryRepository;
            _tourRepository = tourRepository;
            _userRepository = userRepository;
        }
        public async Task<GetRevenueStatResponse> GetRevenueStatistics(GetRevenueStatRequest request)
        {
            var filter = Builders<TransactionHistory>.Filter.And(
                Builders<TransactionHistory>.Filter.Gte(t => t.CreatedAt, request.StartDate.Value.ToDateTime(TimeOnly.MinValue)),
                Builders<TransactionHistory>.Filter.Lte(t => t.CreatedAt, request.EndDate.Value.ToDateTime(TimeOnly.MaxValue))
            );
            int totalBookings = await _transactionHistoryRepository.Count(filter);
            var revenueItems = await _transactionHistoryRepository.GetRevenueStatisticsAsync(request);
            double totalRevenue = revenueItems.Sum(i => i.Revenue);
            return new GetRevenueStatResponse { Items = revenueItems, TotalBookings = totalBookings, TotalRevenue = totalRevenue };
        }

        public async Task<GetTourStatResponse> GetTourStatistics(GetTourStatRequest request)
        {
            var tourStatItems = await _tourRepository.GetTourStatisticsAsync(request);
            int totalTours = tourStatItems.Sum(i => i.Value);
            return new GetTourStatResponse { TotalTours = totalTours, Items = tourStatItems };
        }

        public async Task<GetPostStatResponse> GetPostStatistics(GetPostStatRequest request)
        {
            var postStatItems = await _postRepository.GetPostStatisticsAsync(request);
            return new GetPostStatResponse { Items = postStatItems };
        }

        public async Task<GetUserStatResponse> GetUserStatistics(GetUserStatRequest request)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Gte(t => t.CreatedAt, request.StartDate.Value.ToDateTime(TimeOnly.MinValue)),
                Builders<User>.Filter.Lte(t => t.CreatedAt, request.EndDate.Value.ToDateTime(TimeOnly.MaxValue))
            );
            int totalUsers = await _userRepository.Count(filter);
            var userStatItems = await _userRepository.GetUserStatisticsAsync(request);
            return new GetUserStatResponse { TotalUsers = totalUsers, Items = userStatItems };
        }

        public async Task<List<TourWithPurchaseCount>> GetTopPurchasedToursWithDetailAsync(int topN)
        {
            return await _transactionHistoryRepository.GetTopPurchasedToursWithDetailAsync(topN);
        }
    }
}
