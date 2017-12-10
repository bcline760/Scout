using Scout.Core.Contract;
using Scout.Model.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scout.Service.Contract
{
    public interface IPlayerService
    {
        /// <summary>
        /// Create a new player in the database
        /// </summary>
        /// <param name="player">The player object to create</param>
        /// <returns>The database ID of the player created</returns>
        Task<ObjectModifyResult<int>> CreatePlayer(Player player);

        /// <summary>
        /// Add statistics to an existing player
        /// </summary>
        /// <param name="batting">Add to the player's batting statistics</param>
        /// <param name="pitching">Add to the player's pitching statistics</param>
        /// <returns>Number of added returned</returns>
        Task<ObjectModifyResult<int>> AddStatistics(PlayerBattingStatistics batting, PlayerPitchingStatistics pitching);

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
        Task<ObjectModifyResult<int>> UpdatePlayer(Player player);
    }
}
