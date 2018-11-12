using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Driver;
using MongoDB.Bson;

namespace Scout.Model.DB.Repository
{
    public abstract class DbRepository<TModel> where TModel : ScoutModel
    {
        protected IMongoCollection<TModel> _collection;
        IMongoDatabase _database = null;

        protected DbRepository(IMongoDatabase database)
        {
            _database = database;
            _collection = GetCollection();
        }

        /// <summary>
        /// Gets all records in the collection.
        /// </summary>
        /// <returns>List of all records in the current collection</returns>
        protected async Task<List<TModel>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        /// <summary>
        /// Get a single record by the GUID
        /// </summary>
        /// <returns>Record matching the GUID or null if not found</returns>
        /// <param name="id">Identifier.</param>
        protected async Task<TModel> GetAsync(Guid id)
        {
            var filterDef = Builders<TModel>.Filter.Eq("id", id);
            var results = await _collection.FindAsync(filterDef);

            return results.FirstOrDefault();
        }

        /// <summary>
        /// Update or insert a record to the database
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="model">Model.</param>
        protected async Task<long> SaveAsync(TModel model)
        {
            var updateDef = GetUpdateDefinition(model);
            var filterDef = Builders<TModel>.Filter.Eq("id", model.Id);
            var updateResult = await _collection.UpdateOneAsync(filterDef, updateDef, new UpdateOptions
            {
                IsUpsert = true
            });

            return updateResult.ModifiedCount;
        }

        /// <summary>
        /// Get the name of the collection based off the model. Will appropriatly pluralize
        /// </summary>
        /// <typeparam name="TModel">The type of the model to use</typeparam>
        /// <returns>The name of the collection</returns>
        private IMongoCollection<TModel> GetCollection()
        {
            Type modelType = typeof(TModel);
            string modelName = modelType.Name;
            string collectionName = string.Empty;
            char endingCharacter = modelName[modelName.Length - 1];

            //Be smart about English
            if (char.ToLowerInvariant(endingCharacter) == 'y')
            {
                char nextChar = modelName[modelName.Length - 2];
                char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
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

            return _database.GetCollection<TModel>(collectionName);
        }

        /// <summary>
        /// Get the update definition query for updating
        /// </summary>
        /// <typeparam name="TModel">The type of the model to use</typeparam>
        /// <param name="model">The model to be updated</param>
        /// <returns>The update definition</returns>
        protected UpdateDefinition<TModel> GetUpdateDefinition(TModel model)
        {
            Type modelType = typeof(TModel);

            List<UpdateDefinition<TModel>> updateFields = new List<UpdateDefinition<TModel>>();
            var modelProperties = modelType.GetProperties();

            var updateBuilder = Builders<TModel>.Update;
            foreach (var property in modelProperties)
            {
                var propertyValue = property.GetValue(model);
                var update = updateBuilder.Set(property.Name, propertyValue);
                updateFields.Add(update);
            }

            var updateQuery = updateBuilder.Combine(updateFields);
            return updateQuery;
        }
    }
}