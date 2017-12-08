﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Model.Contract
{
    public class PlayerPitchingStatistics
    {
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public byte PitchingStint { get; set; }
        [DataMember]
        public short GamesWon { get; set; }
        [DataMember]public short GamesLost { get; set; }
        public short GamesPlayed { get; set; }
        [DataMember]
        public short GamesStarted { get; set; }
        [DataMember]
        public short CompleteGames { get; set; }
        [DataMember]
        public short Shutouts { get; set; }
        [DataMember]
        public short GamesSaved { get; set; }
        [DataMember]
        public decimal InningsPitched { get; set; }
        [DataMember]
        public short Hits { get; set; }
        [DataMember]
        public short Runs { get; set; }
        [DataMember]
        public short EarnedRuns { get; set; }
        [DataMember]
        public short Walks { get; set; }
        [DataMember]
        public short Strikeouts { get; set; }
        [DataMember]
        public short Homeruns { get; set; }
        [DataMember]
        public float EarnedRunAverage { get; set; }
        [DataMember]
        public short IntentionalWalks { get; set; }
        [DataMember]
        public short HitBatsmen { get; set; }
        [DataMember]
        public short WildPitches { get; set; }
        [DataMember]
        public short Balks { get; set; }
        [DataMember]
        public short TimesInducedGidp { get; set; }
    }
}
