using Audivia.Domain.ModelRequests.Voucher;
using Audivia.Domain.ModelResponses.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IVoucherService
    {
        Task<VoucherResponse> CreateVoucherAsync(CreateVoucherRequest req);
        Task<VoucherResponse> UpdateVoucherAsync(string id, UpdateVoucherRequest req);
        Task<VoucherResponse> DeleteVoucherAsync(string id);
        Task<VoucherListResponse> GetAllVoucherAsync();
        Task<VoucherResponse> GetVoucherByIdAsync(string id);
    }
}
