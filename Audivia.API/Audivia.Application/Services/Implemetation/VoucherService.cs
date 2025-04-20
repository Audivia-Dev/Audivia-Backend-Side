using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Voucher;
using Audivia.Domain.ModelResponses.Voucher;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        public VoucherService(IVoucherRepository repo)
        {
            _voucherRepository = repo;
        }

        public async Task<VoucherResponse> CreateVoucherAsync(CreateVoucherRequest req)
        {
            if (req is null)
                return new VoucherResponse { Success = false, Message = "Create failed", Response = null };

            var voucher = new Voucher
            {
                Code = req.Code,
                Discount = req.Discount,
                Title = req.Title,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = req.ExpiryDate,
                IsDeleted = false
            };
            await _voucherRepository.Create(voucher);

            return new VoucherResponse
            {
                Success = true,
                Message = "Created successfully",
                Response = ModelMapper.MapVoucherToDTO(voucher)
            };
        }

        public async Task<VoucherResponse> UpdateVoucherAsync(string id, UpdateVoucherRequest req)
        {
            var voucher = await _voucherRepository.GetById(new ObjectId(id));
            if (voucher == null)
                return new VoucherResponse { Success = false, Message = "Not found", Response = null };

            voucher.Code = req.Code;
            voucher.Discount = req.Discount;
            voucher.Title = req.Title;
            voucher.ExpiryDate = req.ExpiryDate;

            await _voucherRepository.Update(voucher);
            return new VoucherResponse
            {
                Success = true,
                Message = "Updated successfully",
                Response = ModelMapper.MapVoucherToDTO(voucher)
            };
        }

        public async Task<VoucherResponse> DeleteVoucherAsync(string id)
        {
            var voucher = await _voucherRepository.GetById(new ObjectId(id));
            if (voucher == null)
                return new VoucherResponse { Success = false, Message = "Not found", Response = null };

            voucher.IsDeleted = true;
            await _voucherRepository.Update(voucher);

            return new VoucherResponse
            {
                Success = true,
                Message = "Deleted successfully",
                Response = ModelMapper.MapVoucherToDTO(voucher)
            };
        }

        public async Task<VoucherListResponse> GetAllVoucherAsync()
        {
            var list = await _voucherRepository.GetAll();
            var active = list.Where(x => x.IsDeleted != true).Select(ModelMapper.MapVoucherToDTO).ToList();

            return new VoucherListResponse
            {
                Success = true,
                Message = "Fetched all successfully",
                Response = active
            };
        }

        public async Task<VoucherResponse> GetVoucherByIdAsync(string id)
        {
            var voucher = await _voucherRepository.GetById(new ObjectId(id));
            if (voucher == null)
                return new VoucherResponse { Success = false, Message = "Not found", Response = null };

            return new VoucherResponse
            {
                Success = true,
                Message = "Fetched successfully",
                Response = ModelMapper.MapVoucherToDTO(voucher)
            };
        }
    }
}
