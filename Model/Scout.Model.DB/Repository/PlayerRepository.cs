using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scout.Model.DB.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private IScoutContext scoutContext = null;

        public PlayerRepository(IScoutContext context)
        {
            scoutContext = context;
        }

        public async Task<int> CreatePlayer(Model.DB.Player player)
        {
            scoutContext.Player.Add(player);
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
            List<Model.DB.Player> players = await scoutContext.Player
                .Where(p => p.FirstName.StartsWith(name) || p.LastName.StartsWith(name))
                .Select(p => p)
                .ToListAsync();

            return players;
        }

        public async Task<Model.DB.Player> GetPlayer(int playerId)
        {
            Model.DB.Player player = await scoutContext.Player.FirstOrDefaultAsync(p => p.PlayerId == playerId);

            return player;
        }

        public async Task<Model.DB.Player> GetPlayer(string playerCode)
        {
            Model.DB.Player player = await scoutContext.Player.FirstOrDefaultAsync(p => p.PlayerIdentifier == playerCode);

            return player;
        }
    }
}
