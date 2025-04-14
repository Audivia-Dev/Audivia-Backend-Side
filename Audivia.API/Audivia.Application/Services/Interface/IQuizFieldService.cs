using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.QuizField;
using Audivia.Domain.ModelResponses.QuizField;
using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IQuizFieldService
    {
        Task<QuizFieldResponse> CreateQuizFieldAsync(CreateQuizFieldRequest req);
        Task<QuizFieldResponse> UpdateQuizFieldAsync(string id, UpdateQuizFieldRequest req);
        Task<QuizFieldResponse> DeleteQuizFieldAsync(string id);    
        Task<QuizFieldListResponse> GetQuizFieldListAsync();
        Task<QuizFieldResponse> GetQuizFieldByIdAsync(string id);   

    }
}
