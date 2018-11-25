using Microsoft.EntityFrameworkCore;
using Scout.Core;

namespace Scout.Model.DB.Context
{
    public interface IScoutContext : IContext
    {
        DbSet<FranchiseModel> Franchise { get; set; }
        DbSet<LeagueModel> League { get; set; }
        DbSet<PlayerBattingStatisticsModel> PlayerBattingStatistics { get; set; }
        DbSet<PlayerPitchingStatisticsModel> PlayerPitchingStatistics { get; set; }
        DbSet<PlayerFieldingStatisticsModel> PlayerFieldingStatistics { get; set; }
        DbSet<PlayerModel> Player { get; set; }
        DbSet<TeamModel> Team { get; set; }

        DbContext DbContext { get; }
    }
}