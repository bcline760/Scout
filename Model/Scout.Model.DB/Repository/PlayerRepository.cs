using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;
using AutoMapper;

using Scout.Core.Contract;
using Scout.Core.Repository;

namespace Scout.Model.DB.Repository
{
    public class PlayerRepository : DbRepository<PlayerModel>, IPlayerRepository
    {
        public PlayerRepository(IMongoDatabase db) : base(db)
        {

        }

        public async Task<List<Player>> FindPlayersByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> GetPlayerAsync(string playerCode)
        {
            var filterDef = Builders<PlayerModel>.Filter.Eq("playerId", playerCode);
            var results = await _collection.FindAsync<PlayerModel>(filterDef);

            return results.Any() ? Mapper.Map<Player>(results.First()) : null;
        }

        public async Task<List<Player>> LoadAllAsync()
        {
            var players = await GetAllAsync();

            return players.Select(Mapper.Map<Player>).ToList();
        }

        public async Task<int> SaveAsync(Player model)
        {
            var playerModel = Mapper.Map<PlayerModel>(model);
            long updateCount = await base.SaveAsync(playerModel);

            return (int)updateCount;
        }

        public async new Task<Player> GetAsync(Guid id)
        {
            var playerModel = await base.GetAsync(id);

            var player = playerModel == null ? Mapper.Map<Player>(playerModel) : null;
            return player;
        }
    }
}
