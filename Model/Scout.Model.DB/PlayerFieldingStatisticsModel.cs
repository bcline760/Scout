using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    public class PlayerFieldingStatisticsModel
    {
        public PlayerFieldingStatisticsModel() { }

        [BsonElement("yr")]
        public short Year { get; set; }
        [BsonElement("st")]
        public short Stint { get; set; }
        [BsonElement("pos")]
        public string Position { get; set; }
        [BsonElement("g")]
        public short Games { get; set; }
        [BsonElement("gs")]
        public short GamesStarted { get; set; }
        [BsonElement("inn")]
        public short InningOuts { get; set; }
        [BsonElement("po")]
        public short PutOuts { get; set; }
        [BsonElement("as")]
        public short Assists { get; set; }
        [BsonElement("e")]
        public short Errors { get; set; }
        [BsonElement("dp")]
        public short DoublePlays { get; set; }
        [BsonElement("wp")]
        public short WildPitches { get; set; }
        [BsonElement("sb")]
        public short StolenBases { get; set; }
        [BsonElement("cs")]
        public short CaughtStealing { get; set; }
        [BsonElement("zr")]
        public short ZoneRating { get; set; }
    }
}
