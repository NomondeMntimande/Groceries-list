
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using AuthApi.Data.Seeders.Interfaces;
using AuthApi.Data.Seeders;
using AuthApi.Services;
using AuthApi.Services.Interfaces;
using AuthApi.Dtos.Enteties;
using AuthApi.Dtos;
using AuthApi.Data;

namespace AuthApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IRoleSeeder, RoleSeeder>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGroceriesListService, GroceriesListService>();
            services.AddScoped<IRoleService, RoleService>();
            // Add ASP.NET Identity
            services.AddIdentity<User, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<AccountContext>()
            .AddDefaultTokenProviders();

            // Add JWT Authentication
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Add Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                //options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            services.AddDbContext<AccountContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRoleSeeder roleSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                });
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            roleSeeder.Seed().GetAwaiter().GetResult();
        }
    }
}
