using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Notification
{
    public class NotificationResponse : AbstractApiResponse<NotificationDTO>
    {
        public override NotificationDTO Response { get; set; }
    }
}
