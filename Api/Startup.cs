using System;
using System.IO;
using Api.Extentions;
using Api.Helpers;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NLog;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
            LogManagerLoadConfig();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureLoggerService();
            services.ConfigureAuthorization();
            services.ConfigureAuthentication(_config);
            services.ConfigureSqlContext(_config);
            services.AddHttpContextAccessor();
            services.ConfigureActionContextAccessor();
            services.ConfigureUrlHelper();
            services.ConfigureRepositoryWrapper();
            services.ConfigureAppUserService();
            services.ConfigureTokenService();
            services.ConfigureProductService();
            services.ConfigureCloudDinaryService(_config);
            services.ConfigureCategoryService();
            services.ConfigureAppUtilsService();
            services.ConfigureSalesOrderService();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                        options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                    })
                    .ConfigureApiBehaviorOptions(options => {
                        options.SuppressModelStateInvalidFilter = true;
                    });

            services.Configure<CloudinarySettings>(_config.GetSection("CloudinarySettings"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                    options.DefaultModelsExpandDepth(-1);
                });
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    await next();
                    if(context.Response.StatusCode == 404 && Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });
            }

            app.ConfigRequestLoggingMiddleware();

            app.ConfigCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void LogManagerLoadConfig()
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }
    }
}
