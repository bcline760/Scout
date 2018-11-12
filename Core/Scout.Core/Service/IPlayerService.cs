using Scout.Core.Contract;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scout.Core.Service
{
    public interface IPlayerService
    {
        /// <summary>
        /// Create a new player in the database
        /// </summary>
        /// <param name="player">The player object to create</param>
        /// <returns>The database ID of the player created</returns>
        Task<ObjectModifyResult<Guid>> CreatePlayer(Player player);

        /// <summary>
        /// Get a list of all the players in the database
        /// </summary>
        /// <returns>List of all players in database</returns>
        Task<List<PlayerListItem>> RetrievePlayers();

        /// <summary>
        /// Find players matching the request criteria
        /// </summary>
        /// <param name="request">Request criteria for player searching</param>
        /// <returns>List of players matching the request or null if none found</returns>
        Task<List<Player>> FindPlayers(PlayerSearchRequest request);

        /// <summary>
        /// Update player data
        /// </summary>
        /// <param name="player">The player object to update</param>
        /// <returns>Boolean to indicate whether the player was updated</returns>
        Task<ObjectModifyResult<Guid>> UpdatePlayer(Player player);
    }
}
