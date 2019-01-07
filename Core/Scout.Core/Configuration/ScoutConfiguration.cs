using System;

namespace Scout.Core.Configuration
{
    public sealed class ScoutConfiguration : IScoutConfiguration
    {
        public ScoutConfiguration()
        {
        }

        public string MongoConnectionString { get; set; }

        public string MongoDatabaseName { get; set; }
    }
}
