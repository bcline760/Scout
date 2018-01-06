using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public class PlayerPitchingRepository : DbRepository, IPlayerPitchingRepository
    {
        public PlayerPitchingRepository(IScoutContext context) : base(context)
        {
        }

        public async Task<int> CreatePlayerPitchingStatistics(PlayerPitchingStatistics pitchingStats)
        {
            if (pitchingStats == null)
                throw new ArgumentNullException(nameof(pitchingStats));

            Context.PlayerPitchingStatistics.Add(pitchingStats);
            int recordsModified = await Context.SaveChangesAsync();

            return pitchingStats.PlayerPitchingStatisticsId;
        }

        public async Task<List<PlayerPitchingStatisticsView>> GetStatistics(int playerId)
        {
            IList<PlayerPitchingStatisticsView> pitchingStats = null;
            await Context.DbContext.LoadStoredProc("usp_GetPlayerPitchingStatistics")
                .WithSqlParam("playerId", playerId)
                .ExecuteStoredProcAsync(sp =>
                {
                    pitchingStats = sp.ReadToList<PlayerPitchingStatisticsView>();
                });

            return pitchingStats.ToList();
        }

        public async Task<int> UpdatePlayerPitchingStatistics(PlayerPitchingStatistics pitchingStatistics)
        {
            var pitchingStats = await Context.PlayerBattingStatistics
                .FirstOrDefaultAsync(b => b.PlayerBattingStatisticsId == pitchingStatistics.PlayerPitchingStatisticsId);

            if (pitchingStats != null)
            {
                Context.DbContext.Attach<PlayerPitchingStatistics>(pitchingStatistics);
                return await Context.SaveChangesAsync();
            }
            else
                return -1; //MAGIC NUMBER
        }
    }
}
