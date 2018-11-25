using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class PlayerFieldingStatisticsModel : PlayerStatisticsModel
    {
        public PlayerFieldingStatisticsModel() { }

        [Column(TypeName = "smallint")]
        [Required]
        public short Year { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short Stint { get; set; }
        [Column(TypeName = "varchar(2)")]
        [Required]
        public string Position { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short Games { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short GamesStarted { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short InningOuts { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short PutOuts { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short Assists { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short Errors { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short DoublePlays { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short WildPitches { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short StolenBases { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short CaughtStealing { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short ZoneRating { get; set; }
    }
}
