using System;
namespace Scout.Core.Configuration
{
    public class MongoConfiguration
    {
        public MongoConfiguration()
        {
        }
        public string MongoDatabase { get; set; }

        public string MongoServer { get; set; }

        public int MongoPort { get; set; }

        public bool UseSsl { get; set; }
    }
}
