using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Scout.Model.DB.Repository
{
    internal class TeamRepository : ITeamRepository
    {
        private IScoutContext _context = null;
        public TeamRepository(IScoutContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTeam(Team team)
        {
            _context.Add<Team>(team);

            int recordsModified = await _context.SaveChangesAsync(true);
            return recordsModified;
        }

        public async Task<int> DeleteTeam(int teamId)
        {
            var team = await GetTeam(teamId);

            if (team != null)
            {
                _context.Team.Remove(team);
                int recordsModified = await _context.SaveChangesAsync(true);
                return recordsModified;
            }
            return -1;
        }

        public async Task<List<Team>> FindTeamsByCode(string teamCode)
        {
            var teams = await _context.Team.Where(t => t.TeamIdentifier == teamCode).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByFranchise(int franchiseId)
        {
            var teams = await _context.Team.Where(t => t.FranchiseId == franchiseId).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByLeague(int leagueId)
        {
            var teams = await _context.Team.Where(t => t.LeagueId == leagueId).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByTeamName(string teamName)
        {
            var teams = await _context.Team.Where(t => t.TeamName.StartsWith(teamName)).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByYear(short year)
        {
            var teams = await _context.Team.Where(t => t.TeamYear == year).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<Team> GetTeam(int teamId)
        {
            var team = await _context.Team.FirstOrDefaultAsync(t => t.TeamId == teamId);

            return team;
        }

        public async Task<int> UpdateTeam(Team team)
        {
            _context.Team.Attach(team).State = EntityState.Modified;
            int recordsModified = await _context.SaveChangesAsync(true);
            return recordsModified;
        }
    }
}
