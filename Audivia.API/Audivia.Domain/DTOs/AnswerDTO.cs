using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class AnswerDTO
    {
        public string Id { get; set; } = null!;
        public string? QuestionId { get; set; }
        public string? Text { get; set; }
        public bool? IsCorrect { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
