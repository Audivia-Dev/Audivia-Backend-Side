using Audivia.Domain.ModelRequests.CheckpointImage;
using Audivia.Domain.ModelResponses.CheckpointImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface ICheckpointImageService
    {
        Task<CheckpointImageResponse> CreateCheckpointImageAsync(CreateCheckpointImageRequest req);
        Task<CheckpointImageResponse> UpdateCheckpointImageAsync(string id, UpdateCheckpointImageRequest req);
        Task<CheckpointImageResponse> DeleteCheckpointImageAsync(string id);
        Task<CheckpointImageListResponse> GetAllCheckpointImagesAsync();
        Task<CheckpointImageResponse> GetCheckpointImageByIdAsync(string id);
    }
}
