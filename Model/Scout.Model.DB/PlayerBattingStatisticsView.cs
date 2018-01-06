using System;
using System.Collections.Generic;
using System.Text;

namespace Scout.Model.DB
{
    public class PlayerBattingStatisticsView
    {
        public string PlayerIdentifier { get; set; }
        public string TeamIdentifier { get; set; }
        public string TeamName { get; set; }
        public short TeamYear { get; set; }
        public short PlateAppearances { get; set; }
        public short AtBats { get; set; }
        public short Hits { get; set; }
        public short Doubles { get; set; }
        public short Triples { get; set; }
        public short Homeruns { get; set; }
        public short RunsBattedIn { get; set; }
        public short SacrificeHits { get; set; }
        public short SacrificeFlies { get; set; }
        public short Walks { get; set; }
        public short IntentionalWalks { get; set; }
        public short HitByPitch { get; set; }
        public short Strikeouts { get; set; }
        public short StolenBases { get; set; }
        public short CaughtStealing { get; set; }
        public short GroundedIntoDoublePlay { get; set; }
        public decimal BattingAverage { get; set; }
        public decimal OnBasePercentage { get; set; }
        public decimal SluggingPercentage { get; set; }
        public decimal OnBasePlusSlugging { get; set; }
        public short OnBasePlusSluggingAdj { get; set; }
    }
}
