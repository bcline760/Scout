using System;
using System.Collections.Generic;
using System.Text;

using Scout.Model.Contract;
using static Scout.Core.Contract.ApiResponse;

namespace Scout.Service.Player
{
    public interface IPlayerService
    {
        /// <summary>
        /// Create a new player
        /// </summary>
        /// <param name="newPlayer">The player object plus any statistics associated with it</param>
        /// <returns>The ID of the new player added</returns>
        Response<int> CreatePlayer(Model.Contract.Player newPlayer);

        /// <summary>
        /// Add batting statistics to the player's profile
        /// </summary>
        /// <param name="battingStats">The player's batting statistics to add</param>
        /// <returns>The ID of the new batting statistics added</returns>
        Response<int> AddPlayerBattingStatistics(PlayerBattingStatistics battingStats);

        /// <summary>
        /// Add pitching statistics to the player's profile
        /// </summary>
        /// <param name="pitchingStats">The new pitching statistics to add to the player</param>
        /// <returns>The ID of the new pitching statistics added</returns>
        Response<int> AddPlayerPitchingStatistics(PlayerPitchingStatistics pitchingStats);

        /// <summary>
        /// Retrieve players based on request criteria
        /// </summary>
        /// <param name="request">Request parameters of search</param>
        /// <returns>List of players matching request</returns>
        Response<List<Model.Contract.Player>> GetPlayerData(PlayerSearchRequest request);

        /// <summary>
        /// Update an existing player
        /// </summary>
        /// <param name="player">The player to be updated</param>
        /// <returns>Boolean value to indicate success or failure of operation</returns>
        Response<bool> UpdatePlayer(Model.Contract.Player player);
    }
}
