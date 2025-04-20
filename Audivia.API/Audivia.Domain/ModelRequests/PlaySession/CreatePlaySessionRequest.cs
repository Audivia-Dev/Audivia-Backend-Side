using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.PlaySession
{
    public class CreatePlaySessionRequest
    {
        public string? UserId { get; set; }
        public string? RouteId { get; set; }
        public string? GroupId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
