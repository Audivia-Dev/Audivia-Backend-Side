using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.ChatRoom;
using Audivia.Domain.ModelResponses.ChatRoom;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomService(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<ChatRoomResponse> CreateChatRoom(CreateChatRoomRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                return new ChatRoomResponse
                {
                    Success = false,
                    Message = "Invalid CreatedBy format",
                    Response = null
                };
            }
            var chatRoom = new ChatRoom
            {
                Name = request.Name,
                Type = request.Type,
                IsActive = true,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _chatRoomRepository.Create(chatRoom);

            return new ChatRoomResponse
            {
                Success = true,
                Message = "ChatRoom created successfully",
                Response = ModelMapper.MapChatRoomToDTO(chatRoom)
            };
        }

        public async Task<ChatRoomListResponse> GetAllChatRooms()
        {
            var chatRooms = await _chatRoomRepository.GetAll();
            var chatRoomDtos = chatRooms
                .Where(t => t.IsActive)
                .Select(ModelMapper.MapChatRoomToDTO)
                .ToList();
            return new ChatRoomListResponse
            {
                Success = true,
                Message = "ChatRooms retrieved successfully",
                Response = chatRoomDtos
            };
        }

        public async Task<ChatRoomResponse> GetChatRoomById(string id)
        {
            var chatRoom = await _chatRoomRepository.FindFirst(t => t.Id == id && t.IsActive);
            if (chatRoom == null)
            {
                return new ChatRoomResponse
                {
                    Success = false,
                    Message = "ChatRoom not found",
                    Response = null
                };
            }

            return new ChatRoomResponse
            {
                Success = true,
                Message = "ChatRoom retrieved successfully",
                Response = ModelMapper.MapChatRoomToDTO(chatRoom)
            };
        }

        public async Task UpdateChatRoom(string id, UpdateChatRoomRequest request)
        {
            var chatRoom = await _chatRoomRepository.FindFirst(t => t.Id == id && t.IsActive);
            if (chatRoom == null) return;
            chatRoom.Name = request.Name ?? chatRoom.Name;
            chatRoom.Type = request.Type ?? chatRoom.Type;
            chatRoom.IsActive = request.IsActive ?? chatRoom.IsActive;
            chatRoom.UpdatedAt = DateTime.UtcNow;

            await _chatRoomRepository.Update(chatRoom);
        }

        public async Task DeleteChatRoom(string id)
        {
            var chatRoom = await _chatRoomRepository.FindFirst(t => t.Id == id && t.IsActive);
            if (chatRoom == null) return;

            chatRoom.IsActive = false;
            chatRoom.UpdatedAt = DateTime.UtcNow;

            await _chatRoomRepository.Update(chatRoom);
        }
    }
}
