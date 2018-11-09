using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Scout.Core.Contract;
using Scout.Core.Repository;

namespace Scout.Model.DB.Repository
{
    public class PlayerRepository : DbRepository, IPlayerRepository
    {
        public PlayerRepository()
        {
        }

        public async Task<int> CreatePlayer(Player player)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Player>> FindPlayersByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Player> GetPlayer(int playerId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Player> GetPlayer(string playerCode)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> UpdatePlayer(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
