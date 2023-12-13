using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthApi.Dtos.Enteties;
using AuthApi.Dtos;

namespace AuthApi.Data
{
    public class AccountContext : IdentityDbContext<User,ApplicationRole,int>
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
        public DbSet<GroceriesList> GroceriesList { get; set; }

        //public DbSet<ListCategory> ListCategory { get; set; }

        //public DbSet<ListItem> ListItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // It would be a good idea to move the connection string to user secrets
            options.UseNpgsql("Host=localhost;Database=GroceriesApp;Username=postgres;Password=Nju@02#13");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        }
    }
}
