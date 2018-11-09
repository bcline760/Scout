using System.Collections.Generic;
using System.Threading.Tasks;

using Scout.Core.Contract;

namespace Scout.Core.Repository
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
        /// Retrieves all the players in the database
        /// </summary>
        /// <returns>List of the entire player database</returns>
        Task<List<Player>> GetAllPlayers();

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
