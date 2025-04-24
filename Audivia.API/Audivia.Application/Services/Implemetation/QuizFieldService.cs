using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.QuizField;
using Audivia.Domain.ModelResponses.QuizField;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class QuizFieldService : IQuizFieldService
    {
        private readonly IQuizFieldRepository _repo;
        public QuizFieldService(IQuizFieldRepository repo)
        {
            _repo = repo;
        }
        public async Task<QuizFieldResponse> CreateQuizFieldAsync(CreateQuizFieldRequest req)
        {
            var newQuizField = new QuizField
            {
                QuizFieldName = req.Name,
                Description = req.Description,
                IsDeleted = false,
            };
            await _repo.Create(newQuizField);
            return new QuizFieldResponse
            {
                Message = "Created QuizField successfully!",
                Success = true,
                Response = ModelMapper.MapQuizFieldToDTO(newQuizField)

            };
        }

        public async Task<QuizFieldResponse> DeleteQuizFieldAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            var quizField = await _repo.GetById(objectId);
            quizField.IsDeleted = !quizField.IsDeleted;
            await _repo.Update(quizField);
            return new QuizFieldResponse
            {
                Message = "Deleted QuizField successfully!",
                Success = true,
                Response = ModelMapper.MapQuizFieldToDTO(quizField)

            };
        }

        public async Task<QuizFieldResponse> GetQuizFieldByIdAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            var quizField = await _repo.GetById(objectId);
            return new QuizFieldResponse
            {
                Message = "Fetch QuizField successfully!",
                Success = true,
                Response = ModelMapper.MapQuizFieldToDTO(quizField)

            };
        }

        public async Task<QuizFieldListResponse> GetQuizFieldListAsync()
        {
            var list = await _repo.GetAllActiveQuizField(); // gọi repo đã lọc sẵn IsDeleted = false

            var quizFieldDTOs = list
                .Select(ModelMapper.MapQuizFieldToDTO)
                .ToList();

            return new QuizFieldListResponse
            {
                Message = "Fetch all QuizField successfully!",
                Success = true,
                Response = quizFieldDTOs
            };
        }

        public async Task<QuizFieldResponse> UpdateQuizFieldAsync(string id, UpdateQuizFieldRequest req)
        {
            var objectId = new ObjectId(id.ToString());
            var quizField = await _repo.GetById(objectId);
            if (quizField is null)
            {
                return new QuizFieldResponse
                {
                    Message = "Update QuizField failed!",
                    Success = false,
                    Response = null

                };
            }
            quizField.QuizFieldName = req.Name ?? quizField.QuizFieldName;
            quizField.Description = req.Description ?? quizField.Description;
            await _repo.Update(quizField);
            return new QuizFieldResponse
            {
                Message = "Update QuizField successfully!",
                Success = true,
                Response = ModelMapper.MapQuizFieldToDTO(quizField)

            };
        }
    }
}
