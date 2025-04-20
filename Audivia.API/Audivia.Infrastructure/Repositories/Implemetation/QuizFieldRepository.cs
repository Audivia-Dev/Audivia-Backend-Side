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
    public class QuizFieldRepository : BaseRepository<QuizField>, IQuizFieldRepository
    {
        public QuizFieldRepository(IMongoDatabase database) : base(database)
        {
        }
        public async Task<List<QuizField>> GetAllActiveQuizField()
        {
            var list = Builders<QuizField>.Filter.Eq(x => x.IsDeleted, false);
            return await _collection.Find(list).ToListAsync();
        }
    }
}
