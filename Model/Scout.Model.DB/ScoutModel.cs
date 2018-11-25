using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scout.Model.DB
{
    public abstract class ScoutModel
    {
        [Key]
        public int Id { get; set; }
        [ConcurrencyCheck,Required]
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        [Column(TypeName = "varchar(32)")]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "varchar(32)")]
        public string CreatedBy { get; set; }
    }
}
