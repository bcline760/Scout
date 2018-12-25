using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    /// <summary>
    /// Base representation of data object
    /// </summary>
    [KnownType(typeof(Franchise)), KnownType(typeof(Player)), KnownType(typeof(Account))]
    [KnownType(typeof(Team)), KnownType(typeof(ScoutingReport)), KnownType(typeof(Game))]
    [KnownType(typeof(League))]
    public abstract class ScoutEntity
    {
        /// <summary>
        /// The primary identifier of the data object
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Get or set when the object was created in the data store
        /// </summary>
        /// <value>The created at.</value>
        [DataMember]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Get or set when the object was last modified
        /// </summary>
        /// <value>The updated at.</value>
        [DataMember]
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Get or set who last modified the object
        /// </summary>
        /// <value>The updated by.</value>
        [DataMember]
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Get or set who last created the data object
        /// </summary>
        /// <value>The created by.</value>
        [DataMember]
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Scout.Core.Contract.ScoutEntity"/> is active.
        /// </summary>
        /// <value><c>true</c> if is active; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsActive { get; set; }
    }
}
