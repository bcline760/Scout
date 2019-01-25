//
//  TableSetup.cs
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
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Scout.Core;
using Scout.Core.Contract;
using Scout.Model.DB.Mongo;

using Autofac;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Scout.Setup
{
    internal class TableSetup
    {
        private IMongoDatabase _db = null;

        public TableSetup(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<bool> CreateAccountCollection()
        {
            string collectionName = GetCollectionName<AccountModel>();

            var options = new CreateCollectionOptions
            {
                UsePowerOf2Sizes = true
            };

            try
            {
                var collection = _db.GetCollection<AccountModel>(collectionName);
                if (collection != null)
                    return true;

                await _db.CreateCollectionAsync(collectionName, options);

                var uniqueOption = new CreateIndexOptions { Unique = true };
                var emailField = new StringFieldDefinition<AccountModel>("EmailAddress");
                var ssoTokenField = new StringFieldDefinition<AccountModel>("SsoToken");
                var ssoProviderField = new StringFieldDefinition<AccountModel>("SsoProvider");

                var emailIx = new IndexKeysDefinitionBuilder<AccountModel>().Ascending(emailField);
                var ssoTokenIx = new IndexKeysDefinitionBuilder<AccountModel>()
                    .Ascending(ssoTokenField)
                    .Ascending(ssoProviderField);

                IEnumerable<CreateIndexModel<AccountModel>> indicies = new List<CreateIndexModel<AccountModel>>
                {
                    new CreateIndexModel<AccountModel>(emailIx),
                    new CreateIndexModel<AccountModel>(ssoTokenIx)
                };

                var result = await collection.Indexes.CreateManyAsync(indicies);

                return true;
            }
            catch (MongoException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> CreatePlayerCollection()
        {
            string collectionName = GetCollectionName<PlayerModel>();
            var options = new CreateCollectionOptions
            {
                UsePowerOf2Sizes = true
            };

            try
            {
                var collection = _db.GetCollection<PlayerModel>(collectionName);
                if (collection != null)
                    return true;

                await _db.CreateCollectionAsync(collectionName, options);

                var uniqueOption = new CreateIndexOptions { Unique = true };
                var searchName = new StringFieldDefinition<PlayerModel>("PlayerSearchName");
                var playerId = new StringFieldDefinition<PlayerModel>("PlayerIdentifier");
                var playerRetro = new StringFieldDefinition<PlayerModel>("RetrosheetId");

                var searchNameIx = new IndexKeysDefinitionBuilder<PlayerModel>().Text(searchName);
                var playerIdIx = new IndexKeysDefinitionBuilder<PlayerModel>().Ascending(playerId);
                var playerRetroIx = new IndexKeysDefinitionBuilder<PlayerModel>().Ascending(playerRetro);

                var indicies = new List<CreateIndexModel<PlayerModel>>
                {
                    new CreateIndexModel<PlayerModel>(searchNameIx),
                    new CreateIndexModel<PlayerModel>(playerIdIx),
                    new CreateIndexModel<PlayerModel>(playerRetroIx)
                };

                var result = await collection.Indexes.CreateManyAsync(indicies);

                return result.Any();
            }
            catch (MongoException mex)
            {
                Console.Error.WriteLine(mex.Message);
                return false;
            }
        }

        public async Task<bool> CreateScoutingReportCollection()
        {
            string collectionName = GetCollectionName<ScoutingReportModel>();
            var options = new CreateCollectionOptions
            {
                UsePowerOf2Sizes = true
            };

            try
            {
                var collection = _db.GetCollection<ScoutingReportModel>(collectionName);
                if (collection != null)
                    return true;

                await _db.CreateCollectionAsync(collectionName, options);
                var createOptions = new CreateIndexOptions
                {
                    Unique = true
                };

                var playerIdFieldDef = new StringFieldDefinition<ScoutingReportModel>("PlayerIdentifier");
                var scoutDateFieldDef = new StringFieldDefinition<ScoutingReportModel>("ScoutingDate");

                var playerIdIx = new IndexKeysDefinitionBuilder<ScoutingReportModel>().Ascending(playerIdFieldDef);
                var scoutDateIx = new IndexKeysDefinitionBuilder<ScoutingReportModel>().Ascending(scoutDateFieldDef);
                var indicies = new List<CreateIndexModel<ScoutingReportModel>>
                {
                    new CreateIndexModel<ScoutingReportModel>(playerIdIx),
                    new CreateIndexModel<ScoutingReportModel>(scoutDateIx)
                };
                var result = await collection.Indexes.CreateManyAsync(indicies);

                return result.Any();
            }
            catch (MongoException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateTeamCollection()
        {
            var collectionName = GetCollectionName<TeamModel>();
            var options = new CreateCollectionOptions { UsePowerOf2Sizes = true };

            try
            {
                var collection = _db.GetCollection<TeamModel>(collectionName);
                if (collection != null)
                    return true;

                await _db.CreateCollectionAsync(collectionName, options);

                var teamIdFieldDef = new StringFieldDefinition<TeamModel>("TeamIdentifier");
                var retroIdFieldDef = new StringFieldDefinition<TeamModel>("TeamRetrosheetId");
                var bbIdFieldDef = new StringFieldDefinition<TeamModel>("TeamBaseballRefId");

                var teamIdIx = new IndexKeysDefinitionBuilder<TeamModel>().Ascending(teamIdFieldDef);
                var retroIdIx = new IndexKeysDefinitionBuilder<TeamModel>().Ascending(retroIdFieldDef);
                var bbIdIx = new IndexKeysDefinitionBuilder<TeamModel>().Ascending(bbIdFieldDef);

                var indicies = new List<CreateIndexModel<TeamModel>>
                {
                    new CreateIndexModel<TeamModel>(teamIdIx),
                    new CreateIndexModel<TeamModel>(retroIdIx),
                    new CreateIndexModel<TeamModel>(bbIdIx)
                };
                var result = await collection.Indexes.CreateManyAsync(indicies);

                return result.Any();
            }
            catch (MongoException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Get the name of the collection based off the model. Will appropriatly pluralize
        /// </summary>
        /// <typeparam name="TModel">The type of the model to use</typeparam>
        /// <returns>The name of the collection</returns>
        private string GetCollectionName<TModel>()
        {
            Type modelType = typeof(TModel);
            string modelName = modelType.Name;
            string collectionName = string.Empty;
            char endingCharacter = modelName[modelName.Length - 1];

            //Be smart about English
            if (char.ToLowerInvariant(endingCharacter) == 'y')
            {
                char nextChar = modelName[modelName.Length - 2];
                char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
                if (vowels.Any(v => v == nextChar))
                    collectionName = string.Concat(modelName, 's'); //bays, toys, keys
                else
                    collectionName = string.Concat(modelName.Substring(0, modelName.Length - 2), "ies"); //histories, flies, countries, etc.
            }
            else if (char.ToLowerInvariant(endingCharacter) == 'o') //Gonna have the odd case here...pianoes
            {
                collectionName = string.Concat(modelName, "es");
            }
            else
                collectionName = string.Concat(modelName, 's'); //Bows, Arrows

            Regex s_seperateWordRegex =
                            new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            return collectionName.ToLowerInvariant();
        }
    }
}
