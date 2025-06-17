using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
using MongoDB.Bson;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ITourRepository: IBaseRepository<Tour>, IDisposable
    {
        Task<List<Tour>> GetToursByTypesExcludingIdsAsync(List<string> tourTypes, List<string> excludeTourIds);
        Task<List<TourStatItem>> GetTourStatisticsAsync(GetTourStatRequest request);
    }
}
