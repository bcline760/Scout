namespace Scout.Core.Configuration
{
    public interface IScoutConfiguration
    {
        string MongoConnectionString { get; set; }
        string DatabaseName { get; set; }
        string KeyStoreName { get; set; }
        CertificateConfiguration Certificate { get; set; }
    }
}