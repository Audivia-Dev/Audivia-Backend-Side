using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TourCheckpoint;
using Audivia.Domain.ModelResponses.TourCheckpoint;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class TourCheckpointService : ITourCheckpointService
    {
        private readonly ITourCheckpointRepository _repository;
        private readonly ICheckpointAudioRepository _audioRepository;
        private readonly ICheckpointImageRepository _imageRepository;
        public TourCheckpointService(ITourCheckpointRepository repository, ICheckpointAudioRepository audioRepository, ICheckpointImageRepository checkpointImageRepository)
        {
            _repository = repository;
            _audioRepository = audioRepository;
            _imageRepository = checkpointImageRepository;
        }

        public async Task<TourCheckpointResponse> CreateTourCheckpointAsync(CreateTourCheckpointRequest req)
        {
            var model = new TourCheckpoint
            {
                TourId = req.TourId,
                Title = req.Title,
                Description = req.Description,
                Latitude = req.Latitude,
                Longitude = req.Longitude,
                Order = req.Order,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
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

            model.TourId = req.TourId ?? model.TourId;
            model.Title = req.Title ?? model.Title;
            model.Description = req.Description ?? model.Description;
            model.Latitude = req.Latitude ?? model.Latitude;
            model.Longitude = req.Longitude ?? model.Longitude;
            model.Order = req.Order ?? model.Order;
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

            // get images
            FilterDefinition<CheckpointImage>? imageFilter = Builders<CheckpointImage>.Filter.Eq(i => i.TourCheckpointId, model.Id);
            var images = await _imageRepository.Search(imageFilter);
            model.Images = images;

            // get audios
            FilterDefinition<CheckpointAudio>? audioFilter = Builders<CheckpointAudio>.Filter.Eq(i => i.TourCheckpointId, model.Id);
            var audios = await _audioRepository.Search(audioFilter);
            model.Audios = audios;

            return new TourCheckpointResponse
            {
                Success = true,
                Message = "Fetched tour checkpoint successfully!",
                Response = ModelMapper.MapTourCheckpointToDTO(model)
            };
        }

        public async Task<List<TourCheckpoint>> GetTourCheckpointsAsync(FilterDefinition<TourCheckpoint>? filter)
        {
            var list = await _repository.Search(filter);
            var activeList = list.Where(x => !x.IsDeleted).ToList();
            foreach(TourCheckpoint tc in activeList)
            {
                FilterDefinition<CheckpointImage>? imageFilter = Builders<CheckpointImage>.Filter.Eq(i => i.TourCheckpointId, tc.Id);
                var images = await _imageRepository.Search(imageFilter);
                tc.Images = images;
            }
            return activeList;
        }

        public async Task<List<TourCheckpointDTO>> GetTourCheckpointsByTourId(string tourId)
        {
           var rs = await _repository.GetTourCheckpointsByTourId(tourId);
           return rs.Select(ModelMapper.MapTourCheckpointToDTO)
                    .ToList();
        }
    }
}

