using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Scout.Core.Repository;
using Scout.Core.Contract;

namespace Scout.Model.DB.Repository
{
    public class TeamRepository : DbRepository, ITeamRepository
    {
        public async Task<int> CreateTeam(Team team)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> FindTeamsByCode(string teamCode)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> FindTeamsByFranchise(int franchiseId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> FindTeamsByLeague(int leagueId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> FindTeamsByTeamName(string teamName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> FindTeamsByYear(short year)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> GetTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> GetTeamByYearCode(short year, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateTeam(Team team)
        {
            throw new NotImplementedException();
        }
    }
}
