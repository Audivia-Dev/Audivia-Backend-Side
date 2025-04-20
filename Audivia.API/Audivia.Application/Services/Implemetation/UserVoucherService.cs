using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.UserVoucher;
using Audivia.Domain.ModelResponses.UserVoucher;
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
    public class UserVoucherService : IUserVoucherService
    {
        private readonly IUserVoucherRepository _userVoucherRepository;
        public UserVoucherService(IUserVoucherRepository repo)
        {
            _userVoucherRepository = repo;
        }

        public async Task<UserVoucherResponse> CreateUserVoucherAsync(CreateUserVoucherRequest req)
        {
            if (req is null)
            {
                return new UserVoucherResponse { Success = false, Message = "Create failed", Response = null };
            }

            var model = new UserVoucher
            {
                UserId = req.UserId,
                VoucherId = req.VoucherId,
                UsedAt = req.UsedAt,
                IsDeleted = false
            };
            await _userVoucherRepository.Create(model);

            return new UserVoucherResponse
            {
                Success = true,
                Message = "Created successfully",
                Response = ModelMapper.MapUserVoucherToDTO(model)
            };
        }

        public async Task<UserVoucherResponse> UpdateUserVoucherAsync(string id, UpdateUserVoucherRequest req)
        {
            var model = await _userVoucherRepository.GetById(new ObjectId(id));
            if (model == null)
                return new UserVoucherResponse { Success = false, Message = "Not found", Response = null };

            model.UserId = req.UserId;
            model.VoucherId = req.VoucherId;
            model.UsedAt = req.UsedAt;

            await _userVoucherRepository.Update(model);
            return new UserVoucherResponse
            {
                Success = true,
                Message = "Updated successfully",
                Response = ModelMapper.MapUserVoucherToDTO(model)
            };
        }

        public async Task<UserVoucherResponse> DeleteUserVoucherAsync(string id)
        {
            var model = await _userVoucherRepository.GetById(new ObjectId(id));
            if (model == null)
                return new UserVoucherResponse { Success = false, Message = "Not found", Response = null };

            model.IsDeleted = true;
            await _userVoucherRepository.Update(model);

            return new UserVoucherResponse
            {
                Success = true,
                Message = "Deleted successfully",
                Response = ModelMapper.MapUserVoucherToDTO(model)
            };
        }

        public async Task<UserVoucherListResponse> GetAllUserVoucherAsync()
        {
            var list = await _userVoucherRepository.GetAll();
            var active = list.Where(x => x.IsDeleted != true).Select(ModelMapper.MapUserVoucherToDTO).ToList();

            return new UserVoucherListResponse
            {
                Success = true,
                Message = "Fetched all successfully",
                Response = active
            };
        }

        public async Task<UserVoucherResponse> GetUserVoucherByIdAsync(string id)
        {
            var model = await _userVoucherRepository.GetById(new ObjectId(id));
            if (model == null)
                return new UserVoucherResponse { Success = false, Message = "Not found", Response = null };

            return new UserVoucherResponse
            {
                Success = true,
                Message = "Fetched successfully",
                Response = ModelMapper.MapUserVoucherToDTO(model)
            };
        }
    }
}
