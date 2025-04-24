using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.UserLocationVisit;
using Audivia.Domain.ModelResponses.UserLocationVisit;
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
    public class UserLocationVisitService : IUserLocationVisitService
    {
        private readonly IUserLocationVisitRepository _userLocationVisitRepository;

        public UserLocationVisitService(IUserLocationVisitRepository userLocationVisitRepository)
        {
            _userLocationVisitRepository = userLocationVisitRepository;
        }

        public async Task<UserLocationVisitResponse> CreateUserLocationVisitAsync(CreateUserLocationVisitRequest req)
        {
            if (req is null)
            {
                return new UserLocationVisitResponse
                {
                    Success = false,
                    Message = "Created visit failed!",
                    Response = null,
                };
            }

            var visit = new UserLocationVisit
            {
                UserId = req.UserId,
                TourcheckpointId = req.TourcheckpointId,
                VisitedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userLocationVisitRepository.Create(visit);

            return new UserLocationVisitResponse
            {
                Success = true,
                Message = "Created visit successfully!",
                Response = ModelMapper.MapUserLocationVisitToDTO(visit)
            };
        }

        public async Task<UserLocationVisitResponse> UpdateUserLocationVisitAsync(string id, UpdateUserLocationVisitRequest req)
        {
            var visit = await _userLocationVisitRepository.GetById(new ObjectId(id));
            if (visit is null)
            {
                return new UserLocationVisitResponse
                {
                    Success = false,
                    Message = "Visit not found!",
                    Response = null
                };
            }

            visit.UserId = req.UserId ?? visit.UserId;
            visit.TourcheckpointId = req.TourcheckpointId ?? visit.TourcheckpointId;
            visit.VisitedAt = DateTime.UtcNow;

            await _userLocationVisitRepository.Update(visit);

            return new UserLocationVisitResponse
            {
                Success = true,
                Message = "Updated visit successfully!",
                Response = ModelMapper.MapUserLocationVisitToDTO(visit)
            };
        }

        public async Task<UserLocationVisitResponse> DeleteUserLocationVisitAsync(string id)
        {
            var visit = await _userLocationVisitRepository.GetById(new ObjectId(id));
            if (visit is null)
            {
                return new UserLocationVisitResponse
                {
                    Success = false,
                    Message = "Visit not found!",
                    Response = null
                };
            }

            visit.IsDeleted = true;
            await _userLocationVisitRepository.Update(visit);

            return new UserLocationVisitResponse
            {
                Success = true,
                Message = "Deleted visit successfully!",
                Response = ModelMapper.MapUserLocationVisitToDTO(visit)
            };
        }

        public async Task<UserLocationVisitListResponse> GetAllUserLocationVisitAsync()
        {
            var list = await _userLocationVisitRepository.GetAll();
            var activeList = list.Where(v => v.IsDeleted == false).ToList();
            var rs = activeList.Select(ModelMapper.MapUserLocationVisitToDTO).ToList();

            return new UserLocationVisitListResponse
            {
                Success = true,
                Message = "Fetched all visits successfully!",
                Response = rs
            };
        }

        public async Task<UserLocationVisitResponse> GetUserLocationVisitByIdAsync(string id)
        {
            var visit = await _userLocationVisitRepository.GetById(new ObjectId(id));
            if (visit is null)
            {
                return new UserLocationVisitResponse
                {
                    Success = false,
                    Message = "Visit not found!",
                    Response = null
                };
            }

            return new UserLocationVisitResponse
            {
                Success = true,
                Message = "Fetched visit successfully!",
                Response = ModelMapper.MapUserLocationVisitToDTO(visit)
            };
        }
    }
}
