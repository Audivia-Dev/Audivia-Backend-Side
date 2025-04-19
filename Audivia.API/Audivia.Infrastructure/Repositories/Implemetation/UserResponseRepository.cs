using Audivia.Domain.ModelResponses.User;
using Audivia.Infrastructure.Interface;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class UserResponseRepository : BaseRepository<Domain.Models.UserResponse>, IUserResponseRepository
    {
        public UserResponseRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
