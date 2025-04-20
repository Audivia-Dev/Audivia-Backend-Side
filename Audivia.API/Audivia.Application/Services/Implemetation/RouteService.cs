using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Route;
using Audivia.Domain.ModelResponses.Answer;
using Audivia.Domain.ModelResponses.Route;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Implemetation;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        public async Task<RouteResponse> CreateRouteAsync(CreateRouteRequest req)
        {
            if (req is null)
            {
                return new RouteResponse
                {
                    Success = false,
                    Message = "Created route failed!",
                    Response = null,

                };
            }
            var route = new Route
            {
                TourId = req.TourId,
                Name = req.Name,
                Description = req.Description,
                IsDeleted = false,
            };
            await _routeRepository.Create(route);
            return new RouteResponse
            {
                Success = true,
                Message = "Created route successfully!",
                Response = ModelMapper.MapRouteToDTO(route),

            };
        }

        public async Task<RouteResponse> DeleteRouteAsync(string id)
        {
           var route = await _routeRepository.GetById(new ObjectId(id.ToString()));
            if (route is null)
            {
                return new RouteResponse
                {
                    Success = false,
                    Message = "Deleted route failed!",
                    Response = null,

                };
            }
            route.IsDeleted = true;
            await _routeRepository.Update(route);
            return new RouteResponse
            {
                Success = true,
                Message = "Deleted route successfully!",
                Response = ModelMapper.MapRouteToDTO(route),
            };
        }

        public async Task<RouteListResponse> GetAllRouteAsync()
        {
            var list = await _routeRepository.GetAll();
            var activeList = list.Where(a => a.IsDeleted == false).ToList();
            var rs = activeList.Select(ModelMapper.MapRouteToDTO).ToList();
            return new RouteListResponse
            {
                Success = true,
                Message = "Fetched all Route successfully!",
                Response = rs
            };
        }

        public async Task<RouteResponse> GetRouteByIdAsync(string id)
        {
            var route = await _routeRepository.GetById(new ObjectId(id.ToString()));
            if (route is null)
            {
                return new RouteResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,

                };
            }
            return new RouteResponse
            {
                Success = true,
                Message = "Fetch route successfully!",
                Response = ModelMapper.MapRouteToDTO(route),

            };
        }

        public async Task<RouteResponse> UpdateRouteAsync(string id, UpdateRouteRequest req)
        {
            var route = await _routeRepository.GetById(new ObjectId(id.ToString()));
            if (route is null)
            {
                return new RouteResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,

                };
            }
            route.TourId = req.TourId;
            route.Name = req.Name;
            route.Description = req.Description;
            await _routeRepository.Update(route);
            return new RouteResponse
            {
                Success = true,
                Message = "Updated route successfully!",
                Response = ModelMapper.MapRouteToDTO(route),

            };
        }
    }
}
