using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Quiz;
using Audivia.Domain.ModelResponses.Post;
using Audivia.Domain.ModelResponses.Quiz;
using Audivia.Domain.ModelResponses.QuizField;
using Audivia.Domain.Models;
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
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task<QuizReponse> CreateQuizAsync(CreateQuizRequest req)
        {
            var newQuiz = new Quiz
            {
                Title = req.Title,
                QuizFieldId = req.QuizFieldId,
                TourCheckpointId = req.TourCheckpointId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Image = req.Image,
            };
            
            await _quizRepository.Create(newQuiz);
            return new QuizReponse
            {
                Message = "Created quiz successfully!",
                Success = true,
                Response = ModelMapper.MapQuizToDTO(newQuiz),
               
            };
        }

        public async Task<QuizReponse> DeleteQuizByIdAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            var quiz = await _quizRepository.GetById(objectId);
            if (quiz != null)
            {
                quiz.IsDeleted = !quiz.IsDeleted;
                await _quizRepository.Update(quiz);
                return new QuizReponse
                {
                    Message = "Deleted quiz successfully!",
                    Success = true,
                    Response = ModelMapper.MapQuizToDTO(quiz),

                };
            }
            return new QuizReponse
            {
                Message = "Deleted quiz failed!",
                Success = false,
                Response = ModelMapper.MapQuizToDTO(quiz),

            };

        }

        public async Task<QuizListResponse> GetAllQuizsAsync()
        {
            var list = await _quizRepository.GetAll();
            var activeList = list.Where(q => q.IsDeleted == false).ToList();
            var rs = activeList
                .Select(ModelMapper.MapQuizToDTO)
                .ToList();

            return new QuizListResponse
            {
                Message = "Fetch all Quiz successfully!",
                Success = true,
                Response = rs
            };
        }

        public async Task<QuizReponse> GetQuizByIdAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            var quiz = await _quizRepository.GetById(objectId);
            if (quiz != null)
            {
               
                return new QuizReponse
                {
                    Message = "Fetch quiz successfully!",
                    Success = true,
                    Response = ModelMapper.MapQuizToDTO(quiz),

                };

            }
            return new QuizReponse
            {
                Message = "Fetch quiz failed!",
                Success = false,
                Response = ModelMapper.MapQuizToDTO(quiz),

            };


        }

        public async Task<QuizReponse> UpdateQuizAsync(string id, UpdateQuizRequest req)
        {
            var objectId = new ObjectId(id.ToString());
            var quiz = await _quizRepository.GetById(objectId);
            if (quiz != null)
            {
                quiz.Title = req.Title ?? quiz.Title;
                quiz.QuizFieldId = req.QuizFieldId ?? quiz.QuizFieldId;
                quiz.TourCheckpointId = req.TourCheckpointId ?? quiz.TourCheckpointId;   
                quiz.UpdatedAt = DateTime.UtcNow;
                quiz.Image = req.Image ?? quiz.Image;
                return new QuizReponse
                {
                    Message = "Update quiz successfully!",
                    Success = true,
                    Response = ModelMapper.MapQuizToDTO(quiz),

                };

            }
            return new QuizReponse
            {
                Message = "Update quiz failed!",
                Success = false,
                Response = ModelMapper.MapQuizToDTO(quiz),

            };
        }
    }
}
