using System;
using System.Text;
using Api.AppServices;
using Contracts;
using Entities;
using Entities.Models.DataTransferObjects;
using LoggerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;

namespace Api.Extentions
{
    public static class ServicesExtentions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                        );
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultSqlConnection"];
            services.AddDbContext<ShoppingCartContext>(options => options
                    .UseSqlServer(connectionString)
            );
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequireModifyProfile", policy => policy.RequireRole("Admin", "Staff", "Guest"));
                opt.AddPolicy("RequireBookManager", policy => policy.RequireRole("Admin", "Staff"));
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(jwt =>
                    {
                        var key = Encoding.UTF8.GetBytes(config["JwtConfig:Secret"]);

                        jwt.SaveToken = true;
                        jwt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            RequireExpirationTime = true
                        };
                    });
        }
        
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureTokenServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }

        public static void ConfigureAppUserServices(this IServiceCollection services)
        {
            services.AddTransient<IAppUserService, AppUserService>();
        }

        public static void ConfigureProductServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
        }

        public static void ConfigureCloudDinaryServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ICloudDinaryService, CloudDinaryService>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        }

        public static void ConfigureCategoryServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
        }

        public static void ConfigureAppUtilsServices(this IServiceCollection services)
        {
            services.AddTransient<IAppUtilsService, AppUtilsService>();
        }
    }
}
