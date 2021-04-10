using System;
using System.Net;

namespace Api.Models
{
    public class ErrorResponse
    {
        public ErrorDetail Error { get; private set; }

        public ErrorResponse(int statusCode, string message, object data = null)
        {
            Error = new ErrorDetail(statusCode, message, data);
        }

        public ErrorResponse(HttpStatusCode httpStatusCode, string message, object data = null)
        {
            Error = new ErrorDetail(httpStatusCode, message, data);
        }
    }
}
