using ExlaqiNasiri.Application.DTOs.Role;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IRoleService
    {
        Task<Result<bool>> CreateRoleAsync(string roleName);
        Task<Result<bool>> RemoveRoleAsync(string id);
        Task<Result<bool>> UpdateRoleAsync(string id, string roleName);
        Task<Result<List<RoleGetDto>>> GetAllRolesAsync();
        Task<Result<RoleGetDto>> GetRoleByIdAsync(string id);
    }
}
