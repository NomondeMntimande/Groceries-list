using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthApi.Services.Interfaces;
using AuthApi.Dtos.Enteties;
using AuthApi.Dtos;
using AuthApi.Helpers;

namespace AuthApi.Controllers
{
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ApplicationRole>>> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ApplicationRole>> GetRole(int id)
        {
            var role = await _roleService.GetRoleAsync(id);
            if  (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            var updatedRole = await _roleService.UpdateRoleAsync(id, role);
            if (updatedRole == null)
            {
                return NotFound();
            }

            return Ok(updatedRole);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationRole>> CreateRole(Role roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operationResult = await _roleService.CreateRoleAsync(roleDto);

            if (operationResult.Status == OperationResultStatus.Success)
            {
                return Ok(operationResult.Data);
            }

            return BadRequest(operationResult.Message);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var deletedRole = await _roleService.DeleteRoleAsync(id);
            if (deletedRole == null)
            {
                return NotFound();
            }
            return Ok(deletedRole);
        }

    }
}
