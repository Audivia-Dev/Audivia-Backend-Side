using Audivia.Domain.Models.ChatBotHistory;
using Audivia.Infrastructure.Interface;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IChatBotSessionRepository : IBaseRepository<ChatBotSession>, IDisposable
    {
        Task<ChatBotSession> GetByClientSessionIdAsync(string clientSessionId);
    }
}
