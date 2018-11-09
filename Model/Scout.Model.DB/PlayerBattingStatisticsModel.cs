
using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    public class PlayerBattingStatisticsModel
    {
        [BsonElement("pa")]
        public short PlateAppearances { get; set; }
        [BsonElement("ab")]
        public short AtBats { get; set; }
        [BsonElement("h")]
        public short Hits { get; set; }
        [BsonElement("db")]
        public short Doubles { get; set; }
        [BsonElement("tr")]
        public short Triples { get; set; }
        [BsonElement("hr")]
        public short Homeruns { get; set; }
        [BsonElement("rbi")]
        public short RunsBattedIn { get; set; }
        [BsonElement("sh")]
        public short SacrificeHits { get; set; }
        [BsonElement("sf")]
        public short SacrificeFlies { get; set; }
        [BsonElement("bb")]
        public short Walks { get; set; }
        [BsonElement("ibb")]
        public short IntentionalWalks { get; set; }
        [BsonElement("hbp")]
        public short HitByPitch { get; set; }
        [BsonElement("k")]
        public short Strikeouts { get; set; }
        [BsonElement("sb")]
        public short StolenBases { get; set; }
        [BsonElement("cs")]
        public short CaughtStealing { get; set; }
        [BsonElement("gidp")]
        public short GroundedIntoDoublePlay { get; set; }
        [BsonElement("avg")]
        public decimal BattingAverage { get; set; }
        [BsonElement("obp")]
        public decimal OnBasePercentage { get; set; }
        [BsonElement("slg")]
        public decimal SluggingPercentage { get; set; }
        [BsonElement("ops")]
        public decimal OnBasePlusSlugging { get; set; }
    }
}
