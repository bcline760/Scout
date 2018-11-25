using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class PlayerModel : ScoutModel
    {
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(16)"), Required]
        public string PlayerIdentifier { get; set; }
        [Column(TypeName = "varchar(16)"), Required]
        public string RetrosheetId { get; set; }
        [Column(TypeName = "varchar(32)"), Required]
        public string FirstName { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(32)"), Required]
        public string LastName { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        [Column(TypeName = "varchar(64)"), Required]
        public string BirthCity { get; set; }
        [Column(TypeName = "varchar(64)")]
        public string BirthStateProvince { get; set; }
        [Column(TypeName = "varchar(64)"), Required]
        public string BirthCountry { get; set; }
        public Nullable<System.DateTime> DeathDate { get; set; }
        [Column(TypeName = "varchar(64)")]
        public string DeathCity { get; set; }
        [Column(TypeName = "varchar(64)")]
        public string DeathStateProvince { get; set; }
        [Column(TypeName = "varchar(64)")]
        public string DeathCountry { get; set; }
        public Nullable<System.DateTime> MajorLeagueDebut { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string Bats { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string Throws { get; set; }
        public Nullable<short> Height { get; set; }
        public Nullable<short> Weight { get; set; }

        public virtual ICollection<PlayerPitchingStatisticsModel> PlayerPitchingStatistics { get; set; }
        public virtual ICollection<PlayerBattingStatisticsModel> PlayerBattingStatistics { get; set; }
        public virtual ICollection<PlayerFieldingStatisticsModel> PlayerFieldingStatistics { get; set; }
    }
}
