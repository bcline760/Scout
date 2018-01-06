using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Model.Contract
{
    public class PlayerBattingStatistics:PlayerStatistics
    {
        [DataMember]
        public short PlateAppearances { get; set; }
        [DataMember]
        public short AtBats { get; set; }
        [DataMember]
        public short Hits { get; set; }
        [DataMember]
        public short Doubles { get; set; }
        [DataMember]
        public short Triples { get; set; }
        [DataMember]
        public short Homeruns { get; set; }
        [DataMember]
        public short RunsBattedIn { get; set; }
        [DataMember]
        public short SacrificeHits { get; set; }
        [DataMember]
        public short SacrificeFlies { get; set; }
        [DataMember]
        public short Walks { get; set; }
        [DataMember]
        public short IntentionalWalks { get; set; }
        [DataMember]
        public short HitByPitch { get; set; }
        [DataMember]
        public short Strikeouts { get; set; }
        [DataMember]
        public short StolenBases { get; set; }
        [DataMember]
        public short CaughtStealing { get; set; }
        [DataMember]
        public short GroundedIntoDoublePlay { get; set; }
        [DataMember]
        public decimal BattingAverage { get; set; }
        [DataMember]
        public decimal OnBasePercentage { get; set; }
        [DataMember]
        public decimal SluggingPercentage { get; set; }
        [DataMember]
        public decimal OnBasePlusSlugging { get; set; }
        [DataMember]
        public short OnBasePlusSluggingAdj { get; set; }
    }
}
