using AuthApi.Data;
using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;
using AuthApi.Helpers;
using AuthApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly AccountContext _dbContext;
        private readonly IMapper _mapper;

        public RoleService(AccountContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ApplicationRole>> GetRoleAsync(int roleId)
        {
            try
            {
                var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
                if (role == null)
                    return OperationResult<ApplicationRole>.Failure("Role not found.");

                return OperationResult<ApplicationRole>.Success(role);
            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<OperationResult<List<ApplicationRole>>> GetAllRolesAsync()
        {

            try
            {
                var roles = await _dbContext.Roles.ToListAsync();
                return OperationResult<List<ApplicationRole>>.Success(roles);
            }
            catch (Exception ex)
            {
                return OperationResult<List<ApplicationRole>>.Failure($"Error trying to retrieve roles. {ex.Message}");
            }
        }

        public async Task<OperationResult<ApplicationRole>> CreateRoleAsync(Role roleDto)
        {
            try
            {
                var role = _mapper.Map<ApplicationRole>(roleDto);
                await _dbContext.Roles.AddAsync(role);
                await _dbContext.SaveChangesAsync();
                return OperationResult<ApplicationRole>.Success(role);
            }
            catch (Exception ex)
            {
                return OperationResult<ApplicationRole>.Failure($"Error creating Role. {ex.Message}");
            }
        }

        public async Task<OperationResult<ApplicationRole>> UpdateRoleAsync(int id, Role roleDto)
        {
            try
            {
                var existingRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

                if (existingRole == null)
                    return OperationResult<ApplicationRole>.Failure("Role not found.");

                existingRole.Name = roleDto.Name;
                existingRole.Description = roleDto.Description ?? existingRole.Description;
                await _dbContext.SaveChangesAsync();

                return OperationResult<ApplicationRole>.Success(existingRole);
            }
            catch (Exception ex)
            {
                return OperationResult<ApplicationRole>.Failure($"Error updating role: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteRoleAsync(int id)
        {
            try
            {
                var role = await _dbContext.Roles.FindAsync(id);
                if (role == null)
                    return OperationResult<bool>.Failure("Role not found.");

                _dbContext.Roles.Remove(role);
                await _dbContext.SaveChangesAsync();

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"Error deleting role: {ex.Message}");
            }
        }
    }
}
