using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    public class TeamModel:ScoutModel
    {
        [BsonElement("div")]
        public string DivisionCode { get; set; }
        [BsonElement("teamId")]
        public string TeamIdentifier { get; set; }
        [BsonElement("yr")]
        public short TeamYear { get; set; }
        [BsonElement("name")]
        public string TeamName { get; set; }
        [BsonElement("win")]
        public byte Wins { get; set; }
        [BsonElement("loss")]
        public byte Losses { get; set; }
        [BsonElement("wonDiv")]
        public bool WonDivision { get; set; }
        [BsonElement("wonWc")]
        public bool WonWildCard { get; set; }
        [BsonElement("wonLg")]
        public bool WonLeague { get; set; }
        [BsonElement("champ")]
        public bool WonWorldSeries { get; set; }
        [BsonElement("gp")]
        public short GamesPlayed { get; set; }
        [BsonElement("gpAtHome")]
        public short GamesPlayedAtHome { get; set; }
        [BsonElement("park")]
        public string ParkName { get; set; }
        [BsonElement("attendance")]
        public int TotalAttendance { get; set; }
        [BsonElement("pfBatting")]
        public byte ParkFactorBatting { get; set; }
        [BsonElement("pfPitching")]
        public byte ParkFactorPitching { get; set; }
        [BsonElement("retroId")]
        public string TeamRetrosheetId { get; set; }
        [BsonElement("bbrefId")]
        public string TeamBaseballRefId { get; set; }
    }
}
