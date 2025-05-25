using Microsoft.AspNetCore.SignalR;

namespace Audivia.API.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("userId")?.Value;
            Console.WriteLine($"OnConnectedAsync called - userId: {userId}, ConnectionId: {Context.ConnectionId}");

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst("userId")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Server gọi để gửi số thông báo chưa đọc mới cho user
        public async Task SendUnreadCount(string userId, int unreadCount)
        {
            await Clients.Group(userId).SendAsync("ReceiveUnreadCount", unreadCount);
        }
    }
}
