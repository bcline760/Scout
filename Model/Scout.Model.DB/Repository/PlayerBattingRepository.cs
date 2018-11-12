using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;
using MongoDB.Driver;
using Scout.Core.Contract;
using Scout.Core.Repository;

namespace Scout.Model.DB.Repository
{
    public class PlayerBattingRepository : DbRepository<PlayerBattingStatisticsModel>, IPlayerBattingRepository
    {
        public PlayerBattingRepository(IMongoDatabase db) : base(db)
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

        public async new Task<PlayerBattingStatistics> GetAsync(Guid id)
        {
            var result = await base.GetAsync(id);

            return result == null ? null : Mapper.Map<PlayerBattingStatistics>(result);
        }

        public async Task<List<PlayerBattingStatistics>> GetByTeam(string teamId)
        {
            var filterDef = Builders<PlayerBattingStatisticsModel>.Filter.Eq("teamId", teamId);
            var results = await _collection.FindAsync<PlayerBattingStatisticsModel>(filterDef);

            return results.ToList().Select(Mapper.Map<PlayerBattingStatistics>).ToList();
        }

        public async Task<List<PlayerBattingStatistics>> GetByPlayer(string playerId)
        {
            var filterDef = Builders<PlayerBattingStatisticsModel>.Filter.Eq("playerId", playerId);
            var results = await _collection.FindAsync<PlayerBattingStatisticsModel>(filterDef);

            return results.ToList().Select(Mapper.Map<PlayerBattingStatistics>).ToList();
        }
    }
}
