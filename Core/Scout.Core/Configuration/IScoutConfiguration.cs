namespace Scout.Core.Configuration
{
    public interface IScoutConfiguration
    {
        string MongoConnectionString { get; set; }
        string MongoDatabaseName { get; set; }
        string KeyStoreName { get; set; }
    }
}