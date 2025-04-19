using Audivia.Domain.ModelRequests.Route;
using Audivia.Domain.ModelRequests.UserLocationVisit;
using Audivia.Domain.ModelResponses.Route;
using Audivia.Domain.ModelResponses.UserLocationVisit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IUserLocationVisitService
    {
        Task<UserLocationVisitResponse> CreateUserLocationVisitAsync(CreateUserLocationVisitRequest req);
        Task<UserLocationVisitResponse> UpdateUserLocationVisitAsync(string id, UpdateUserLocationVisitRequest req);
        Task<UserLocationVisitResponse> DeleteUserLocationVisitAsync(string id);
        Task<UserLocationVisitListResponse> GetAllUserLocationVisitAsync();
        Task<UserLocationVisitResponse> GetUserLocationVisitByIdAsync(string id);
    }
}
