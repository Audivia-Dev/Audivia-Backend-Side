using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Notification
{
    public class NotificationListResponse : AbstractApiResponse<List<NotificationDTO>>
    {
        public override List<NotificationDTO> Response { get; set; }
    }
}
