using Audivia.Domain.Models;
using Audivia.Domain.Models.ChatBotHistory;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class ChatBotMessageRepository : BaseRepository<ChatBotMessage>, IChatBotMessageRepository
    {
        private readonly IMongoCollection<ChatBotMessage> _messages;

        public ChatBotMessageRepository(IMongoDatabase database) : base(database) 
        {
            _messages = database.GetCollection<ChatBotMessage>("ChatBotMessage");

        }

        public async Task<IEnumerable<ChatBotMessage>> GetMessagesByClientSessionIdAsync(string clientSessionId, int pageNumber, int pageSize)
        {
            var filter = Builders<ChatBotMessage>.Filter.Eq(m => m.ClientSessionId, clientSessionId);
            var sort = Builders<ChatBotMessage>.Sort.Ascending(m => m.Timestamp);

            return await _messages.Find(filter)
                                .Sort(sort)
                                .Skip((pageNumber - 1) * pageSize)
                                .Limit(pageSize)
                                .ToListAsync();
        }
    }
}
