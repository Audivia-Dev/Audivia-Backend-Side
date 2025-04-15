using Audivia.Domain.Enums;
using Audivia.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class QuestionDTO
    {
        public string Id { get; set; }

        public string? QuizId { get; set; }

        public QuestionType Type { get; set; } // e.g., "MultipleChoice", "TrueFalse", etc.

        public string? Text { get; set; }

        public double? Points { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
