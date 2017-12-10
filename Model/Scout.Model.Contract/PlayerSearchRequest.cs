using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Model.Contract
{
    [DataContract]
    public class PlayerSearchRequest
    {
        /// <summary>
        /// The unique identifier of the player
        /// </summary>
        [IgnoreDataMember]
        public int? PlayerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PlayerCode { get; set; }

        /// <summary>
        /// Beginning of playing time window
        /// </summary>
        [DataMember]
        public DateTime? ActiveWindowStart { get; set; }

        /// <summary>
        /// End of player's playing time window
        /// </summary>
        [DataMember]
        public DateTime? ActiveWindowEnd { get; set; }

        /// <summary>
        /// The player's given name to search
        /// </summary>
        [DataMember]
        public string PlayerName { get; set; }
    }
}
