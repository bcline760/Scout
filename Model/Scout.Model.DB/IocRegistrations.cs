using Scout.Core;
using Scout.Model.DB.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scout.Model.DB
{
    public class IocRegistrations : IRegister
    {
        public void Register(IContainer container)
        {
            container.RegisterType<IScoutContext, ScoutContext>(DependencyLifetime.Singleton);
            container.RegisterType<IPlayerRepository, PlayerRepository>(DependencyLifetime.Singleton);
            container.RegisterType<ITeamRepository, TeamRepository>(DependencyLifetime.Singleton);
        }
    }
}
