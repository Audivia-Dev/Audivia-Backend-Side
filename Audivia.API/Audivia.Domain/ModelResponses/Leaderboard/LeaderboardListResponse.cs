using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Leaderboard
{
    public class LeaderboardListResponse : AbstractApiResponse<List<LeaderboardDTO>>
    {
        public override List<LeaderboardDTO> Response {  get; set; }
    }
}
