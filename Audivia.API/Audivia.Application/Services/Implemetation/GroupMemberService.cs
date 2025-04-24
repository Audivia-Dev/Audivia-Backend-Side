using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.GroupMember;
using Audivia.Domain.ModelResponses.GroupMember;
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
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IGroupMemberRepository _groupMemberRepository;

        public GroupMemberService(IGroupMemberRepository groupMemberRepository)
        {
            _groupMemberRepository = groupMemberRepository;
        }

        public async Task<GroupMemberResponse> CreateGroupMemberAsync(CreateGroupMemberRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.UserId) || string.IsNullOrEmpty(req.GroupId))
            {
                return new GroupMemberResponse { Success = false, Message = "Invalid request!" };
            }

            var gm = new GroupMember
            {
                UserId = req.UserId,
                GroupId = req.GroupId,
                JoinedAt = DateTime.UtcNow,
            };

            await _groupMemberRepository.Create(gm);

            return new GroupMemberResponse
            {
                Success = true,
                Message = "Created successfully!",
                Response = ModelMapper.MapGroupMemberToDTO(gm),
            };
        }


        public async Task<GroupMemberResponse> UpdateGroupMemberAsync(string id, UpdateGroupMemberRequest req)
        {
            var gm = await _groupMemberRepository.GetById(new ObjectId(id));
            if (gm == null)
            {
                return new GroupMemberResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null
                };
            }

            gm.UserId = req.UserId ?? gm.UserId;
            gm.GroupId = req.GroupId ?? gm.GroupId;

            await _groupMemberRepository.Update(gm);

            return new GroupMemberResponse
            {
                Success = true,
                Message = "Updated successfully!",
                Response = ModelMapper.MapGroupMemberToDTO(gm)
            };
        }

        public async Task<GroupMemberResponse> DeleteGroupMemberAsync(string id)
        {
            var gm = await _groupMemberRepository.GetById(new ObjectId(id));
            if (gm == null)
                return new GroupMemberResponse { Success = false, Message = "Not found!" };

            await _groupMemberRepository.Delete(gm);

            return new GroupMemberResponse
            {
                Success = true,
                Message = "Deleted successfully!",
                Response = ModelMapper.MapGroupMemberToDTO(gm),
            };
        }

        public async Task<GroupMemberResponse> GetGroupMemberByIdAsync(string id)
        {
            var gm = await _groupMemberRepository.GetById(new ObjectId(id));
            return gm == null
                ? new GroupMemberResponse { Success = false, Message = "Not found!" }
                : new GroupMemberResponse
                {
                    Success = true,
                    Message = "Fetched successfully!",
                    Response = ModelMapper.MapGroupMemberToDTO(gm),
                };
        }

        public async Task<GroupMemberListResponse> GetAllGroupMembersAsync()
        {
            var list = await _groupMemberRepository.GetAll();
            return new GroupMemberListResponse
            {
                Success = true,
                Message = "Fetched all successfully!",
                Response = list.Select(ModelMapper.MapGroupMemberToDTO).ToList()
            };
        }
    }
}
