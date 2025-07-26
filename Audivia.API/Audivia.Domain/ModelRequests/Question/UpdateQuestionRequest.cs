using Audivia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Question
{
    public class UpdateQuestionRequest
    {
        public string? QuizId { get; set; }

        public QuestionType Type { get; set; } // e.g., "MultipleChoice", "TrueFalse", etc.

        public string? Text { get; set; }
        public double? Points { get; set; }
        public int? Order { get; set; }
        public string? TrueAnswerNote { get; set; }

    }
}
