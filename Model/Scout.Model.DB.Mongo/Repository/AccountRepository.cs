//
//  UserRepository.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Scout.Core.Contract;
using Scout.Core.Repository;

namespace Scout.Model.DB.Mongo.Repository
{
    public class AccountRepository : DbRepository<AccountModel>, IAccountRepository
    {
        public AccountRepository(IMongoDatabase db)
        {
            _db = db;
            _collection = _db.GetCollection<AccountModel>(GetCollectionName());
        }

        public async Task<List<Account>> LoadAllAsync()
        {
            var allAccounts = await _collection.Find(new BsonDocument()).ToListAsync();

            return allAccounts.Select(Mapper.Map<AccountModel, Account>).ToList();
        }

        public async Task<int> SaveAsync(Account model)
        {
            var entity = Mapper.Map<Account, AccountModel>(model);
            return await SaveAsync(entity);
        }

        public new async Task<Account> GetAsync(Guid id)
        {
            var account = await base.GetAsync(id);

            return Mapper.Map<AccountModel, Account>(account);
        }

        public async Task<Account> LoadByEmailAsync(string email)
        {
            var builder = Builders<AccountModel>.Filter.Eq(e => e.EmailAddress, email);
            var account = await _collection.FindAsync(builder);

            return Mapper.Map<AccountModel, Account>(account.FirstOrDefault());
        }

        public async Task<Account> LoadByProvider(SingleSignOnProvider provider, string token)
        {
            int nProvider = (int)provider;
            var filter = Builders<AccountModel>.Filter;
            var filterBuilder = filter.Eq(e => e.SsoToken, token) & filter.Eq(e => e.SsoProvider, nProvider);
            var accountResult = await _collection.FindAsync(filterBuilder);

            return Mapper.Map<AccountModel, Account>(accountResult.FirstOrDefault());
        }
    }
}
