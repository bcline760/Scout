using Scout.Core.Contract;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scout.Core.Service
{
    public interface IPlayerService : IScoutService<Player>
    {
        /// <summary>
        /// Find players matching the request criteria
        /// </summary>
        /// <param name="request">Request criteria for player searching</param>
        /// <returns>List of players matching the request or null if none found</returns>
        Task<List<Player>> FindPlayers(PlayerSearchRequest request);
    }
}
