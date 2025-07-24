using Audivia.Domain.ModelRequests.UserCurrentLocation;
using Audivia.Domain.ModelResponses.UserCurrentLocation;

namespace Audivia.Application.Services.Interface
{
    public interface IUserCurrentLocationService
    {
        Task<UserCurrentLocationResponse> CreateUserCurrentLocationAsync(CreateUserCurrentLocationRequest req);
        Task<UserCurrentLocationResponse> UpdateUserCurrentLocationAsync(string id, UpdateUserCurrentLocationRequest req);
        Task<UserCurrentLocationResponse> DeleteUserCurrentLocationAsync(string id);
        Task<UserCurrentLocationListResponse> GetAllUserCurrentLocationAsync();
        Task<UserCurrentLocationResponse> GetUserCurrentLocationByIdAsync(string id);
    }
}
