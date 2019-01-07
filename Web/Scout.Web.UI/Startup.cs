using System;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Scout.Core;
using Scout.Web.Api;

using Autofac;
using Scout.Core.Configuration;

namespace Scout.Web.UI
{
    public class Startup
    {
        private IHostingEnvironment _env = null;
        private ILoggerFactory _loggerFactory = null;

        public Startup(IConfiguration configuration, ILoggerFactory factory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            _loggerFactory = factory;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(ScoutApiController).Assembly);
            services.AddAuthentication().AddJwtBearer();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var config = new ScoutConfiguration
            {
                MongoConnectionString = Configuration.GetConnectionString("MongoDB"),
                MongoDatabaseName = Configuration["DatabaseName"]
            };

            builder.RegisterInstance<IScoutConfiguration>(config);
            ContainerLoader.LoadContainers(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 &&
                      !Path.HasExtension(context.Request.Path.Value) &&
                      !context.Request.Path.Value.StartsWith("/api/", StringComparison.CurrentCulture))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller}/{action=Index}/{id?}");
            //    routes.MapRoute("apiRoute", "api/{controller}/{action}/{id?}");
            //});
        }
    }
}