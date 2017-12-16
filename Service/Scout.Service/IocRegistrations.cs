using Scout.Core;
using Scout.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scout.Service
{
    public class IocRegistrations : IRegister
    {
        public void Register(IContainer container)
        {
            container.RegisterType<IPlayerService, PlayerService>(DependencyLifetime.Singleton);
        }
    }
}
