using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.Api.Middleware
{

    public static class MyAuthorizationMiddlewareExtenstions
    {
        public static IApplicationBuilder UseMyAuthorization (this IApplicationBuilder app)
        {
            app.UseMiddleware<MyAuthorizationMiddleware>();
            return app;
        }
    }


    public class MyAuthorizationMiddleware : IMiddleware //only .NET 5
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
