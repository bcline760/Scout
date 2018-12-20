using System.Collections.Generic;
using System.Threading.Tasks;
using Scout.Core.Contract;

namespace Scout.Core.Service
{
    public interface IScoutReportService : IScoutService<ScoutingReport>
    {
        /// <summary>
        /// Get scouting reports based on a player asynchronously
        /// </summary>
        /// <returns>The reports.</returns>
        /// <param name="playerId">Player identifier.</param>
        Task<List<ScoutingReport>> GetReports(int playerId);
    }
}
