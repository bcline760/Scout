using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;
using Scout.Core.Repository;
using Scout.Core.Contract;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public class TeamRepository : DbRepository<TeamModel>, ITeamRepository
    {
        public TeamRepository(IScoutContext db) : base(db)
        { }

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

        public async Task<Team> GetTeamByYearCode(short year, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> LoadAllAsync()
        {
            var teams = await GetAllAsync();

            return teams.Select(Mapper.Map<Team>).ToList();
        }

        public async Task<int> SaveAsync(Team model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var teamModel = Mapper.Map<TeamModel>(model);
            var result = await SaveAsync(model);

            return result;
        }

        public async new Task<Team> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
