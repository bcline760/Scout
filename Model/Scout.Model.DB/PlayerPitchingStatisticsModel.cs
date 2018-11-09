using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    public class PlayerPitchingStatisticsModel
    {
        [BsonElement("stint")]
        public byte PitchingStint { get; set; }
        [BsonElement("w")]
        public short GamesWon { get; set; }
        [BsonElement("l")]
        public short GamesLost { get; set; }
        [BsonElement("gp")] 
        public short GamesPlayed { get; set; }
        [BsonElement("gs")]
        public short GamesStarted { get; set; }
        [BsonElement("cg")]
        public short CompleteGames { get; set; }
        [BsonElement("so")]
        public short Shutouts { get; set; }
        [BsonElement("sv")]
        public short GamesSaved { get; set; }
        [BsonElement("ip")]
        public decimal InningsPitched { get; set; }
        [BsonElement("h")]
        public short Hits { get; set; }
        [BsonElement("r")]
        public short Runs { get; set; }
        [BsonElement("er")]
        public short EarnedRuns { get; set; }
        [BsonElement("bb")]
        public short Walks { get; set; }
        [BsonElement("k")]
        public short Strikeouts { get; set; }
        [BsonElement("hr")]
        public short Homeruns { get; set; }
        [BsonElement("era")]
        public float EarnedRunAverage { get; set; }
        [BsonElement("ibb")]
        public short IntentionalWalks { get; set; }
        [BsonElement("hb")]
        public short HitBatsmen { get; set; }
        [BsonElement("wp")]
        public short WildPitches { get; set; }
        [BsonElement("bk")]
        public short Balks { get; set; }
        [BsonElement("gidp")]
        public short TimesInducedGidp { get; set; }
    }
}
