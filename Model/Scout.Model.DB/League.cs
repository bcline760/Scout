using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scout.Model.DB
{
    public class League
    {
        [Key]
        public int LeagueId { get; set; }
        [Column(TypeName = "varchar(3)"),Required]
        public string LeagueCode { get; set; }
        [Column(TypeName = "varchar(32)"),Required]
        public string LeagueName { get; set; }
        [Required]
        public short YearStarted { get; set; }
        public short? YearEnded { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
