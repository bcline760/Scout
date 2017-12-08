using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scout.Model.DB
{
    public class PlayerPitchingStatistics
    {
        public int PlayerPitchingStatisticsId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public byte PitchingStint { get; set; }
        public short GamesWon { get; set; }
        public short GamesLost { get; set; }
        public short GamesPlayed { get; set; }
        public short GamesStarted { get; set; }
        public short CompleteGames { get; set; }
        public short Shutouts { get; set; }
        public short GamesSaved { get; set; }
        public decimal InningsPitched { get; set; }
        public short Hits { get; set; }
        public short Runs { get; set; }
        public short EarnedRuns { get; set; }
        public short Walks { get; set; }
        public short Strikeouts { get; set; }
        public short Homeruns { get; set; }
        public float EarnedRunAverage { get; set; }
        public short IntentionalWalks { get; set; }
        public short HitBatsmen { get; set; }
        public short WildPitches { get; set; }
        public short Balks { get; set; }
        public short TimesInducedGidp { get; set; }

        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
