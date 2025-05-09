using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Message;
using Audivia.Domain.ModelResponses.Message;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System.Reflection.Metadata.Ecma335;

namespace Audivia.Application.Services.Implemetation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageResponse> CreateMessage(CreateMessageRequest request)
        {
            if (!ObjectId.TryParse(request.SenderId, out _) || !ObjectId.TryParse(request.ChatRoomId, out _))
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "Invalid SenderId or ChatRoomId format",
                    Response = null
                };
            }
            var message = new Message
            {
                SenderId = request.SenderId,
                Content = request.Content,
                ChatRoomId = request.ChatRoomId,
                Status = request.Status, // read or sending or sent or thu hoi...
                Type = request.Type,
                CreatedAt = DateTime.UtcNow,
            };

            await _messageRepository.Create(message);

            return new MessageResponse
            {
                Success = true,
                Message = "Message created successfully",
                Response = ModelMapper.MapMessageToDTO(message)
            };
        }

        public async Task<MessageListResponse> GetAllMessages()
        {
            var messages = await _messageRepository.GetAll();
            var messageDtos = messages
                .Select(ModelMapper.MapMessageToDTO)
                .ToList();
            return new MessageListResponse
            {
                Success = true,
                Message = "Messages retrieved successfully",
                Response = messageDtos
            };
        }

        public async Task<MessageResponse> GetMessageById(string id)
        {
            var message = await _messageRepository.FindFirst(t => t.Id == id);
            if (message == null)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "Message not found",
                    Response = null
                };
            }

            return new MessageResponse
            {
                Success = true,
                Message = "Message retrieved successfully",
                Response = ModelMapper.MapMessageToDTO(message)
            };
        }

        public async Task<MessageDTO> UpdateMessage(string id, UpdateMessageRequest request)
        {
            var message = await _messageRepository.FindFirst(t => t.Id == id);
            if (message == null) return null;
            
            message.Type = request.Type ?? message.Type;
            message.Content = request.Content ?? message.Content;
            message.Status = request.Status ?? message.Status;

            await _messageRepository.Update(message);
            return ModelMapper.MapMessageToDTO(message);
        }

        public async Task<MessageDTO> DeleteMessage(string id)
        {
            var message = await _messageRepository.FindFirst(t => t.Id == id);
            if (message == null) return null;

            await _messageRepository.Delete(message);
            return ModelMapper.MapMessageToDTO(message);
        }
    }
}
