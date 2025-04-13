using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;
using Audivia.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<UserResponse> CreateAccount(UserCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
