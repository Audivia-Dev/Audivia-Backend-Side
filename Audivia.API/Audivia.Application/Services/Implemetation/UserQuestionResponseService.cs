using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.UserQuestionResponse;
using Audivia.Domain.ModelResponses.UserQuestionResponse;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Org.BouncyCastle.Ocsp;

namespace Audivia.Application.Services.Implemetation
{
    public class UserQuestionResponseService : IUserQuestionResponseService
    {
        private readonly IUserQuizResponseRepository _userQuizResponseRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuizRepository _quizRepository;
        public UserQuestionResponseService(IUserQuizResponseRepository userResponseRepository, IAnswerRepository answerRepository, IQuizRepository quizRepository)
        {
            _userQuizResponseRepository = userResponseRepository;
            _answerRepository = answerRepository;
            _quizRepository = quizRepository;
        }
        public async Task<UserQuestionResponseResponse> CreateUserQuestionResponseAsync(CreateUserQuestionResponseRequest req)
        {
            var existedQuizResponse = await _userQuizResponseRepository.FindFirst(uq => uq.QuizId == req.QuizId);

            if (existedQuizResponse != null && existedQuizResponse.IsDone)
            {
                throw new NotSupportedException("This user completed this quiz before!");
            }

            var newQuestionResponse = new UserQuestionResponse { UserId = req.UserId, QuestionId = req.QuestionId, AnswerId = req.AnswerId, QuizId = req.QuizId, IsCorrect = false };

            // if this is the first answer for the quiz:
            if (existedQuizResponse == null)
            {
                UserQuizResponse newQuizResponse = new UserQuizResponse { UserId = req.UserId, QuizId = req.QuizId, CorrectAnswersCount = 0, RespondedAt = DateTime.UtcNow };
                await _userQuizResponseRepository.Create(await HandleUserQuizResponseAsync(newQuizResponse, newQuestionResponse));
            }

            else
            {
                await _userQuizResponseRepository.Update(await HandleUserQuizResponseAsync(existedQuizResponse, newQuestionResponse));
            }
            return new UserQuestionResponseResponse
            {
                Success = true,
                Message = "Created user question response successfully!",
                Response = ModelMapper.MapUserQuestionResponseToDTO(newQuestionResponse),

            };
        }

        private async Task<UserQuizResponse> HandleUserQuizResponseAsync(UserQuizResponse userQuizResponse, UserQuestionResponse newQuestionResponse)
        {
            // check answer
            bool isCorrectAnswer = await CheckAnswer(newQuestionResponse.QuestionId, newQuestionResponse.AnswerId);
            if (isCorrectAnswer)
            {
                newQuestionResponse.IsCorrect = true;
                userQuizResponse.CorrectAnswersCount++;
            }

            userQuizResponse.QuestionAnswers.Add(newQuestionResponse);
            
            var quiz = await _quizRepository.FindFirst(q => q.Id == userQuizResponse.QuizId);
            if (userQuizResponse.QuestionAnswers.Count == quiz.QuestionsCount)
            {
                userQuizResponse.IsDone = true;
            }
            return userQuizResponse;
        }

        private async Task<bool> CheckAnswer(string questionId, string userChosenAnswerId)
        {
            var questionCorrectAnswer = await _answerRepository.FindFirst(a => a.QuestionId == questionId && a.IsCorrect == true);
            return (questionCorrectAnswer.Id == userChosenAnswerId);
        }
    }
}
