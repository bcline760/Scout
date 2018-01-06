using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scout.Model.DB.Repository
{
    public interface IPlayerPitchingRepository
    {
        /// <summary>
        /// Add to an overall player's pitching statistics
        /// </summary>
        /// <param name="pitchingStats">The pitching stats to insert for player</param>
        /// <returns>ID of the newly created pitching record</returns>
        Task<int> CreatePlayerPitchingStatistics(PlayerPitchingStatistics pitchingStats);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<List<PlayerPitchingStatisticsView>> GetStatistics(int playerId);

        /// <summary>
        /// Updates an existing player's pitching statistics
        /// </summary>
        /// <param name="pitchingStatistics">The pitching statistics model to update</param>
        /// <returns>The number of records modified</returns>
        Task<int> UpdatePlayerPitchingStatistics(PlayerPitchingStatistics pitchingStatistics);
    }
}
