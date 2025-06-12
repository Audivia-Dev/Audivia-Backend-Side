using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
using MongoDB.Bson;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ITourRepository: IBaseRepository<Tour>, IDisposable
    {
        Task<List<Tour>> GetToursByTypesExcludingIdsAsync(List<string> tourTypes, List<string> excludeTourIds);
    }
}
