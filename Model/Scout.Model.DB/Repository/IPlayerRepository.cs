using Scout.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scout.Model.DB.Repository
{
    public interface IPlayerRepository
    {
        /// <summary>
        /// Create a new player in the database
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        Task<int> CreatePlayer(Player player);

        /// <summary>
        /// Add to a player's fielding statistics
        /// </summary>
        /// <param name="fieldingStatistics">The new set of fielding statistics to add</param>
        /// <returns>Number of records returned</returns>
        Task<int> CreatePlayerFieldingStatistics(PlayerFieldingStatistics fieldingStatistics);

        /// <summary>
        /// Get a player by database identifier
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<Player> GetPlayer(int playerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerCode"></param>
        /// <returns></returns>
        Task<Player> GetPlayer(string playerCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Player>> FindPlayersByName(string name);

        /// <summary>
        /// Updates an existing player
        /// </summary>
        /// <param name="player">The player record to update</param>
        /// <returns>Number of records updated</returns>
        Task<int> UpdatePlayer(Player player);
    }
}
