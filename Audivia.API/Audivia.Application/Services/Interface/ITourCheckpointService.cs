using Audivia.Domain.ModelRequests.TourCheckpoint;
using Audivia.Domain.ModelResponses.TourCheckpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface ITourCheckpointService
    {
        Task<TourCheckpointResponse> CreateTourCheckpointAsync(CreateTourCheckpointRequest req);
        Task<TourCheckpointResponse> UpdateTourCheckpointAsync(string id, UpdateTourCheckpointRequest req);
        Task<TourCheckpointResponse> DeleteTourCheckpointAsync(string id);
        Task<TourCheckpointListResponse> GetAllTourCheckpointsAsync();
        Task<TourCheckpointResponse> GetTourCheckpointByIdAsync(string id);
    }
}
