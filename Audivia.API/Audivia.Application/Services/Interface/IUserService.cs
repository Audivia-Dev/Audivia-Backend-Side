using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IUserService 
    {
        Task<UserResponse> CreateAccount(UserCreateRequest request);
      
    }
}
