using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ITourPreferenceRepository : IBaseRepository<TourPreference>, IDisposable
    {
    }
}
