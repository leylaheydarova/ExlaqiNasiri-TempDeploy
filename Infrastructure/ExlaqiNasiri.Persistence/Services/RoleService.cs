using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Role;
using ExlaqiNasiri.Application.ResultPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<bool>> CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = roleName
                };
                IdentityResult result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("create role"));
            }
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<RoleGetDto>>> GetAllRolesAsync()
        {
            var role = await _roleManager.Roles
                .Select(r => new RoleGetDto
                {
                    Id = r.Id,
                    roleName = r.Name
                }).ToListAsync();
            if (!role.Any())
            {
                role.Add(new RoleGetDto
                {
                    Id = "",
                    roleName = "There is no role!"
                });
            }
            return Result<List<RoleGetDto>>.Success(role);
        }

        public async Task<Result<RoleGetDto>> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return Result<RoleGetDto>.Failure(Error.NotFoundError("role"));
            var dto = new RoleGetDto
            {
                Id = role.Id,
                roleName = role.Name
            };
            return Result<RoleGetDto>.Success(dto);
        }

        public async Task<Result<bool>> RemoveRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return Result<bool>.Failure(Error.NotFoundError("role"));
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("remove role"));
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateRoleAsync(string id, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return Result<bool>.Failure(Error.NotFoundError("role"));
            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("update role"));
            return Result<bool>.Success(true);
        }
    }
}
