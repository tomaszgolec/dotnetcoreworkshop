using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.Api.Middleware
{


    public static class LoggerMiddlewareExtenstions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggerMiddleware>();
            return app;
        }
    }


    public class LoggerMiddleware
    {
        public readonly RequestDelegate Next;

        public LoggerMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           
                Trace.WriteLine($"{context.Request.Method}{context.Request.Path}");

                await Next(context);

                Trace.WriteLine($"{context.Response.StatusCode}");
           
        }

    }
}
