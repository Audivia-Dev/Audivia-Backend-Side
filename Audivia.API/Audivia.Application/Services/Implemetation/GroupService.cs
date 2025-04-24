using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Group;
using Audivia.Domain.ModelResponses.Group;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GroupResponse> CreateGroupAsync(CreateGroupRequest req)
        {
            if (req is null || string.IsNullOrWhiteSpace(req.Name))
            {
                return new GroupResponse
                {
                    Success = false,
                    Message = "Group name is required!",
                    Response = null,
                };
            }

            var group = new Domain.Models.Group
            {
                Name = req.Name,
                IsDeleted = false,
            };

            await _groupRepository.Create(group);

            return new GroupResponse
            {
                Success = true,
                Message = "Created group successfully!",
                Response = ModelMapper.MapGroupToDTO(group),
            };
        }

        public async Task<GroupResponse> UpdateGroupAsync(string id, UpdateGroupRequest req)
        {
            var group = await _groupRepository.GetById(new ObjectId(id));
            if (group is null)
            {
                return new GroupResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }

            group.Name = req.Name ?? group.Name;
            await _groupRepository.Update(group);

            return new GroupResponse
            {
                Success = true,
                Message = "Updated group successfully!",
                Response = ModelMapper.MapGroupToDTO(group),
            };
        }

        public async Task<GroupResponse> DeleteGroupAsync(string id)
        {
            var group = await _groupRepository.GetById(new ObjectId(id));
            if (group is null)
            {
                return new GroupResponse
                {
                    Success = false,
                    Message = "Deleted group failed!",
                    Response = null,
                };
            }

            group.IsDeleted = true;
            await _groupRepository.Update(group);

            return new GroupResponse
            {
                Success = true,
                Message = "Deleted group successfully!",
                Response = ModelMapper.MapGroupToDTO(group),
            };
        }

        public async Task<GroupListResponse> GetAllGroupsAsync()
        {
            var list = await _groupRepository.GetAll();
            var activeList = list.Where(g => g.IsDeleted == false).ToList();
            var result = activeList.Select(ModelMapper.MapGroupToDTO).ToList();

            return new GroupListResponse
            {
                Success = true,
                Message = "Fetched all groups successfully!",
                Response = result,
            };
        }

        public async Task<GroupResponse> GetGroupByIdAsync(string id)
        {
            var group = await _groupRepository.GetById(new ObjectId(id));
            if (group is null)
            {
                return new GroupResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }

            return new GroupResponse
            {
                Success = true,
                Message = "Fetched group successfully!",
                Response = ModelMapper.MapGroupToDTO(group),
            };
        }
    }
}
