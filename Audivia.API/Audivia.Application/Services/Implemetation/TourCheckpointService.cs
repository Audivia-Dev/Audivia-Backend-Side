using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.TourCheckpoint;
using Audivia.Domain.ModelResponses.TourCheckpoint;
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
    public class TourCheckpointService : ITourCheckpointService
    {
        private readonly ITourCheckpointRepository _repository;

        public TourCheckpointService(ITourCheckpointRepository repository)
        {
            _repository = repository;
        }

        public async Task<TourCheckpointResponse> CreateTourCheckpointAsync(CreateTourCheckpointRequest req)
        {
            var model = new TourCheckpoint
            {
                TourId = req.TourId,
                RouteId = req.RouteId,
                Title = req.Title,
                Description = req.Description,
                Latitude = req.Latitude,
                Longitude = req.Longitude,
                Order = req.Order,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repository.Create(model);

            return new TourCheckpointResponse
            {
                Success = true,
                Message = "Created tour checkpoint successfully!",
                Response = ModelMapper.MapTourCheckpointToDTO(model)
            };
        }

        public async Task<TourCheckpointResponse> UpdateTourCheckpointAsync(string id, UpdateTourCheckpointRequest req)
        {
            var model = await _repository.GetById(new ObjectId(id));
            if (model == null)
            {
                return new TourCheckpointResponse
                {
                    Success = false,
                    Message = "Tour checkpoint not found!"
                };
            }

            model.TourId = req.TourId;
            model.RouteId = req.RouteId;
            model.Title = req.Title;
            model.Description = req.Description;
            model.Latitude = req.Latitude;
            model.Longitude = req.Longitude;
            model.Order = req.Order;
            model.UpdatedAt = DateTime.UtcNow;

            await _repository.Update(model);

            return new TourCheckpointResponse
            {
                Success = true,
                Message = "Updated tour checkpoint successfully!",
                Response = ModelMapper.MapTourCheckpointToDTO(model)
            };
        }

        public async Task<TourCheckpointResponse> DeleteTourCheckpointAsync(string id)
        {
            var model = await _repository.GetById(new ObjectId(id));
            if (model == null)
            {
                return new TourCheckpointResponse
                {
                    Success = false,
                    Message = "Tour checkpoint not found!"
                };
            }

            model.IsDeleted = true;
            await _repository.Update(model);

            return new TourCheckpointResponse
            {
                Success = true,
                Message = "Deleted tour checkpoint successfully!",
                Response = ModelMapper.MapTourCheckpointToDTO(model)
            };
        }

        public async Task<TourCheckpointListResponse> GetAllTourCheckpointsAsync()
        {
            var list = await _repository.GetAll();
            var activeList = list.Where(x => !x.IsDeleted).Select(ModelMapper.MapTourCheckpointToDTO).ToList();

            return new TourCheckpointListResponse
            {
                Success = true,
                Message = "Fetched all tour checkpoints successfully!",
                Response = activeList
            };
        }

        public async Task<TourCheckpointResponse> GetTourCheckpointByIdAsync(string id)
        {
            var model = await _repository.GetById(new ObjectId(id));
            if (model == null)
            {
                return new TourCheckpointResponse
                {
                    Success = false,
                    Message = "Tour checkpoint not found!"
                };
            }

            return new TourCheckpointResponse
            {
                Success = true,
                Message = "Fetched tour checkpoint successfully!",
                Response = ModelMapper.MapTourCheckpointToDTO(model)
            };
        }
    }
}

