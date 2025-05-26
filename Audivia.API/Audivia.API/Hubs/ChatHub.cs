using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatRoom;
using Audivia.Domain.ModelRequests.ChatRoomMember;
using Audivia.Domain.ModelRequests.Message;
using Audivia.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Audivia.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IChatRoomMemberService _chatRoomMemberService;
        private readonly IChatRoomService _chatRoomService;
        private static readonly Dictionary<string, string> ConnectedUsers = new();
        public ChatHub(IMessageService messageService, IChatRoomService chatRoomService, IChatRoomMemberService chatRoomMemberService)
        {
            _chatRoomMemberService = chatRoomMemberService;
            _messageService = messageService;
            _chatRoomService = chatRoomService;
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("userId")?.Value;
            Console.WriteLine($"OnConnectedAsync ChatHub called - userId: {userId}, ConnectionId: {Context.ConnectionId}");

            ConnectedUsers[Context.ConnectionId] = userId;

            await Clients.All.SendAsync("UserOnline", userId);
            await base.OnConnectedAsync();
        }
        public async Task JoinRoom(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
        }

        public async Task LeaveRoom(string chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
        }

        public async Task SendTyping(string chatRoomId, string userId)
        {
            await Clients.OthersInGroup(chatRoomId).SendAsync("UserTyping", new
            {
                chatRoomId,
                userId
            });
        }

        //for testing
        public async Task SendMessage(CreateMessageRequest req)
        {
            try
            {
                await _messageService.CreateMessage(req);
               
                await Clients.Group(req.ChatRoomId).SendAsync("ReceiveMessage", req);
            }
            catch (Exception ex) 
            {
                await Clients.Caller.SendAsync("Error", "Member not found!");
            }

        }

        //for testing
        public async Task JoinChatRoom(CreateChatRoomMemberRequest req)
        {
            try
            {
                //await Groups.AddToGroupAsync(chatRoomId, userId);

                //await Clients.Group(chatRoomId).SendAsync("UserJoined", userId);
                // Thêm kết nối vào nhóm SignalR
                await Groups.AddToGroupAsync(Context.ConnectionId, req.ChatRoomId);
                Context.Items["ChatRoomId"] = req.ChatRoomId;
                Context.Items["UserId"] = req.UserId;

                // Thông báo cho các thành viên khác
                await Clients.Group(req.ChatRoomId).SendAsync("UserJoined", req.UserId);
                await _chatRoomMemberService.CreateChatRoomMember(req);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", $"Failed to join chat room: {ex.Message}");
            }
        }

        //for testing
        public async Task LeaveChatRoom(CreateChatRoomMemberRequest req)
        {
            try
            {
                // Xóa kết nối khỏi nhóm SignalR
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, req.ChatRoomId);

                // Thông báo cho các thành viên khác
                await Clients.Group(req.ChatRoomId).SendAsync("UserLeft", req.UserId);
                await _chatRoomMemberService.DeleteChatRoomMember(req.UserId);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
