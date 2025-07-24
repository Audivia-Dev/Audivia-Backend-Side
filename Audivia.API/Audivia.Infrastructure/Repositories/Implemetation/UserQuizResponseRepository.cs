using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class UserQuizResponseRepository : BaseRepository<Domain.Models.UserQuizResponse>, IUserQuizResponseRepository
    {
        public UserQuizResponseRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
