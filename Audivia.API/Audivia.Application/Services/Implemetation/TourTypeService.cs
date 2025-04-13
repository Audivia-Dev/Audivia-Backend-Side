using Audivia.Application.Services.Interface;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TourType;
using Audivia.Domain.ModelResponses.TourType;
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
    public class TourTypeService : ITourTypeService
    {
        private readonly ITourTypeRepository _tourTypeRepository;

        public TourTypeService(ITourTypeRepository tourTypeRepository)
        {
            _tourTypeRepository = tourTypeRepository;
        }

        public async Task<TourTypeResponse> CreateTourType(CreateTourTypeRequest request)
        {
            var tourType = new TourType
            {
                TourTypeName = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _tourTypeRepository.Create(tourType);

            return new TourTypeResponse
            {
                Success = true,
                Message = "Tour type created successfully",
                Response = new TourTypeDTO
                {
                    Id = tourType.Id,
                    Name = tourType.TourTypeName,
                }
            };
        }

        public async Task DeleteTourType(string id)
        {
            var tourType = await _tourTypeRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tourType == null) throw new Exception("Tour type not found");

            tourType.IsDeleted = true;
            tourType.UpdatedAt = DateTime.UtcNow;

            await _tourTypeRepository.Update(tourType);
        }

        public async Task<TourTypeListResponse> GetAllActiveTourTypes()
        {
            var list = await _tourTypeRepository.GetAll();

            // Lọc danh sách chỉ lấy những bản ghi chưa bị xóa
            var activeList = list.Where(t => !t.IsDeleted).ToList();

            if (!activeList.Any())
            {
                // Không có bản ghi nào hoặc tất cả đều bị xóa
                return new TourTypeListResponse
                {
                    Success = false,
                    Message = "No active tour types found.",
                    Response = new List<TourTypeDTO>()
                };
            }

            return new TourTypeListResponse
            {
                Success = true,
                Message = "Tour types retrieved successfully",
                Response = activeList.Select(t => new TourTypeDTO
                {
                    Id = t.Id,
                    Name = t.TourTypeName,
                }).ToList()
            };
        }


        public async Task<TourTypeResponse> GetTourTypeInformation(string id)
        {
            var item = await _tourTypeRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (item == null)
            {
                return new TourTypeResponse
                {
                    Success = false,
                    Message = "Tour type not found or has been deleted.",
                    Response = null
                };
            }

            return new TourTypeResponse
            {
                Success = true,
                Message = "Tour type retrieved successfully",
                Response = new TourTypeDTO
                {
                    Id = item.Id,
                    Name = item.TourTypeName,
                }
            };
        }

        public async Task UpdateTourType(string id, UpdateTourTypeRequest request)
        {
            var tourType = await _tourTypeRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tourType == null) throw new Exception("Tour type not found");

            tourType.TourTypeName = request.Name;
            tourType.UpdatedAt = DateTime.UtcNow;

            await _tourTypeRepository.Update(tourType);
        }
    }
}
