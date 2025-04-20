using Audivia.Domain.ModelRequests.PlaySession;
using Audivia.Domain.ModelResponses.PlaySession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IPlaySessionService
    {
        Task<PlaySessionResponse> CreatePlaySessionAsync(CreatePlaySessionRequest req);
        Task<PlaySessionResponse> UpdatePlaySessionAsync(string id, UpdatePlaySessionRequest req);
        Task<PlaySessionResponse> DeletePlaySessionAsync(string id);
        Task<PlaySessionListResponse> GetAllPlaySessionsAsync();
        Task<PlaySessionResponse> GetPlaySessionByIdAsync(string id);
    }
}
