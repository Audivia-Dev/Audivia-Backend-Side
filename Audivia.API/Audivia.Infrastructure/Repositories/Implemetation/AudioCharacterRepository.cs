using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class AudioCharacterRepository : BaseRepository<AudioCharacter>, IAudioCharacterRepository
    {
        public AudioCharacterRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
