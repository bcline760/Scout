using Scout.Core.Contract;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scout.Core.Service
{
    /// <summary>
    /// Scout service.
    /// </summary>
    public interface IScoutService<TContract> where TContract : ScoutEntity
    {
        /// <summary>
        /// Save (which could be an upsert) and object to the data store
        /// </summary>
        /// <returns>Number of records modified</returns>
        /// <param name="contract">The data contract to save</param>
        Task<ObjectModifyResult<int>> SaveAsync(TContract contract);

        /// <summary>
        /// Get all data objects in the data store. This can be an expensive operation
        /// </summary>
        /// <returns>All data objects in data store.</returns>
        Task<IEnumerable<TContract>> GetAllAsync();

        /// <summary>
        /// Get a singular data object from the data store by primary identifier
        /// </summary>
        /// <returns>Data object matching the identifier or null if not found</returns>
        /// <param name="id">Identifier.</param>
        Task<TContract> GetAsync(int id);

        /// <summary>
        /// Remove a contract from the active roster
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="contract">Contract.</param>
        Task<ObjectModifyResult<int>> Delete(TContract contract);
    }
}