using Audivia.Domain.Enums;

namespace Audivia.Domain.ModelRequests.Question
{
    public class CreateQuestionRequest
    {
        public string? QuizId { get; set; }

        public QuestionType Type { get; set; } // e.g., "MultipleChoice", "TrueFalse", etc.

        public string? Text { get; set; }
        public double? Points { get; set; }
        public int Order { get; set; }
        public string? TrueAnswerNote { get; set; }

    }
}
