
using System.Text;
using System.Threading.Tasks;
using Api.Extentions;
using Contracts;
using Microsoft.AspNetCore.Http;

namespace Api.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var log = new StringBuilder();
            log.AppendLine("Request {method} {url} => {statusCode}");
            log.AppendLine(context.Request?.Method);
            log.AppendLine(context.Request?.Path.Value);
            log.AppendLine(await context.Request.GetRequestBodyAsync());

            _logger.LogInfo(log.ToString());

            await _next(context);
        }
    }
}
