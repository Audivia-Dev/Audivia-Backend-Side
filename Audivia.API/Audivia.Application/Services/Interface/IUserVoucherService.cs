using Audivia.Domain.ModelRequests.UserVoucher;
using Audivia.Domain.ModelResponses.UserVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IUserVoucherService
    {
        Task<UserVoucherListResponse> GetAllUserVoucherAsync();
        Task<UserVoucherResponse> CreateUserVoucherAsync(CreateUserVoucherRequest req);
        Task<UserVoucherResponse> UpdateUserVoucherAsync(string id, UpdateUserVoucherRequest req);
        Task<UserVoucherResponse> GetUserVoucherByIdAsync(string id);
        Task<UserVoucherResponse> DeleteUserVoucherAsync(string id);
    }
}
