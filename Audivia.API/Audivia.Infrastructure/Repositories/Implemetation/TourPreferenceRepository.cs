using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TourPreferenceRepository : BaseRepository<TourPreference>, ITourPreferenceRepository
    {
        public TourPreferenceRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
