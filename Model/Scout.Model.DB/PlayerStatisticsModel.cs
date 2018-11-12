using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    [BsonKnownTypes(typeof(PlayerBattingStatisticsModel), typeof(PlayerFieldingStatisticsModel),
                    typeof(PlayerPitchingStatisticsModel))]
    [BsonIgnoreExtraElements]
    public abstract class PlayerStatisticsModel : ScoutModel
    {
        [BsonElement("playerId")]
        public string PlayerIdentifier { get; set; }
        [BsonElement("teamId")]
        public string TeamIdentifier { get; set; }
        [BsonElement("teamName")]
        public string TeamName { get; set; }
        [BsonElement("teamYr")]
        public short TeamYear { get; set; }
    }
}
