using Scout.Core;

using Autofac;
using MongoDB.Driver;
using Scout.Core.Configuration;

namespace Scout.Model.DB.Mongo
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder container)
        {
            container.Register(r =>
            {
                var config = ScoutConfiguration.LoadConfig<MongoConfiguration>("Mongo");

                var client = new MongoClient(new MongoClientSettings
                {
                    ConnectionMode = ConnectionMode.Automatic,
                    Server = new MongoServerAddress(
                        config.MongoServer,
                        config.MongoPort
                    ),
                    UseSsl = config.UseSsl
                });

                var db = client.GetDatabase(config.MongoDatabase);
                return db;

            }).As<IMongoDatabase>().SingleInstance();
        }
    }
}
