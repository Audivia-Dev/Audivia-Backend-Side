using Audivia.Domain.Models;
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
    public class UserVoucherRepository : BaseRepository<UserVoucher>, IUserVoucherRepository
    {
        public UserVoucherRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
