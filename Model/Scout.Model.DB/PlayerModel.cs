using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;
namespace Scout.Model.DB
{
    public class PlayerModel : ScoutModel
    {
        [BsonElement("playerId")]
        public string PlayerIdentifier { get; set; }
        [BsonElement("retroId")]
        public string RetrosheetId { get; set; }
        [BsonElement("fname")]
        public string FirstName { get; set; }
        [BsonElement("lname")]
        public string LastName { get; set; }
        [BsonElement("bdate")]
        public DateTime? Birthdate { get; set; }
        [BsonElement("bcity")]
        public string BirthCity { get; set; }
        [BsonElement("bstate")]
        public string BirthStateProvince { get; set; }
        [BsonElement("bcountry")]
        public string BirthCountry { get; set; }
        [BsonElement("ddate")]
        public DateTime? DeathDate { get; set; }
        [BsonElement("dcity")]
        public string DeathCity { get; set; }
        [BsonElement("dstate")]
        public string DeathStateProvince { get; set; }
        [BsonElement("dcountry")]
        public string DeathCountry { get; set; }
        [BsonElement("mlbDebut")]
        public DateTime? MajorLeagueDebut { get; set; }
        [BsonElement("bats")]
        public string Bats { get; set; }
        [BsonElement("throws")]
        public string Throws { get; set; }
        [BsonElement("ht")]
        public short? Height { get; set; }
        [BsonElement("wt")]
        public short? Weight { get; set; }
    }
}
