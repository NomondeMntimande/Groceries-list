using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;
using AuthApi.Helpers;

namespace AuthApi.Services.Interfaces
{
    public interface IRoleService
    {
        Task<OperationResult<ApplicationRole>> GetRoleAsync(int roleId);
        Task<OperationResult<List<ApplicationRole>>> GetAllRolesAsync();
        Task<OperationResult<ApplicationRole>> CreateRoleAsync(Role role);
        Task<OperationResult<ApplicationRole>> UpdateRoleAsync(int id, Role role);
        Task<OperationResult<bool>> DeleteRoleAsync(int id);
    }
}
