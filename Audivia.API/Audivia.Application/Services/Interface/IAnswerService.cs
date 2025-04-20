using Audivia.Domain.ModelRequests.Answer;
using Audivia.Domain.ModelResponses.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IAnswerService 
    {
        Task<AnswerListResponse> GetAllAnswer();
        Task<AnswerResponse> CreateAnswerAsync(CreateAnswerRequest request);
        Task<AnswerResponse> UpdateAnswerAsync(string id, UpdateAnswerRequest request);
        Task<AnswerResponse> DeleteAnswerAsync(string id);
        Task<AnswerResponse> GetAnswerByIdAsync(string id); 

    }
}
