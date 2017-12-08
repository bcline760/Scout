using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scout.Model.DB
{
    public class Franchise
    {
        [Key]
        public int FranchiseId { get; set; }
        [Column(TypeName = "varchar(4)"),Required]
        public string FranchiseCode { get; set; }
        [Column(TypeName = "varchar(64)"), Required]
        public string FranchiseName { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
