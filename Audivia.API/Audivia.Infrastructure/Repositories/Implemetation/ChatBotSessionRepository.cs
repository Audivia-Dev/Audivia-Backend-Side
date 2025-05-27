using Audivia.Domain.Models.ChatBotHistory;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class ChatBotSessionRepository : BaseRepository<ChatBotSession>, IChatBotSessionRepository
    {
        private readonly IMongoCollection<ChatBotSession> _sessions;

        public ChatBotSessionRepository(IMongoDatabase database) : base(database)
        {
            _sessions = database.GetCollection<ChatBotSession>(nameof(ChatBotSession));
        }

        public async Task<ChatBotSession> GetByClientSessionIdAsync(string clientSessionId)
        {
            return await _sessions.Find(s => s.ClientSessionId == clientSessionId && s.IsActive).FirstOrDefaultAsync();
        }
        
    }
}
