using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Route;
using Audivia.Domain.ModelRequests.UserResponse;
using Audivia.Domain.ModelResponses.Route;
using Audivia.Domain.ModelResponses.User;
using Audivia.Domain.ModelResponses.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IUserResponseService
    {
        Task<UserResponseResponse> CreateUserResponseAsync(CreateUserResponseRequest req);
        Task<UserResponseResponse> UpdateUserResponseAsync(string id, UpdateUserResponseRequest req);
        Task<UserResponseResponse> DeleteUserResponseAsync(string id);
        Task<UserResponseListResponse> GetAllUserResponseAsync();
        Task<UserResponseResponse> GetUserResponseByIdAsync(string id);
    }
}
