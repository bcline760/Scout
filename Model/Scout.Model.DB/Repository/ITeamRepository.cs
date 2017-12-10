using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scout.Model.DB.Repository
{
    public interface ITeamRepository
    {
        /// <summary>
        /// Create a new team season
        /// </summary>
        /// <param name="team">The new team season entity to add to data store</param>
        /// <returns></returns>
        Task<int> CreateTeam(Team team);

        /// <summary>
        /// Update an existing team season
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        Task<int> UpdateTeam(Team team);

        /// <summary>
        /// Delete a team season
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<int> DeleteTeam(int teamId);

        /// <summary>
        /// Get a team by the identifier
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<Team> GetTeam(int teamId);

        /// <summary>
        /// Find teams by their name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        Task<List<Team>> FindTeamsByTeamName(string teamName);

        /// <summary>
        /// Find teams by their team code
        /// </summary>
        /// <param name="teamCode">The three-character code identifying a team for that year</param>
        /// <returns>Teams matching the identifier</returns>
        Task<List<Team>> FindTeamsByCode(string teamCode);

        /// <summary>
        /// Finds all teams matching a franchise. A franchise can be many teams, example the Seattle Pilots and Seattle Mariners
        /// </summary>
        /// <param name="franchiseId">The ID of the franchise</param>
        /// <returns>List of teams matching the franchise</returns>
        Task<List<Team>> FindTeamsByFranchise(int franchiseId);

        /// <summary>
        /// Finds all teams in a league.
        /// </summary>
        /// <param name="leagueId">The identifier of the league</param>
        /// <returns>A list of teams matching the league</returns>
        Task<List<Team>> FindTeamsByLeague(int leagueId);

        /// <summary>
        /// Find teams for a given season (year)
        /// </summary>
        /// <param name="year">The given baseball season year</param>
        /// <returns>The teams that match the current year</returns>
        Task<List<Team>> FindTeamsByYear(short year);
    }
}
