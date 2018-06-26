using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Abb.SimpleChat.Business.Logic.Interfaces;
using Abb.SimpleChat.External.RepositoryEntityFamework;
using Abb.SimpleChat.Controllers;

namespace Abb.SimpleChat
{
    public class Startup
    {
        IFormCollection collection;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(typeof(IRepository<>), typeof(SimpleChatRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                       name: "AuthorizationView",
                       template: "{controller=Authorization}/{action=AuthorizationView}");
/*routes.MapRoute(
                    name: "MessagingView",
                    template: "{controller=Messaging}/{action=MessagingView}");*/
            });
            /*app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
                await context.Response.Redirect(nameof(AuthorizationView));
            });*/
        }
    }
}
