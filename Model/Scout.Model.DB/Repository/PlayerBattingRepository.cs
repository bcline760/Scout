using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using Scout.Core.Contract;
using Scout.Core.Repository;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public class PlayerBattingRepository : DbRepository<PlayerBattingStatisticsModel>, IPlayerBattingRepository
    {
        public PlayerBattingRepository(IScoutContext context) : base(context)
        {
        }

        public async Task<List<PlayerBattingStatistics>> LoadAllAsync()
        {
            var players = await GetAllAsync();

            return players.Select(Mapper.Map<PlayerBattingStatistics>).ToList();
        }

        public async Task<int> SaveAsync(PlayerBattingStatistics model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var pbsModel = Mapper.Map<PlayerBattingStatisticsModel>(model);
            var result = await SaveAsync(pbsModel);

            return (int)result;
        }

        public async new Task<PlayerBattingStatistics> GetAsync(int id)
        {
            var result = await base.GetAsync(id);

            return result == null ? null : Mapper.Map<PlayerBattingStatistics>(result);
        }

        public async Task<List<PlayerBattingStatistics>> GetByTeam(string teamId)
        {
            var results = await (from c in Context.PlayerBattingStatistics where c.TeamIdentifier == teamId select c).ToListAsync();

            return results.Select(Mapper.Map<PlayerBattingStatistics>).ToList();
        }

        public async Task<List<PlayerBattingStatistics>> GetByPlayer(string playerId)
        {
            var results = await (from c in Context.PlayerBattingStatistics where c.PlayerIdentifier == playerId select c).ToListAsync();

            return results.Select(Mapper.Map<PlayerBattingStatistics>).ToList();
        }
    }
}
