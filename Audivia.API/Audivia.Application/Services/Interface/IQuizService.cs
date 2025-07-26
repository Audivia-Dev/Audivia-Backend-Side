using Audivia.Domain.ModelRequests.Quiz;
using Audivia.Domain.ModelResponses.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IQuizService
    {
        Task<QuizReponse> CreateQuizAsync(CreateQuizRequest req);
        Task<QuizReponse> UpdateQuizAsync(string id, UpdateQuizRequest req);
        Task<QuizReponse> DeleteQuizByIdAsync(string id);
        Task<QuizReponse> GetQuizByIdAsync(string id);
        Task<QuizReponse> GetQuizByTourIdAsync(string tourId); 
        Task<QuizListResponse> GetAllQuizsAsync();

    }
}
