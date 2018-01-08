using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public class PlayerRepository : DbRepository, IPlayerRepository
    {
        public PlayerRepository(IScoutContext context) : base(context)
        {
        }

        public async Task<int> CreatePlayer(Model.DB.Player player)
        {
            Context.Player.Add(player);
            await Context.SaveChangesAsync();

            return player.PlayerId;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            List<Player> allPlayers = await Context.Player.ToListAsync();

            return allPlayers;
        }

        public async Task<List<Model.DB.Player>> FindPlayersByName(string name)
        {
            List<Model.DB.Player> players = await Context.Player
                .Where(p => p.FirstName.StartsWith(name) || p.LastName.StartsWith(name))
                .Select(p => p)
                .ToListAsync();

            return players;
        }

        public async Task<Model.DB.Player> GetPlayer(int playerId)
        {
            Model.DB.Player player = await Context.Player.FirstOrDefaultAsync(p => p.PlayerId == playerId);

            return player;
        }

        public async Task<Model.DB.Player> GetPlayer(string playerCode)
        {
            Model.DB.Player player = await Context.Player
                .FirstOrDefaultAsync(p => p.PlayerIdentifier == playerCode);

            return player;
        }

        public async Task<int> UpdatePlayer(Player player)
        {
            var oldPlayer = await Context.Player
                .FirstOrDefaultAsync(b => b.PlayerId == player.PlayerId);

            if (oldPlayer != null)
            {
                Context.DbContext.Attach<Player>(player);
                return await Context.SaveChangesAsync();
            }
            else
                return -1; //MAGIC NUMBER
        }
    }
}
