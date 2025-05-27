using Audivia.Domain.Models.ChatBotHistory;
using Audivia.Infrastructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IChatBotMessageRepository : IBaseRepository<ChatBotMessage>, IDisposable
    {
        Task<IEnumerable<ChatBotMessage>> GetMessagesByClientSessionIdAsync(string clientSessionId, int pageNumber, int pageSize);
    }
}
