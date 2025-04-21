using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.ChatRoomMember;
using Audivia.Domain.ModelResponses.ChatRoomMember;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class ChatRoomMemberService : IChatRoomMemberService
    {
        private readonly IChatRoomMemberRepository _chatRoomMemberRepository;

        public ChatRoomMemberService(IChatRoomMemberRepository chatRoomMemberRepository)
        {
            _chatRoomMemberRepository = chatRoomMemberRepository;
        }

        public async Task<ChatRoomMemberResponse> CreateChatRoomMember(CreateChatRoomMemberRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _) || !ObjectId.TryParse(request.ChatRoomId, out _))
            {
                return new ChatRoomMemberResponse
                {
                    Success = false,
                    Message = "Invalid UserId or ChatRoomId format",
                    Response = null
                };
            }
            var chatRoomMember = new ChatRoomMember
            {
                ChatRoomId = request.ChatRoomId,
                UserId = request.UserId,
                Nickname = request.Nickname,
                IsHost = request.IsHost,
                JoinedAt = DateTime.UtcNow
            };

            await _chatRoomMemberRepository.Create(chatRoomMember);

            return new ChatRoomMemberResponse
            {
                Success = true,
                Message = "ChatRoomMember created successfully",
                Response = ModelMapper.MapChatRoomMemberToDTO(chatRoomMember)
            };
        }

        public async Task<ChatRoomMemberListResponse> GetAllChatRoomMembers()
        {
            var chatRoomMembers = await _chatRoomMemberRepository.GetAll();
            var chatRoomMemberDtos = chatRoomMembers
                .Select(ModelMapper.MapChatRoomMemberToDTO)
                .ToList();
            return new ChatRoomMemberListResponse
            {
                Success = true,
                Message = "ChatRoomMembers retrieved successfully",
                Response = chatRoomMemberDtos
            };
        }

        public async Task<ChatRoomMemberResponse> GetChatRoomMemberById(string id)
        {
            var chatRoomMember = await _chatRoomMemberRepository.FindFirst(t => t.Id == id);
            if (chatRoomMember == null)
            {
                return new ChatRoomMemberResponse
                {
                    Success = false,
                    Message = "ChatRoomMember not found",
                    Response = null
                };
            }

            return new ChatRoomMemberResponse
            {
                Success = true,
                Message = "ChatRoomMember retrieved successfully",
                Response = ModelMapper.MapChatRoomMemberToDTO(chatRoomMember)
            };
        }

        public async Task UpdateChatRoomMember(string id, UpdateChatRoomMemberRequest request)
        {
            var chatRoomMember = await _chatRoomMemberRepository.FindFirst(t => t.Id == id);
            if (chatRoomMember == null) return;

            
            chatRoomMember.Nickname = request.Nickname ?? chatRoomMember.Nickname;
            chatRoomMember.IsHost = request.IsHost ?? chatRoomMember.IsHost;

            await _chatRoomMemberRepository.Update(chatRoomMember);
        }

        public async Task DeleteChatRoomMember(string id)
        {
            var chatRoomMember = await _chatRoomMemberRepository.FindFirst(t => t.Id == id);
            if (chatRoomMember == null) return;

            await _chatRoomMemberRepository.Delete(chatRoomMember);
        }
    }
}
