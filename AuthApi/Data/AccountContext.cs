using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthApi.Dtos.Enteties;

namespace AuthApi.Data
{
    public class AccountContext : IdentityUserContext<IdentityUser>
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
        public DbSet<GroceriesList> GroceriesList { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // It would be a good idea to move the connection string to user secrets
            options.UseNpgsql("Host=localhost;Database=GroceriesApp;Username=postgres;Password=Nju@02#13");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
