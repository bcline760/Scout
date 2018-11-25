using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Autofac;

using Scout.Model.DB.Context;
using Scout.Core.Repository;
using Scout.Model.DB.Repository;
using Scout.Core;
namespace Scout.Model.DB
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder container)
        {
            container.Register(r =>
            {
                var config = r.Resolve<IConfiguration>();

                var builder = new DbContextOptionsBuilder<ScoutContext>()
                    .UseSqlServer(config.GetConnectionString("default"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                var context = new ScoutContext(builder.Options);

                return context;
            }).As<IScoutContext>().SingleInstance();

            container.RegisterType<PlayerRepository>().As<IPlayerRepository>().InstancePerDependency();
            container.RegisterType<TeamRepository>().As<ITeamRepository>().InstancePerDependency();
        }
    }
}
