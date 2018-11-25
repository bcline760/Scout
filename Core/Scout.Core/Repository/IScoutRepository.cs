using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scout.Core.Repository
{
    /// <summary>
    /// Base repository for all Scout models
    /// </summary>
    public interface IScoutRepository<TModel>
    {
        /// <summary>
        /// Loads all.
        /// </summary>
        /// <returns>The all.</returns>
        Task<List<TModel>> LoadAllAsync();

        /// <summary>
        /// Get a DB entity by the ID
        /// </summary>
        /// <returns>A DB entity matching the GUID or null</returns>
        /// <param name="id">The GUID</param>
        Task<TModel> GetAsync(int id);

        /// <summary>
        /// Save a record to the database
        /// </summary>
        /// <returns>The number of records modified.</returns>
        /// <param name="model">The db object to be saved to the database</param>
        Task<int> SaveAsync(TModel model);
    }
}
