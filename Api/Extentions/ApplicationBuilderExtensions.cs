using System;
using Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Api.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
