using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;

namespace Audivia.Application.Services.Interface
{
    public interface IStatisticsService
    {
        Task<GetRevenueStatResponse> GetRevenueStatistics(GetRevenueStatRequest request);
        Task<GetTourStatResponse> GetTourStatistics(GetTourStatRequest request);
        Task<GetPostStatResponse> GetPostStatistics(GetPostStatRequest request);
        Task<GetUserStatResponse> GetUserStatistics(GetUserStatRequest request);
        Task<List<TourWithPurchaseCount>> GetTopPurchasedToursWithDetailAsync(int topN);
    }
}
