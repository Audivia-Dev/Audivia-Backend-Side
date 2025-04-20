using Audivia.Domain.ModelRequests.PlayResult;
using Audivia.Domain.ModelResponses.PlayResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IPlayResultService
    {
        Task<PlayResultResponse> CreatePlayResultAsync(CreatePlayResultRequest req);
        Task<PlayResultResponse> UpdatePlayResultAsync(string id, UpdatePlayResultRequest req);
        Task<PlayResultResponse> DeletePlayResultAsync(string id);
        Task<PlayResultListResponse> GetAllPlayResultsAsync();
        Task<PlayResultResponse> GetPlayResultByIdAsync(string id);
    }
}
