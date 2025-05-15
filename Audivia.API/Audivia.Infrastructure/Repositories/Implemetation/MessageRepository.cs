using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<List<Message>> GetMessagesByChatRoomId(string chatRoomId)
        {
            var objectId = new ObjectId(chatRoomId);
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("chatroom_id", objectId)),
                new BsonDocument("$sort", new BsonDocument("created_at", 1)),
                new BsonDocument("$lookup", new BsonDocument
                {
                    {"from", "User" },
                    { "localField", "sender_id" },
                    { "foreignField", "_id" },
                    { "as", "Sender" }
                }),
                 new BsonDocument("$unwind", "$Sender")


            };
            var rs = await _collection.Aggregate<Message>(pipeline).ToListAsync();
            return rs;
        }
    }
}
