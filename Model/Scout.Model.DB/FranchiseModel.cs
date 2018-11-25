using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scout.Model.DB
{
    public class FranchiseModel : ScoutModel
    {
        [Required]
        public string FranchiseCode { get; set; }
        [Required]
        public string FranchiseName { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public ICollection<TeamModel> Teams { get; set; }
    }
}
