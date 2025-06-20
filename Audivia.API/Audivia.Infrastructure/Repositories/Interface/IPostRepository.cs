using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IPostRepository : IBaseRepository<Post>, IDisposable
    {
        Task<List<PostStatItem>> GetPostStatisticsAsync(GetPostStatRequest request);
    }
}
