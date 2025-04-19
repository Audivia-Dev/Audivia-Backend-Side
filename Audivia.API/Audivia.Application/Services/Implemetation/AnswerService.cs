using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Answer;
using Audivia.Domain.ModelResponses.Answer;
using Audivia.Domain.ModelResponses.Question;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Implemetation;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class AnswerService : IAnswerService
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerRepository _answerRepository;
        public AnswerService(IQuestionService questionService, IAnswerRepository answerRepository)
        {
            _questionService = questionService;
            _answerRepository = answerRepository;
        }
        public async Task<AnswerResponse> CreateAnswerAsync(CreateAnswerRequest request)
        {
           
            var question = await _questionService.GetQuestionModel(request.QuestionId);
            if (question is null)
            {
                return new AnswerResponse
                {
                    Success = false,
                    Message = "Not found question!",
                    Response = null
                };
            }
            var newAnswer = new Answer
            {
                Text = request.Text,
                IsCorrect = request.IsCorrect,
                QuestionId = request.QuestionId,
                IsDeleted = false,
            };
            
            var rs = await _answerRepository.Create(newAnswer);
            if (rs != null)
            {
                question.Answers.Add(newAnswer);
                await _questionService.UpdateQuestion(question);
            }
            return new AnswerResponse
            {
                Success = true,
                Message = "Created Answer successfully!",
                Response = ModelMapper.MapAnswerToDTO(newAnswer)
            };

        }

        public async Task<AnswerResponse> DeleteAnswerAsync(string id)
        {
            var answer = await _answerRepository.GetById(new ObjectId(id.ToString()));
            if (answer is null)
            {
                return new AnswerResponse
                {
                    Success = false,
                    Message = "Not found answer!",
                    Response = null
                };
            }

            answer.IsDeleted = !answer.IsDeleted;
            await _answerRepository.Update(answer);
            return new AnswerResponse
            {
                Success = true,
                Message = "Deleted Answer successfully!",
                Response = ModelMapper.MapAnswerToDTO(answer)
            };
        }

        public async Task<AnswerListResponse> GetAllAnswer()
        {
            var list = await _answerRepository.GetAll();
            var activeList = list.Where(a => a.IsDeleted == false).ToList();
            var rs = activeList.Select(ModelMapper.MapAnswerToDTO).ToList();
            return new AnswerListResponse
            {
                Success = true,
                Message = "Fetched all Answers successfully!",
                Response = rs
            };
        }

        public async Task<AnswerResponse> GetAnswerByIdAsync(string id)
        {
            var answer = await _answerRepository.GetById(new ObjectId(id.ToString()));
            if (answer is null)
            {
                return new AnswerResponse
                {
                    Success = false,
                    Message = "Not found answer!",
                    Response = null
                };
            }
            return new AnswerResponse
            {
                Success = true,
                Message = "Fectched Answer successfully!",
                Response = ModelMapper.MapAnswerToDTO(answer)
            };
        }

        public async Task<AnswerResponse> UpdateAnswerAsync(string id, UpdateAnswerRequest request)
        {
            var answer = await _answerRepository.GetById(new ObjectId(id.ToString()));
            if (answer is null)
            {
                return new AnswerResponse
                {
                    Success = false,
                    Message = "Not found answer!",
                    Response = null
                };
            }
            answer.Text = request.Text;
            answer.IsCorrect = request.IsCorrect;
            answer.QuestionId = request.QuestionId;
            await _answerRepository.Update(answer);
            return new AnswerResponse
            {
                Success = true,
                Message = "Updated Answer successfully!",
                Response = ModelMapper.MapAnswerToDTO(answer)
            };
        }
    }
}
