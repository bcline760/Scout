using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB
{
    [BsonIgnoreExtraElements]
    public class ScoutPointerModel
    {
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("created")]
        public DateTime Created { get; set; }
        [BsonElement("updated")]
        public DateTime? Updated { get; set; }
    }
}
