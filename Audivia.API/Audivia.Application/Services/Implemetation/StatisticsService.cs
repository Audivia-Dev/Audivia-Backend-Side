using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Infrastructure.Repositories.Interface;

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
            var revenueItems = await _transactionHistoryRepository.GetRevenueStatisticsAsync(request);
            return new GetRevenueStatResponse { Items = revenueItems };
        }

        public async Task<GetTourStatResponse> GetTourStatistics(GetTourStatRequest request)
        {
            var tourStatItems = await _tourRepository.GetTourStatisticsAsync(request);
            return new GetTourStatResponse { Items = tourStatItems };
        }

        public async Task<GetPostStatResponse> GetPostStatistics(GetPostStatRequest request)
        {
            var postStatItems = await _postRepository.GetPostStatisticsAsync(request);
            return new GetPostStatResponse { Items = postStatItems };
        }
        
        public async Task<GetUserStatResponse> GetUserStatistics(GetUserStatRequest request)
        {
            var userStatItems = await _userRepository.GetUserStatisticsAsync(request);
            return new GetUserStatResponse { Items = userStatItems };
        }
    }
}
