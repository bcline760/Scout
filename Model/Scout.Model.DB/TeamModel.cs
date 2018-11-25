
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class TeamModel : ScoutModel
    {
        public int FranchiseId { get; set; }
        public int LeagueId { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string DivisionCode { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(3)"), Required]
        public string TeamIdentifier { get; set; }
        [Required]
        public short TeamYear { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(64)"), Required]
        public string TeamName { get; set; }
        [Required]
        public byte Wins { get; set; }
        [Required]
        public byte Losses { get; set; }
        [Required]
        public bool WonDivision { get; set; }
        [Required]
        public bool WonWildCard { get; set; }
        [Required]
        public bool WonLeague { get; set; }
        [Required]
        public bool WonWorldSeries { get; set; }
        [Required]
        public short GamesPlayed { get; set; }
        [Required]
        public short GamesPlayedAtHome { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string ParkName { get; set; }
        [Required]
        public int TotalAttendance { get; set; }
        [Required]
        public byte ParkFactorBatting { get; set; }
        [Required]
        public byte ParkFactorPitching { get; set; }
        [Column(TypeName = "varchar(3)"), Required]
        public string TeamRetrosheetId { get; set; }
        [Column(TypeName = "varchar(3)"), Required]
        public string TeamBaseballRefId { get; set; }

        [ForeignKey("FK_Team_League")]
        public LeagueModel League { get; set; }
        [ForeignKey("FK_Team_Franchise")]
        public FranchiseModel Franchise { get; set; }
    }
}
