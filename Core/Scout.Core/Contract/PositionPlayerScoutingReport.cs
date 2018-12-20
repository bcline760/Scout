using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class PositionPlayerScoutingReport
    {
        public PositionPlayerScoutingReport()
        {
        }

        [DataMember]
        public byte PresentHittingAbilityGrade { get; set; }
        [DataMember]
        public byte FutureHittingAbilityGrade { get; set; }
        [DataMember]
        public byte PresentPowerGrade { get; set; }
        [DataMember]
        public byte FuturePowerGrade { get; set; }
        [DataMember]
        public byte PresentRunningSpeedGrade { get; set; }
        [DataMember] 
        public byte FutureRunningSpeedGrade { get; set; }
        [DataMember]
        public byte PresentBaserunningGrade { get; set; }
        [DataMember]
        public byte FutureBaserunningGrade { get; set; }
        [DataMember]
        public byte PresentArmStrengthGrade { get; set; }
        [DataMember]
        public byte FutureArmStrengthGrade { get; set; }
        [DataMember]
        public byte PresentArmAccuracyGrade { get; set; }
        [DataMember]
        public byte FutureArmAccuracyGrade { get; set; }
        [DataMember]
        public byte PresentFieldingGrade { get; set; }
        [DataMember]
        public byte FutureFieldingGrade { get; set; }
        [DataMember]
        public byte PresentRangeGrade { get; set; }
        [DataMember]
        public byte FutureRangeGrade { get; set; }
        [DataMember]
        public byte PresentAggressivenessGrade { get; set; }
        [DataMember]
        public byte FutureAggressivenessGrade { get; set; }
        [DataMember]
        public byte PresentBaseballInstinctsGrade { get; set; }
        [DataMember]
        public byte FutureBaseballInstinctsGrade { get; set; }
    }
}
