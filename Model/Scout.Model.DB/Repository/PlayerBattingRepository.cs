using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public class PlayerBattingRepository : DbRepository, IPlayerBattingRepository
    {
        public PlayerBattingRepository(IScoutContext context) : base(context)
        {
        }

        public async Task<int> CreatePlayerBattingStatistics(PlayerBattingStatistics battingStats)
        {
            Context.PlayerBattingStatistics.Add(battingStats);

            await Context.SaveChangesAsync();
            return battingStats.PlayerBattingStatisticsId;
        }

        public async Task<List<PlayerBattingStatisticsView>> GetStatistics(int playerId)
        {
            IList<PlayerBattingStatisticsView> battingStats = null;
            await Context.DbContext.LoadStoredProc("usp_GetPlayerBattingStatistics")
                .WithSqlParam("playerId", playerId)
                .ExecuteStoredProcAsync(sp =>
                {
                    battingStats = sp.ReadToList<PlayerBattingStatisticsView>();
                });

            return battingStats.ToList();
        }

        public async Task<int> UpdatePlayerBattingStatistics(PlayerBattingStatistics battingStatistics)
        {
            var battingStats = await Context.PlayerBattingStatistics
                .FirstOrDefaultAsync(b => b.PlayerBattingStatisticsId == battingStatistics.PlayerBattingStatisticsId);

            if (battingStats != null)
            {
                Context.DbContext.Attach<PlayerBattingStatistics>(battingStatistics);
                return await Context.SaveChangesAsync();
            }
            else
                return -1; //MAGIC NUMBER
        }
    }
}
