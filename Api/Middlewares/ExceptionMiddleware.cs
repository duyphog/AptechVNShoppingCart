using System;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Microsoft.AspNetCore.Http;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
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
                await HandleExceptionAsync(httpContext);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "aplication/json";

            var response = new ErrorResponse(httpContext.Response.StatusCode, "Internal Server Error");

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
