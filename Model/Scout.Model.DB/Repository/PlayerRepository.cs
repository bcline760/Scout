using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;

using Scout.Core.Contract;
using Scout.Core.Repository;
using Scout.Model.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace Scout.Model.DB.Repository
{
    public class PlayerRepository : DbRepository<PlayerModel>, IPlayerRepository
    {
        public PlayerRepository(IScoutContext db) : base(db)
        {

        }

        public async Task<List<Player>> FindPlayersByNameAsync(string name)
        {
            var playerList = await Context.Player
                .Where(p => p.FirstName.StartsWith(name) || p.LastName.StartsWith(name))
                .Select(p => p)
                .ToListAsync();

            return playerList.Select(Mapper.Map<Player>).ToList();
        }

        public async Task<Player> GetPlayerAsync(string playerCode)
        {
            var player = await (from p in Context.Player where p.PlayerIdentifier == playerCode select p).FirstOrDefaultAsync();

            return Mapper.Map<Player>(player);
        }

        public async Task<List<Player>> LoadAllAsync()
        {
            var players = await base.GetAllAsync();
            return players.Select(Mapper.Map<Player>).ToList();
        }

        public async Task<int> SaveAsync(Player model)
        {
            return await base.SaveAsync(Mapper.Map<PlayerModel>(model));
        }

        public async new Task<Player> GetAsync(int id)
        {
            var player = await base.GetAsync(id);
            return Mapper.Map<Player>(player);
        }
    }
}
