using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IChatRoomRepository : IBaseRepository<ChatRoom>, IDisposable
    {
        Task<List<ChatRoom>> GetChatRoomsOfUser(string userId);
        Task<ChatRoom> GetChatRoomById(string id);
    }
}
