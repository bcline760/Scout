using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    public class LeagueModel : ScoutModel
    {
        [BsonElement("code")]
        public string LeagueCode { get; set; }
        [BsonElement("name")]
        public string LeagueName { get; set; }
        [BsonElement("started")]
        public short YearStarted { get; set; }
        [BsonElement("ended")]
        public short? YearEnded { get; set; }
    }
}
