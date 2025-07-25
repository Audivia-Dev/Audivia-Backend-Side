namespace Audivia.Domain.ModelRequests.Quiz
{
    public class CreateQuizRequest
    {

        public string? QuizFieldId { get; set; } = "6882e474f30d6df86e734a4b"; // default quiz field
        public string TourId { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public int QuestionsCount { get; set; } = 0;
    }
}
