using Microsoft.EntityFrameworkCore;
using Scout.Core;
using Scout.Model.DB;

namespace Scout.Model.DB.Context
{
    public interface IScoutContext : IContext
    {
        DbSet<Franchise> Franchise { get; set; }
        DbSet<League> League { get; set; }
        DbSet<PlayerBattingStatistics> PlayerBattingStatistics { get; set; }
        DbSet<PlayerPitchingStatistics> PlayerPitchingStatistics { get; set; }
        DbSet<PlayerFieldingStatistics> PlayerFieldingStatistics { get; set; }
        DbSet<Player> Player { get; set; }
        DbSet<Team> Team { get; set; }

        DbContext DbContext { get; }
    }
}