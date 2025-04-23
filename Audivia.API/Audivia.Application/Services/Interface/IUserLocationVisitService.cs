using Audivia.Domain.ModelRequests.UserLocationVisit;
using Audivia.Domain.ModelResponses.UserLocationVisit;

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
