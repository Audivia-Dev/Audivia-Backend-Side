using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class ChatRoomRepository : BaseRepository<ChatRoom>, IChatRoomRepository
    {
        private readonly IMongoCollection<ChatRoomMember> _memberCollection;
        public ChatRoomRepository(IMongoDatabase database, IChatRoomMemberRepository memberRepository) : base(database)
        {
            _memberCollection = database.GetCollection<ChatRoomMember>("ChatRoomMember");
        }
        public async Task<List<ChatRoom>> GetChatRoomsOfUser(string userId)
        {
            // Bước 1: Lấy danh sách các ChatRoomId
            var roomIds = await _memberCollection
                .Find(x => x.UserId == userId)
                .Project(x => x.ChatRoomId)
                .ToListAsync();

            // Bước 2: Truy vấn danh sách ChatRoom tương ứng
            var rooms = await _collection
                .Find(x => roomIds.Contains(x.Id))
                .ToListAsync();

            return rooms;
        }
    }
}
