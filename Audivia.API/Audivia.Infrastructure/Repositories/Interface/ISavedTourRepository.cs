using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ISavedTourRepository : IBaseRepository<SavedTour>, IDisposable
    {
        Task<List<SavedTour>> GetSavedTourByUserId(string id);
    }
}
