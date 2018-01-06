using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public class TeamRepository : DbRepository, ITeamRepository
    {
        public TeamRepository(IScoutContext context):base(context)
        {
        }

        public async Task<int> CreateTeam(Team team)
        {
            Context.Add<Team>(team);

            int recordsModified = await Context.SaveChangesAsync(true);
            return recordsModified;
        }

        public async Task<int> DeleteTeam(int teamId)
        {
            var team = await GetTeam(teamId);

            if (team != null)
            {
                Context.Team.Remove(team);
                int recordsModified = await Context.SaveChangesAsync(true);
                return recordsModified;
            }
            return -1;
        }

        public async Task<List<Team>> FindTeamsByCode(string teamCode)
        {
            var teams = await Context.Team.Where(t => t.TeamIdentifier == teamCode).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<Team> GetTeamByYearCode(short year, string code)
        {
            var team = await Context.Team.FirstOrDefaultAsync(t => t.TeamIdentifier == code && t.TeamYear == year);

            return team;
        }

        public async Task<List<Team>> FindTeamsByFranchise(int franchiseId)
        {
            var teams = await Context.Team.Where(t => t.FranchiseId == franchiseId).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByLeague(int leagueId)
        {
            var teams = await Context.Team.Where(t => t.LeagueId == leagueId).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByTeamName(string teamName)
        {
            var teams = await Context.Team.Where(t => t.TeamName.StartsWith(teamName)).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> FindTeamsByYear(short year)
        {
            var teams = await Context.Team.Where(t => t.TeamYear == year).Select(t => t).ToListAsync();

            return teams;
        }

        public async Task<Team> GetTeam(int teamId)
        {
            var team = await Context.Team.FirstOrDefaultAsync(t => t.TeamId == teamId);

            return team;
        }

        public async Task<int> UpdateTeam(Team team)
        {
            Context.Team.Attach(team).State = EntityState.Modified;
            int recordsModified = await Context.SaveChangesAsync(true);
            return recordsModified;
        }
    }
}
