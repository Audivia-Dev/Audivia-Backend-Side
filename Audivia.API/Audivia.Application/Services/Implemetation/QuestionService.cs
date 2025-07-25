using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Question;
using Audivia.Domain.ModelResponses.Question;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Implemetation;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        public QuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }
        public async Task<QuestionResponse> CreateQuestionAsync(CreateQuestionRequest request)
        {
            var newQuestion = new Question
            {
                Type = request.Type,
                Text = request.Text,
                Points = request.Points,
                QuizId = request.QuizId,
                Order = request.Order,
                CreatedAt = DateTime.UtcNow,
            };
            await _questionRepository.Create(newQuestion);
            return new QuestionResponse
            {
                Success = true,
                Message = "Created Question successfully!",
                Response = ModelMapper.MapQuestionToDTO(newQuestion)
            };
        }

        public async Task<QuestionResponse> DeleteQuestionAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            var question = await _questionRepository.GetById(objectId);
            if (question is null)
            {
                return new QuestionResponse
                {
                    Success = false,
                    Message = "Not found question!",
                    Response = null
                };
            }
            await _questionRepository.Delete(question);
            return new QuestionResponse
            {
                Success = true,
                Message = "Deleted question successfully!",
                Response = ModelMapper.MapQuestionToDTO(question)
            };
        }

        public async Task<QuestionListResponse> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllWithAnswersAsync();
            var activeQuestions = questions.Where(q => !q.IsDeleted).ToList();
            var rs = activeQuestions.Select(ModelMapper.MapQuestionToDTO).ToList();
            return new QuestionListResponse
            {
                Success = true,
                Message = "Fetched all questions successfully!",
                Response = rs
            };
        }


        public async Task<QuestionResponse> GetQuestionByIdAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            var question = await _questionRepository.GetById(objectId);
            FilterDefinition<Answer> filterDefinition = Builders<Answer>.Filter.Eq(i => i.QuestionId, id);
            var answers = await _answerRepository.Search(filterDefinition);
            question.Answers = (List<Answer>?)answers;
            if (question is null)
            {
                return new QuestionResponse
                {
                    Success = false,
                    Message = "Not found question!",
                    Response = null
                };
            }
            return new QuestionResponse
            {
                Success = true,
                Message = "Fetch question successfully!",
                Response = ModelMapper.MapQuestionToDTO(question)
            };
        }

        public async Task<QuestionResponse> GetQuestionByQuizIdAndOrderAsync(string quizId, int order)
        {
            var question = await _questionRepository.FindFirst(q => q.QuizId == quizId && q.Order == order);

            if (question is null)
            {
                return new QuestionResponse
                {
                    Success = false,
                    Message = "Not found question!",
                    Response = null
                };
            }

            // get answers
            FilterDefinition<Answer> filterDefinition = Builders<Answer>.Filter.Eq(i => i.QuestionId, question.Id);
            var answers = await _answerRepository.Search(filterDefinition);
            question.Answers = (List<Answer>?)answers;
           
            
            return new QuestionResponse
            {
                Success = true,
                Message = "Fetch question successfully!",
                Response = ModelMapper.MapQuestionToDTO(question)
            };
        }

        public async Task<Question> GetQuestionModel(string id)
        {
            return await _questionRepository.GetById(new ObjectId(id.ToString()));
        }

        public async Task<QuestionListResponse> GetQuestionsByQuizIdAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            FilterDefinition<Question> filterDefinition = Builders<Question>.Filter.Eq(i => i.QuizId, id);
            var questions = await _questionRepository.Search(filterDefinition);

            return new QuestionListResponse
            {
                Success = true,
                Message = "Fetched all questions successfully!",
                Response = questions.Select(ModelMapper.MapQuestionToDTO).ToList()
            };
        }

        public async Task UpdateQuestion(Question question)
        {
            //var q = await _questionRepository.GetById(new ObjectId(id.ToString()));
            //if (question.Answers != null) 
            //{
            //    q.Answers = question.Answers;
            //} 
            //q.Answers = question.Answers;
            await _questionRepository.Update(question);
            
        }

        public async Task<QuestionResponse> UpdateQuestionAsync(string id, UpdateQuestionRequest request)
        {
            var objectId = new ObjectId(id.ToString()); 
            var question = await _questionRepository.GetById(objectId);
            if (question is null)
            {
                return new QuestionResponse
                {
                    Success = false,
                    Message = "Not found question!",
                    Response = null
                };
            }
            question.Text = request.Text ?? question.Text;   
            question.Points = request.Points ?? question.Points;
            question.QuizId = request.QuizId ?? question.QuizId;
            question.Order = request.Order ?? question.Order;
            question.UpdatedAt = DateTime.UtcNow;
            await _questionRepository.Update(question);
            return new QuestionResponse
            {
                Success = true,
                Message = "Updated Question successfully!",
                Response = ModelMapper.MapQuestionToDTO(question)
            };
        }
    }
}
