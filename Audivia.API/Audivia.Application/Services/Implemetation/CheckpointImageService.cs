using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.CheckpointImage;
using Audivia.Domain.ModelResponses.CheckpointImage;
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
    public class CheckpointImageService : ICheckpointImageService
    {
        private readonly ICheckpointImageRepository _checkpointImageRepository;

        public CheckpointImageService(ICheckpointImageRepository checkpointImageRepository)
        {
            _checkpointImageRepository = checkpointImageRepository;
        }

        public async Task<CheckpointImageResponse> CreateCheckpointImageAsync(CreateCheckpointImageRequest req)
        {
            if (req is null)
            {
                return new CheckpointImageResponse
                {
                    Success = false,
                    Message = "Created checkpoint image failed!",
                    Response = null,
                };
            }

            var model = new CheckpointImage
            {
                TourCheckpointId = req.TourCheckpointId,
                ImageUrl = req.ImageUrl,
                Description = req.Description,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _checkpointImageRepository.Create(model);

            return new CheckpointImageResponse
            {
                Success = true,
                Message = "Created checkpoint image successfully!",
                Response = ModelMapper.MapCheckpointImageToDTO(model),
            };
        }

        public async Task<CheckpointImageResponse> UpdateCheckpointImageAsync(string id, UpdateCheckpointImageRequest req)
        {
            var model = await _checkpointImageRepository.GetById(new ObjectId(id));

            if (model is null)
            {
                return new CheckpointImageResponse
                {
                    Success = false,
                    Message = "Checkpoint image not found!",
                    Response = null,
                };
            }

            model.TourCheckpointId = req.TourCheckpointId ?? model.TourCheckpointId;
            model.ImageUrl = req.ImageUrl ?? model.TourCheckpointId;
            model.Description = req.Description ?? model.Description;
            model.UpdatedAt = DateTime.UtcNow;

            await _checkpointImageRepository.Update(model);

            return new CheckpointImageResponse
            {
                Success = true,
                Message = "Updated checkpoint image successfully!",
                Response = ModelMapper.MapCheckpointImageToDTO(model),
            };
        }

        public async Task<CheckpointImageResponse> DeleteCheckpointImageAsync(string id)
        {
            var model = await _checkpointImageRepository.GetById(new ObjectId(id));

            if (model is null)
            {
                return new CheckpointImageResponse
                {
                    Success = false,
                    Message = "Checkpoint image not found!",
                    Response = null,
                };
            }

            model.IsDeleted = true;
            model.UpdatedAt = DateTime.UtcNow;

            await _checkpointImageRepository.Update(model);

            return new CheckpointImageResponse
            {
                Success = true,
                Message = "Deleted checkpoint image successfully!",
                Response = ModelMapper.MapCheckpointImageToDTO(model),
            };
        }

        public async Task<CheckpointImageListResponse> GetAllCheckpointImagesAsync()
        {
            var list = await _checkpointImageRepository.GetAll();
            var activeList = list.Where(x => !x.IsDeleted).ToList();
            var dtoList = activeList.Select(ModelMapper.MapCheckpointImageToDTO).ToList();

            return new CheckpointImageListResponse
            {
                Success = true,
                Message = "Fetched all checkpoint images successfully!",
                Response = dtoList
            };
        }

        public async Task<CheckpointImageResponse> GetCheckpointImageByIdAsync(string id)
        {
            var model = await _checkpointImageRepository.GetById(new ObjectId(id));

            if (model is null || model.IsDeleted)
            {
                return new CheckpointImageResponse
                {
                    Success = false,
                    Message = "Checkpoint image not found!",
                    Response = null,
                };
            }

            return new CheckpointImageResponse
            {
                Success = true,
                Message = "Fetched checkpoint image successfully!",
                Response = ModelMapper.MapCheckpointImageToDTO(model),
            };
        }
    }
}


