using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class Franchise : ScoutEntity
    {
        public Franchise()
        {
        }

        [DataMember]
        public string FranchiseCode { get; set; }
        [DataMember]
        public string FranchiseName { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }
}
