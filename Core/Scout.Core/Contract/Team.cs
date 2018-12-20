using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class Team : ScoutEntity
    {
        [DataMember]
        public int FranchiseId { get; set; }
        [DataMember]
        public int LeagueId { get; set; }
        [DataMember]
        public string DivisionCode { get; set; }
        [DataMember]
        public string TeamIdentifier { get; set; }
        [DataMember] 
        public short TeamYear { get; set; }
        [DataMember] 
        public string TeamName { get; set; }
        [DataMember] 
        public byte Wins { get; set; }
        [DataMember] 
        public byte Losses { get; set; }
        [DataMember]
        public bool WonDivision { get; set; }
        [DataMember]
        public bool WonWildCard { get; set; }
        [DataMember]
        public bool WonLeague { get; set; }
        [DataMember]
        public bool WonWorldSeries { get; set; }
        [DataMember]
        public short GamesPlayed { get; set; }
        [DataMember]
        public short GamesPlayedAtHome { get; set; }
        [DataMember]
        public string ParkName { get; set; }
        [DataMember]
        public int TotalAttendance { get; set; }
        [DataMember]
        public byte ParkFactorBatting { get; set; }
        [DataMember]
        public byte ParkFactorPitching { get; set; }
        [DataMember]
        public string TeamRetrosheetId { get; set; }
        [DataMember]
        public string TeamBaseballRefId { get; set; }

        [DataMember]
        public virtual ICollection<PlayerPitchingStatistics> PlayerPitchingStatistics { get; set; }
        [DataMember]
        public virtual ICollection<PlayerBattingStatistics> PlayerBattingStatistics { get; set; }
        [DataMember]
        public virtual ICollection<PlayerFieldingStatistics> PlayerFieldingStatistics { get; set; }
    }
}
