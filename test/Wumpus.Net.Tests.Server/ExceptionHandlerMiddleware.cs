using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Serialization;

namespace Wumpus.Server
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WumpusJsonSerializer _serializer;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _serializer = new WumpusJsonSerializer();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = new RestError { Message = new Utf8String(ex.ToString()) };
                await context.Response.Body.WriteAsync(_serializer.WriteUtf8(response));
            }
        }
    }
}
