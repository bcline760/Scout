using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public abstract class ScoutEntity
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public DateTime? UpdatedAt { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
    }
}
