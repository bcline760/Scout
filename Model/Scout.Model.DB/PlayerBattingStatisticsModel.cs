
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class PlayerBattingStatisticsModel : PlayerStatisticsModel
    {
        [Column(TypeName ="smallint"),Required]
        public short PlateAppearances { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short AtBats { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Hits { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Doubles { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Triples { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Homeruns { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short RunsBattedIn { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short SacrificeHits { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short SacrificeFlies { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Walks { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short IntentionalWalks { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short HitByPitch { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short Strikeouts { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short StolenBases { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short CaughtStealing { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short GroundedIntoDoublePlay { get; set; }
        [Column(TypeName = "decimal(0,3)"), Required]
        public decimal BattingAverage { get; set; }
        [Column(TypeName = "decimal(0,3)"), Required]
        public decimal OnBasePercentage { get; set; }
        [Column(TypeName = "decimal(0,3)"), Required]
        public decimal SluggingPercentage { get; set; }
        [Column(TypeName = "decimal(0,3)"), Required]
        public decimal OnBasePlusSlugging { get; set; }
    }
}
