using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class League : ScoutEntity
    {
        [DataMember]
        public string LeagueCode { get; set; }
        [DataMember]
        public string LeagueName { get; set; }
        [DataMember]
        public short YearStarted { get; set; }
        [DataMember]
        public short? YearEnded { get; set; }
    }
}
