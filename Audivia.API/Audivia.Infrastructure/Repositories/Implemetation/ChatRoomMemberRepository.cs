using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class ChatRoomMemberRepository : BaseRepository<ChatRoomMember>, IChatRoomMemberRepository
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        public ChatRoomMemberRepository(IMongoDatabase database, IChatRoomRepository chatRoomRepository) : base(database)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<ChatRoom> GetPrivateChatRoomBetweenUsers(string user1, string user2)
        {
            var user1RoomIds = await _collection.Find(x => x.UserId == user1)
                .Project(x => x.ChatRoomId).ToListAsync();
            var commonRoomIds = await _collection.Find(x => x.UserId == user2 && user1RoomIds.Contains(x.ChatRoomId))
                .Project(x => x.ChatRoomId).ToListAsync();
            foreach (var roomId in commonRoomIds)
            {
                var room = await _chatRoomRepository.GetById(new ObjectId(roomId));
                if (room != null && room.Type == "private")
                {
                    var memberCount = await _collection.CountDocumentsAsync(x => x.ChatRoomId == roomId);
                    if (memberCount == 2)
                    {
                        return room;
                    }
                }
            }
            return null;
        }
    }
}
