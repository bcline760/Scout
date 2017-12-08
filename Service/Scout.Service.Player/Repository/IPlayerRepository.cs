using Scout.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scout.Service.Player
{
    public interface IPlayerRepository
    {
        /// <summary>
        /// Create a new player in the database
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        Task<int> CreatePlayer(Model.DB.Player player);

        /// <summary>
        /// Add to an existing player's overall batting statistics
        /// </summary>
        /// <param name="battingStats">The batting stats to insert for player</param>
        /// <returns></returns>
        Task<int> CreatePlayerBattingStatistics(PlayerBattingStatistics battingStats);

        /// <summary>
        /// Add to an overall player's pitching statistics
        /// </summary>
        /// <param name="pitchingStats">The pitching stats to insert for player</param>
        /// <returns></returns>
        Task<int> CreatePlayerPitchingStatistics(PlayerPitchingStatistics pitchingStats);

        /// <summary>
        /// Get a player by database identifier
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<Model.DB.Player> GetPlayer(int playerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerCode"></param>
        /// <returns></returns>
        Task<Model.DB.Player> GetPlayer(string playerCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Model.DB.Player>> FindPlayersByName(string name);
    }
}
