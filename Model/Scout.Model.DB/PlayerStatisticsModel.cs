using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public abstract class PlayerStatisticsModel : ScoutModel
    {
        [Column(TypeName = "varchar(16)"), Required]
        public string PlayerIdentifier { get; set; }
        [Column(TypeName = "varchar(16)"), Required]
        public string TeamIdentifier { get; set; }
        [Column(TypeName = "varchar(32)"), Required]
        public string TeamName { get; set; }
        [Column(TypeName = "smallint"), Required]
        public short TeamYear { get; set; }

        [ForeignKey("FK_PlayerFieldingStatistics_Player")]
        public virtual PlayerModel Player { get; set; }
        [ForeignKey("FK_PlayerFieldingStatistics_Team")]
        public virtual TeamModel Team { get; set; }
    }
}
