using System;
using System.Net;
using Newtonsoft.Json;

namespace Api.Models
{
    public class ErrorDetail
    {
        public int StatusCode { get; private set; }

        public string Message { get; private set; }

        public object Data { get; private set; }

        public ErrorDetail(int statusCode, string message, object data = null)
        {
            StatusCode = statusCode;

            Message = message;

            Data = data;
        }

        public ErrorDetail(HttpStatusCode httpStatusCode, string message, object data = null)
        {
            StatusCode = (int) httpStatusCode;

            Message = message;

            Data = data;
        }
    }
}
