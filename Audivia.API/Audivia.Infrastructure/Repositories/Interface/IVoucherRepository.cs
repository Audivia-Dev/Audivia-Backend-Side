using Audivia.Domain.DTOs;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IVoucherRepository : IBaseRepository<Voucher>, IDisposable
    {
    }
}
