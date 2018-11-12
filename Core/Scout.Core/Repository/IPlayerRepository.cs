using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Scout.Core.Contract;

namespace Scout.Core.Repository
{
    public interface IPlayerRepository : IScoutRepository<Player>
    {
        /// <summary>
        /// Get a player by the player identifier code
        /// </summary>
        /// <param name="playerCode">The player's unique code</param>
        /// <returns>The player matching the code or null if not found</returns>
        Task<Player> GetPlayerAsync(string playerCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Player>> FindPlayersByNameAsync(string name);
    }
}
