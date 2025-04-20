using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IMongoDatabase database) : base(database)
        {

        }
        public async Task<List<Question>> GetAllWithAnswersAsync()
        {
            var rawResults = await _collection.Aggregate()
            .Lookup("answers", "_id", "question_id", "answers")
            .ToListAsync();

            foreach (var doc in rawResults)
            {
                Console.WriteLine(doc.ToJson()); // Check xem field `answers` có được đính kèm không
            }
            return await _collection.Aggregate()
                .Lookup(
                    foreignCollectionName: "answers",
                    localField: "_id",
                    foreignField: "question_id",
                    @as: "answers"
                )
                .As<Question>()  // Quan trọng: Map kết quả sang kiểu Question
                .ToListAsync();
        }


    }
}
