//
//  PlayerRepository.cs
//
//  Author:
//       bcline <bcline760@yahoo.com>
//
//  Copyright (c) 2019 ${CopyrightHolder}
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using AutoMapper;
using Scout.Core.Contract;
using Scout.Core.Repository;

namespace Scout.Model.DB.Mongo.Repository
{
    internal class PlayerRepository : DbRepository<PlayerModel>, IPlayerRepository
    {
        public PlayerRepository(IMongoDatabase db)
        {
            _db = null;
            _collection = _db.GetCollection<PlayerModel>(GetCollectionName());
        }

        public async Task<List<Player>> FindPlayersByNameAsync(string name)
        {
            var filter = Builders<PlayerModel>.Filter.Text(name);
            var results = await _collection.FindAsync(filter);
            return results.ToList().Select(Mapper.Map<PlayerModel, Player>).ToList();
        }

        public new async Task<Player> GetAsync(Guid id)
        {
            var model = await base.GetAsync(id);
            return model != null ? Mapper.Map<PlayerModel, Player>(model) : null;
        }

        public async Task<Player> GetPlayerAsync(string playerCode)
        {
            var filter = Builders<PlayerModel>.Filter.Eq("player_code", playerCode);
            var result = await _collection.FindAsync(filter);

            return Mapper.Map<PlayerModel, Player>(result.FirstOrDefault());
        }

        public async Task<List<Player>> LoadAllAsync()
        {
            var allPlayers = await _collection.Find(new BsonDocument()).ToListAsync();

            return allPlayers.Select(Mapper.Map<PlayerModel, Player>).ToList();
        }

        public async Task<int> SaveAsync(Player model)
        {
            return await SaveAsync(Mapper.Map<Player, PlayerModel>(model));
        }
    }
}
