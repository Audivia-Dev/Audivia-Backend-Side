using Audivia.Domain.ModelRequests.Route;
using Audivia.Domain.ModelResponses.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IRouteService
    {
        Task<RouteResponse> CreateRouteAsync(CreateRouteRequest req);
        Task<RouteResponse> UpdateRouteAsync(string id, UpdateRouteRequest req);
        Task<RouteResponse> DeleteRouteAsync(string id);
        Task<RouteListResponse> GetAllRouteAsync();
        Task<RouteResponse> GetRouteByIdAsync(string id);
    }
}
