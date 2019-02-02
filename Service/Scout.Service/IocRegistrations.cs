using Scout.Core;
using Scout.Core.Service;

using Autofac;

namespace Scout.Service
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder container)
        {
            container.RegisterType<PlayerService>().As<IPlayerService>();
            container.RegisterType<AccountService>().As<IAccountService>();
        }
    }
}
