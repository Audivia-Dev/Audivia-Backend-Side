using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Leaderboard
{
    public class CreateLeaderboardRequest
    {
        public string? TourId { get; set; }
        public string? UserId { get; set; }
        public string? Rank { get; set; }
        public double? Score { get; set; }
    }
}
