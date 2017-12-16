using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Model.Contract
{
    public class PlayerAdvancedBattingStatistics
    {
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public decimal BattingAverage { get; set; }
        [DataMember]
        public decimal TotalAverage { get; set; }
        [DataMember]
        public decimal OnBasePercentage { get; set; }
        [DataMember]
        public decimal SluggingPercentage { get; set; }
        [DataMember]
        public decimal OnBasePlusSlugging { get; set; }
        [DataMember]
        public short OnBasePlusSluggingAdj { get; set; }
        [DataMember]
        public decimal BattingAvgOfBallsInPlay { get; set; }
        [DataMember]
        public decimal HomeRunPercentage { get; set; }
        [DataMember]
        public decimal StrikeoutPercentage { get; set; }
        [DataMember]
        public decimal WalkPercentage { get; set; }
        [DataMember]
        public decimal ExtraBaseHitPercentage { get; set; }
        [DataMember]
        public decimal InPlayPercentage { get; set; }
    }
}
