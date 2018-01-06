using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class PlayerFieldingStatistics
    {
        public PlayerFieldingStatistics() { }

        [Key, Column(TypeName = "int")]
        public int PlayerFieldingStatisticsId { get; set; }
        [Column(TypeName = "int")]
        [Required]
        public int PlayerId { get; set; }
        [Column(TypeName = "int")]
        [Required]
        public int TeamId { get; set; }
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

        [ForeignKey("FK_PlayerFieldingStatistics_Player")]
        public virtual Player Player { get; set; }
        [ForeignKey("FK_PlayerFieldingStatistics_Team")]
        public virtual Team Team { get; set; }
    }
}
