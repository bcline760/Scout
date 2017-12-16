using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Scout.Service.Contract;
using Scout.Service;
using Scout.Core;
using Scout.Model.DB;
using Scout.Model.DB.Repository;
using Microsoft.EntityFrameworkCore;

namespace Scout.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //What I want to do
            //IContainer container = new IocContainer(services);
            //ContainerLoader.LoadContainers(container);

            //What I don't want to do
            services.AddDbContext<ScoutContext>(ctx => {
                ctx.UseSqlServer(Configuration["ConnectionString"]);
            });
            services.AddScoped<IScoutContext, ScoutContext>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPlayerService, PlayerService>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
