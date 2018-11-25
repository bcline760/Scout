using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Scout.Model.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace Scout.Model.DB.Repository
{
    public abstract class DbRepository<TModel> where TModel : ScoutModel
    {
        protected IScoutContext Context;

        protected DbRepository(IScoutContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets all records in the collection.
        /// </summary>
        /// <returns>List of all records in the current collection</returns>
        protected async Task<List<TModel>> GetAllAsync()
        {
            return await Context.DbContext.Set<TModel>().ToListAsync();
        }

        /// <summary>
        /// Get a single record by the GUID
        /// </summary>
        /// <returns>Record matching the GUID or null if not found</returns>
        /// <param name="id">Identifier.</param>
        protected async Task<TModel> GetAsync(int id)
        {
            var result = await Context.DbContext.FindAsync<TModel>(id);
            return result;
        }

        /// <summary>
        /// Update or insert a record to the database
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="model">Model.</param>
        protected async Task<int> SaveAsync(TModel model)
        {
            await Context.DbContext.Upsert<TModel>(model)
                .On(m => m.Id)
                .RunAsync();

            return model.Id;
        }
    }
}