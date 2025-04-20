using Audivia.Domain.ModelRequests.UserCurrentLocation;
using Audivia.Domain.ModelRequests.UserLocationVisit;
using Audivia.Domain.ModelRequests.UserResponse;
using Audivia.Domain.ModelResponses.UserCurrentLocation;
using Audivia.Domain.ModelResponses.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
