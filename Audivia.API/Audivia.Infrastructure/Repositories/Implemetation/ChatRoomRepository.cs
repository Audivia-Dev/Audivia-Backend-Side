using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class ChatRoomRepository : BaseRepository<ChatRoom>, IChatRoomRepository
    {
        private readonly IMongoCollection<ChatRoomMember> _memberCollection;
        private readonly IMongoCollection<User> _userCollection;
        public ChatRoomRepository(IMongoDatabase database, IChatRoomMemberRepository memberRepository) : base(database)
        {
            _memberCollection = database.GetCollection<ChatRoomMember>("ChatRoomMember");
            _userCollection = database.GetCollection<User>("User");
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
                .SortByDescending(x => x.CreatedAt)
                .ToListAsync();
            foreach (var room in rooms)
            {
                var members = await _memberCollection
                    .Find(m => m.ChatRoomId == room.Id)
                    .ToListAsync();

                foreach (var member in members)
                {
                    member.User = await _userCollection
                        .Find(u => u.Id == member.UserId)
                        .FirstOrDefaultAsync();
                }

                room.Members = members;
            }

            return rooms;
        }

        public async Task<ChatRoom> GetChatRoomById(string id)
        {
            // 1. Lấy chatRoom theo id
            var chatRoom = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (chatRoom == null) return null;

            // 2. Lấy member của chatRoom
            var members = await _memberCollection
                .Find(m => m.ChatRoomId == chatRoom.Id)
                .ToListAsync();

            // 3. Lấy user cho từng member
            foreach (var member in members)
            {
                member.User = await _userCollection
                    .Find(u => u.Id == member.UserId)
                    .FirstOrDefaultAsync();
            }

            // 4. Gán members có user info cho chatRoom
            chatRoom.Members = members;

            return chatRoom;
        }
    }
}
