using Microsoft.EntityFrameworkCore;
using Scout.Model.DB;
using Scout.Services.Player.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scout.Service.Player
{
    internal class PlayerRepository:IPlayerRepository
    {
        private ScoutContext scoutContext = null;

        public PlayerRepository(ScoutContext context)
        {
            scoutContext = context;
        }

        public async Task<int> CreatePlayer(Model.DB.Player player)
        {
            scoutContext.Players.Add(player);
            await scoutContext.SaveChangesAsync();

            return player.PlayerId;
        }

        public async Task<int> CreatePlayerBattingStatistics(PlayerBattingStatistics battingStats)
        {
            scoutContext.PlayerBattingStatisticsSet.Add(battingStats);
            await scoutContext.SaveChangesAsync();

            return battingStats.PlayerBattingStatisticsId;
        }

        public async Task<int> CreatePlayerPitchingStatistics(PlayerPitchingStatistics pitchingStats)
        {
            scoutContext.PlayerPitchingStatisticsSet.Add(pitchingStats);
            await scoutContext.SaveChangesAsync();

            return pitchingStats.PlayerPitchingStatisticsId;
        }

        public async Task<List<Model.DB.Player>> FindPlayersByName(string name)
        {
            List< Model.DB.Player> players = await scoutContext.Players
                .Where(p => p.FirstName.StartsWith(name) || p.LastName.StartsWith(name))
                .Select(p => p)
                .ToListAsync();

            return players;
        }

        public async Task<Model.DB.Player> GetPlayer(int playerId)
        {
            Model.DB.Player player = await scoutContext.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);

            return player;
        }

        public async Task<Model.DB.Player> GetPlayer(string playerCode)
        {
            Model.DB.Player player = await scoutContext.Players.FirstOrDefaultAsync(p => p.PlayerIdentifier == playerCode);

            return player;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (scoutContext != null)
                        scoutContext.Dispose();
                }

                scoutContext = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
