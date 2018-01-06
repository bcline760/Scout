using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scout.Model.DB.Repository
{
    public interface IPlayerBattingRepository
    {
        /// <summary>
        /// Add to an existing player's overall batting statistics
        /// </summary>
        /// <param name="battingStats">The batting stats to insert for player</param>
        /// <returns></returns>
        Task<int> CreatePlayerBattingStatistics(PlayerBattingStatistics battingStats);

        /// <summary>
        /// Get a list of all of the player's batting statistics
        /// </summary>
        /// <param name="playerId">The primary identifier of the player</param>
        /// <returns>List of batting statistics matching the player</returns>
        Task<List<PlayerBattingStatisticsView>> GetStatistics(int playerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="battingStatistics"></param>
        /// <returns></returns>
        Task<int> UpdatePlayerBattingStatistics(PlayerBattingStatistics battingStatistics);
    }
}
