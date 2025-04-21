using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class SavedTourRepository : BaseRepository<SavedTour>, ISavedTourRepository
    {
        public SavedTourRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
