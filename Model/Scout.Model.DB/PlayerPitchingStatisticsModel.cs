using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class PlayerPitchingStatisticsModel : PlayerStatisticsModel
    {
        [Column(TypeName = "tinyint"), Required]
        public byte PitchingStint { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short GamesWon { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short GamesLost { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short GamesPlayed { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short GamesStarted { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short CompleteGames { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Shutouts { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short GamesSaved { get; set; }
        [Column(TypeName = "decimal(3,2)"), Required]
        public decimal InningsPitched { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Hits { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Runs { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short EarnedRuns { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Walks { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Strikeouts { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Homeruns { get; set; }
        [Column(TypeName = "smallint"), Required]
        public decimal EarnedRunAverage { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short IntentionalWalks { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short HitBatsmen { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short WildPitches { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Balks { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short TimesInducedGidp { get; set; }
    }
}
