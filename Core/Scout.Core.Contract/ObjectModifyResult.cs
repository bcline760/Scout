using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Core.Contract
{
    /// <summary>
    /// Defines properties for object modification queries
    /// </summary>
    /// <typeparam name="TPrimary"></typeparam>
    [DataContract]
    public class ObjectModifyResult<TPrimary>
    {
        /// <summary>
        /// Get or set the number of records modified
        /// </summary>
        [DataMember]
        public int RecordsModified { get; set; }

        /// <summary>
        /// Get or set the primary identifer from the modified/created object
        /// </summary>
        [DataMember]
        public TPrimary PrimaryIdentifier { get; set; }
    }
}
