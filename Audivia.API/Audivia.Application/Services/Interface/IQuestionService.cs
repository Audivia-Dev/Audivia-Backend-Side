using Audivia.Domain.ModelRequests.Question;
using Audivia.Domain.ModelResponses.Question;
using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IQuestionService
    {
        Task<QuestionResponse> CreateQuestionAsync(CreateQuestionRequest request);
        Task<QuestionResponse> UpdateQuestionAsync(string id, UpdateQuestionRequest request);
        Task UpdateQuestion(Question question);
        Task<QuestionResponse> DeleteQuestionAsync(string id);
        Task<QuestionResponse> GetQuestionByIdAsync(string id);
        Task<QuestionListResponse> GetQuestionsByQuizIdAsync(string id);
        Task<QuestionListResponse> GetAllQuestionsAsync();
        Task<Question> GetQuestionModel(string id);
    }
}
