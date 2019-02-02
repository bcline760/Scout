using System;

namespace Scout.Core.Configuration
{
    public sealed class ScoutConfiguration : IScoutConfiguration
    {
        public ScoutConfiguration()
        {
        }

        public string MongoConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string KeyStoreName { get; set; }

        public CertificateConfiguration Certificate { get; set; }
    }
}
