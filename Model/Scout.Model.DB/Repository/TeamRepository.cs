using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Scout.Model.DB.Repository
{
    internal class TeamRepository : ITeamRepository
    {
        private ScoutContext _context = null;
        public TeamRepository(ScoutContext context)
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
                _context.Teams.Remove(team);
                int recordsModified = await _context.SaveChangesAsync(true);
                return recordsModified;
            }
            return -1;
        }

        public async Task<List<Team>> FindTeamsByCode(string teamCode)
        {
            var teams = await _context.Teams.Where(t => t.TeamIdentifier == teamCode).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByFranchise(int franchiseId)
        {
            var teams = await _context.Teams.Where(t => t.FranchiseId == franchiseId).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByLeague(int leagueId)
        {
            var teams = await _context.Teams.Where(t => t.LeagueId == leagueId).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByTeamName(string teamName)
        {
            var teams = await _context.Teams.Where(t => t.TeamName.StartsWith(teamName)).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByYear(short year)
        {
            var teams = await _context.Teams.Where(t => t.TeamYear == year).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<Team> GetTeam(int teamId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);

            return team;
        }

        public async Task<int> UpdateTeam(Team team)
        {
            _context.Teams.Attach(team).State = EntityState.Modified;
            int recordsModified = await _context.SaveChangesAsync(true);
            return recordsModified;
        }
    }
}
