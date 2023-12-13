using AuthApi.Data.Seeders.Interfaces;
using AuthApi.Dtos.Enteties;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Data.Seeders
{
    public class RoleSeeder : IRoleSeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleSeeder(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole { Name = "Admin", Description = "Administrator role" },
                new ApplicationRole { Name = "Manager", Description = "Manager role" },
                new ApplicationRole { Name = "User", Description = "Regular user role" },
                new ApplicationRole { Name = "Guest", Description = "Guest role" },
            };

            foreach (var role in roles)
            {
                await CreateRoleIfNotExists(role);
            }

        }

        private async Task CreateRoleIfNotExists(ApplicationRole role)
        {
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {
                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{role.Name}'.");
                }
            }
        }
    }
}
