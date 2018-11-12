using System.Collections.Generic;
using System.Threading.Tasks;

using Scout.Core.Contract;
namespace Scout.Core.Repository
{
    public interface IPlayerPitchingRepository : IScoutRepository<PlayerPitchingStatistics>
    {
        /// <summary>
        /// Get player statistics by their team.
        /// </summary>
        /// <returns>All batting statistics matching the team identifier.</returns>
        /// <param name="teamId">Team identifier.</param>
        Task<List<PlayerPitchingStatistics>> GetByTeam(string teamId);

        /// <summary>
        /// Get player statistics by the player identifier. 
        /// </summary>
        /// <returns>All batting statistics by the player identifier</returns>
        /// <param name="playerId">Player identifier.</param>
        Task<List<PlayerPitchingStatistics>> GetByPlayer(string playerId);
    }
}
