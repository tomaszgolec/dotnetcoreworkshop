using Altkom.Shop.Api.Middleware;
using Altkom.Shop.Fakers;
using Altkom.Shop.FakeServices;
using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //todo important , cos tu chyba nie tak wstrzykniete
            services.AddScoped<MyAuthorizationMiddleware>();
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //todo rysunek w notatkach jest zebys zrozumial lepiej
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //logger middleware
            app.UseLoggerMiddleware();
            //app.UseMiddleware<LoggerMiddleware>();
            //app.Use(async (context, next) =>
            //{
            //    Trace.WriteLine($"{context.Request.Method}{context.Request.Path}");

            //    await next();

            //    Trace.WriteLine($"{context.Response.StatusCode}");
            //});

            //logger middleware
            app.UseMyAuthorization();
            //app.UseMiddleware<MyAuthorizationMiddleware>();
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    }
            //});


            //app.Map("api/customer",)


            //przechwutje przychodzacy request
            //app.Run(context => context.Response.WriteAsync("helloworld!"));

            //todo to co mamy ponizej jest zamiast funkcji Run

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context => await context.Response.WriteAsync("hello world"));


                endpoints.MapGet("/api/customers", async context => await context.Response.WriteAsync("hello customers"));
                endpoints.MapGet("/api/customers/{id:int}", async context => {
                    int id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    await context.Response.WriteAsync($"wirtaj customer {id}");


                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();


                    var customer = customerService.Get(id);

                    await context.Response.WriteAsJsonAsync(customer);

                });


                

            });

        }
    }
}
