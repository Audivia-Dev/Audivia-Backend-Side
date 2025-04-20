using Audivia.Domain.ModelRequests.Leaderboard;
using Audivia.Domain.ModelResponses.Leaderboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface ILeaderboardService
    {
        Task<LeaderboardResponse> CreateLeaderboardAsync(CreateLeaderboardRequest req);
        Task<LeaderboardResponse> UpdateLeaderboardAsync(string id, UpdateLeaderboardRequest req);
        Task<LeaderboardResponse> DeleteLeaderboardAsync(string id);
        Task<LeaderboardListResponse> GetAllLeaderboardsAsync();
        Task<LeaderboardResponse> GetLeaderboardByIdAsync(string id);
    }
}
