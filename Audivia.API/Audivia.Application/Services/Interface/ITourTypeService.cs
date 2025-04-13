using Audivia.Domain.ModelRequests.TourType;
using Audivia.Domain.ModelResponses.TourType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface ITourTypeService
    {
        Task<TourTypeResponse> GetTourTypeInformation(string id);
        Task<TourTypeResponse> CreateTourType(CreateTourTypeRequest request);
        Task UpdateTourType(string id, UpdateTourTypeRequest request);
        Task DeleteTourType(string id);
        Task<TourTypeListResponse> GetAllActiveTourTypes();
    }
}
