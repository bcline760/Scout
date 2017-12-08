using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scout.Model.Contract
{
    public class Player
    {
        public Player()
        {
            BattingStatistics = new List<PlayerBattingStatistics>();
            AdvancedBattingStatistics = new List<PlayerAdvancedBattingStatistics>();
            PitchingStatistics = new List<PlayerPitchingStatistics>();
        }

        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public string PlayerIdentifier { get; set; }
        [DataMember]
        public string RetrosheetId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime? Birthdate { get; set; }
        [DataMember]
        public string BirthCity { get; set; }
        [DataMember]
        public string BirthStateProvince { get; set; }
        [DataMember]
        public string BirthCountry { get; set; }
        [DataMember]
        public DateTime? DeathDate { get; set; }
        [DataMember]
        public string DeathCity { get; set; }
        [DataMember]
        public string DeathStateProvince { get; set; }
        [DataMember]
        public string DeathCountry { get; set; }
        [DataMember]
        public string PrimaryPosition { get; set; }
        [DataMember]
        public int? DraftTeamId { get; set; }
        [DataMember]
        public short? DraftYear { get; set; }
        [DataMember]
        public DateTime? MajorLeagueDebut { get; set; }
        [DataMember]
        public string Bats { get; set; }
        [DataMember]
        public string Throws { get; set; }
        [DataMember]
        public short? Height { get; set; }
        [DataMember]
        public short? Weight { get; set; }

        [DataMember]
        public List<PlayerBattingStatistics> BattingStatistics { get; set; }
        [DataMember]
        public List<PlayerAdvancedBattingStatistics> AdvancedBattingStatistics { get; set; }
        [DataMember]
        public List<PlayerPitchingStatistics> PitchingStatistics { get; set; }
    }
}
