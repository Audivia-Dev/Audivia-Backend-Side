using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TourRepository : BaseRepository<Tour>, ITourRepository
    {
        public TourRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
