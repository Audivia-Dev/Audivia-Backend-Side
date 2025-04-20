using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class ChatRoomMemberRepository : BaseRepository<ChatRoomMember>, IChatRoomMemberRepository
    {
        public ChatRoomMemberRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
