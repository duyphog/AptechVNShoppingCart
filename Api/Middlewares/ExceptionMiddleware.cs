using System;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILoggerManager logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "aplication/json";

                var response = _env.IsDevelopment()
                 ? new ErrorResponse(httpContext.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                 : new ErrorResponse(httpContext.Response.StatusCode, "Internal Server Error");

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
