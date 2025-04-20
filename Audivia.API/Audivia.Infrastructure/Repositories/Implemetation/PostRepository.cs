using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IMongoDatabase database) : base(database) { }
    }

}
