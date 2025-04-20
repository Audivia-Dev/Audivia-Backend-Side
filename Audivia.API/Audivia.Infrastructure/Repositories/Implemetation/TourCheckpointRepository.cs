using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TourCheckpointRepository : BaseRepository<TourCheckpoint>, ITourCheckpointRepository
    {
        public TourCheckpointRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
