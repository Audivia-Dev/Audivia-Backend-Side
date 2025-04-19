using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class UserAudioTourRepository : BaseRepository<UserAudioTour>, IUserAudioTourRepository
    {
        public UserAudioTourRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
