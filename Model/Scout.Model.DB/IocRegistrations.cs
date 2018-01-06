using Scout.Core;
using Scout.Model.DB.Context;
using Scout.Model.DB.Repository;

namespace Scout.Model.Context
{
    public class IocRegistrations : IRegister
    {
        public void Register(IContainer container)
        {
            container.RegisterType<IScoutContext, ScoutContext>(DependencyLifetime.Singleton);
            container.RegisterType<IPlayerRepository, PlayerRepository>(DependencyLifetime.Singleton);
            container.RegisterType<IPlayerBattingRepository, PlayerBattingRepository>(DependencyLifetime.Transient);
            container.RegisterType<IPlayerPitchingRepository, PlayerPitchingRepository>(DependencyLifetime.Transient);
            container.RegisterType<ITeamRepository, TeamRepository>(DependencyLifetime.Singleton);
        }
    }
}
