using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IUserCheckpointProgressRepository : IBaseRepository<UserCheckpointProgress>, IDisposable
    {
        Task<List<UserCheckpointProgress>> GetByTourProgressId(string tourProgressId);
    }
}
