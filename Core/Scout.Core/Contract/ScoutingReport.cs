using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class ScoutingReport : ScoutEntity
    {
        public ScoutingReport()
        {
        }

        [DataMember]
        public DateTime ScoutingDate { get; set; }
        [DataMember]
        public short GamesSeen { get; set; }
        [DataMember]
        public short InningsSeen { get; set; }
        [DataMember]
        public string CurrentLevel { get; set; }
        [DataMember]
        public string NextTearLevel { get; set; }
        [DataMember]
        public string TopLevel { get; set; }
        [DataMember]
        public string PlayerOfInterest { get; set; }
        [DataMember]
        public byte PresentOverallEvaluationGrade { get; set; }
        [DataMember]
        public byte FutureOverallEvaluationGrade { get; set; }

        [DataMember]
        public PitchingScoutingReport PitchingReport { get; set; }
        [DataMember]
        public PositionPlayerScoutingReport PositionPlayerReport { get; set; }

        [DataMember]
        public string ScoutingSummary { get; set; }


    }
}
