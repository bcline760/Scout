using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scout.Model.DB
{
    public class FranchiseModel : ScoutModel
    {
        [BsonElement("code")]
        public string FranchiseCode { get; set; }
        [BsonElement("name")]
        public string FranchiseName { get; set; }
        [BsonElement("active")]
        public bool IsActive { get; set; }
    }
}
