﻿using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Role;
using Audivia.Domain.ModelResponses.Role;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleResponse> CreateRole(CreateRoleRequest request)
        {
            var role = new Role
            {
                RoleName = request.RoleName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _roleRepository.Create(role);

            return new RoleResponse
            {
                Success = true,
                Message = "Role created successfully",
                Response = ModelMapper.MapRoleToDTO(role)
            };
        }

        public async Task<List<RoleDTO>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAll();
            return roles
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapRoleToDTO)
                .ToList();
        }

        public async Task<RoleResponse> GetRoleById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid role id!");
            }
            var role = await _roleRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found!");
            }

            return new RoleResponse
            {
                Success = true,
                Message = "Role retrieved successfully",
                Response = ModelMapper.MapRoleToDTO(role)
            };
        }

        public async Task UpdateRole(string id, UpdateRoleRequest request)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid role id!");
            }
            var role = await _roleRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (role == null) return;

            role.RoleName = request.RoleName ?? role.RoleName;
            role.UpdatedAt = DateTime.UtcNow;

            await _roleRepository.Update(role);
        }

        public async Task DeleteRole(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid role id!");
            }
            var role = await _roleRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (role == null) return;

            role.IsDeleted = true;
            role.UpdatedAt = DateTime.UtcNow;

            await _roleRepository.Update(role);
        }
    }
}
