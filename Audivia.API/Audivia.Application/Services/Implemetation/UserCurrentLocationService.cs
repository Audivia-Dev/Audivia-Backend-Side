using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.UserCurrentLocation;
using Audivia.Domain.ModelResponses.UserCurrentLocation;
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
    public class UserCurrentLocationService : IUserCurrentLocationService
    {
        private readonly IUserCurrentLocationRepository _userCurrentLocationRepository;

        public UserCurrentLocationService(IUserCurrentLocationRepository userCurrentLocationRepository)
        {
            _userCurrentLocationRepository = userCurrentLocationRepository;
        }

        public async Task<UserCurrentLocationResponse> CreateUserCurrentLocationAsync(CreateUserCurrentLocationRequest req)
        {
            if (req is null)
            {
                return new UserCurrentLocationResponse
                {
                    Success = false,
                    Message = "Created UserCurrentLocation failed!",
                    Response = null
                };
            }

            var location = new UserCurrentLocation
            {
                UserId = req.UserId,
                Latitude = req.Latitude,
                Longitude = req.Longitude,
                UpdatedAt = DateTime.UtcNow,
                Accuracy = req.Accuracy
            };

            await _userCurrentLocationRepository.Create(location);

            return new UserCurrentLocationResponse
            {
                Success = true,
                Message = "Created UserCurrentLocation successfully!",
                Response = ModelMapper.MapUserCurrentLocationToDTO(location)
            };
        }

        public async Task<UserCurrentLocationResponse> DeleteUserCurrentLocationAsync(string id)
        {
            var location = await _userCurrentLocationRepository.GetById(new ObjectId(id));
            if (location == null)
            {
                return new UserCurrentLocationResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null
                };
            }

            await _userCurrentLocationRepository.Delete(location);

            return new UserCurrentLocationResponse
            {
                Success = true,
                Message = "Deleted successfully!",
                Response = ModelMapper.MapUserCurrentLocationToDTO(location)
            };
        }

        public async Task<UserCurrentLocationListResponse> GetAllUserCurrentLocationAsync()
        {
            var list = await _userCurrentLocationRepository.GetAll();
            var rs = list.Select(ModelMapper.MapUserCurrentLocationToDTO).ToList();

            return new UserCurrentLocationListResponse
            {
                Success = true,
                Message = "Fetched all UserCurrentLocation successfully!",
                Response = rs
            };
        }

        public async Task<UserCurrentLocationResponse> GetUserCurrentLocationByIdAsync(string id)
        {
            var location = await _userCurrentLocationRepository.GetById(new ObjectId(id));
            if (location == null)
            {
                return new UserCurrentLocationResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null
                };
            }

            return new UserCurrentLocationResponse
            {
                Success = true,
                Message = "Fetched UserCurrentLocation successfully!",
                Response = ModelMapper.MapUserCurrentLocationToDTO(location)
            };
        }

        public async Task<UserCurrentLocationResponse> UpdateUserCurrentLocationAsync(string id, UpdateUserCurrentLocationRequest req)
        {
            var location = await _userCurrentLocationRepository.GetById(new ObjectId(id));
            if (location == null)
            {
                return new UserCurrentLocationResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null
                };
            }

            location.Latitude = req.Latitude ?? location.Latitude;
            location.Longitude = req.Longitude ?? location.Longitude;
            location.UpdatedAt = DateTime.UtcNow;
            location.Accuracy = req.Accuracy ?? location.Accuracy;

            await _userCurrentLocationRepository.Update(location);

            return new UserCurrentLocationResponse
            {
                Success = true,
                Message = "Updated UserCurrentLocation successfully!",
                Response = ModelMapper.MapUserCurrentLocationToDTO(location)
            };
        }
    }

}
