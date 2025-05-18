using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TourCheckpoint;
using Audivia.Domain.ModelResponses.TourCheckpoint;
using Audivia.Domain.Models;
using MongoDB.Driver;

namespace Audivia.Application.Services.Interface
{
    public interface ITourCheckpointService
    {
        Task<TourCheckpointResponse> CreateTourCheckpointAsync(CreateTourCheckpointRequest req);
        Task<TourCheckpointResponse> UpdateTourCheckpointAsync(string id, UpdateTourCheckpointRequest req);
        Task<TourCheckpointResponse> DeleteTourCheckpointAsync(string id);
        Task<TourCheckpointListResponse> GetAllTourCheckpointsAsync();
        Task<List<TourCheckpoint>> GetTourCheckpointsAsync(FilterDefinition<TourCheckpoint>? filter);
        Task<TourCheckpointResponse> GetTourCheckpointByIdAsync(string id);
        Task<List<TourCheckpointDTO>> GetTourCheckpointsByTourId(string tourId);
    }
}
