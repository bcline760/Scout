using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Scout.Model.DB
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(LeagueModel), typeof(PlayerModel), typeof(FranchiseModel),
                    typeof(TeamModel), typeof(PlayerStatisticsModel))]
    public abstract class ScoutModel
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }
        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }
    }
}
