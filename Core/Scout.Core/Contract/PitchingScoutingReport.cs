using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class PitchingScoutingReport
    {
        [DataMember]
        public byte PresentFastballGrade { get; set; }
        [DataMember]
        public byte FutureFastballGrade { get; set; }
        [DataMember]
        public Range FastballVelocity { get; set; }
        [DataMember]
        public byte PresentFastballMovementGrade { get; set; }
        [DataMember]
        public byte FutureFastballMovementGrade { get; set; }
        [DataMember]
        public byte PresentCurveballGrade { get; set; }
        [DataMember]
        public byte FutureCurveballGrade { get; set; }
        [DataMember]
        public Range CurveballVelocity { get; set; }
        [DataMember]
        public byte PresentSliderGrade { get; set; }
        [DataMember]
        public byte FutureSliderGrade { get; set; }
        [DataMember]
        public Range SliderVelocity { get; set; }
        [DataMember]
        public byte PresentOtherPitchGrade { get; set; }
        [DataMember]
        public byte FutureOtherPitchGrade { get; set; }
        [DataMember]
        public Range OtherPitchVelocity { get; set; }
        [DataMember]
        public byte PresentControlGrade { get; set; }
        [DataMember]
        public byte FutureControlGrade { get; set; }
        [DataMember]
        public byte PresentPoiseGrade { get; set; }
        [DataMember]
        public byte FuturePoiseGrade { get; set; }
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
