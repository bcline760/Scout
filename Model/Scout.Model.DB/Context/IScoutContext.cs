using Microsoft.EntityFrameworkCore;
using Scout.Core;

namespace Scout.Model.DB
{
    public interface IScoutContext : IContext
    {
        DbSet<Franchise> Franchise { get; set; }
        DbSet<League> League { get; set; }
        DbSet<PlayerBattingStatistics> PlayerBattingStatisticsSet { get; set; }
        DbSet<PlayerPitchingStatistics> PlayerPitchingStatisticsSet { get; set; }
        DbSet<Player> Player { get; set; }
        DbSet<Team> Team { get; set; }
    }
}