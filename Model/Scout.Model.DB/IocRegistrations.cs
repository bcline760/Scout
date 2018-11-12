using Scout.Core.Repository;
using Scout.Model.DB.Repository;

using Autofac;

using MongoDB.Driver;
namespace Scout.Core.Context
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder container)
        {
            container.Register(r =>
            {
                var client = new MongoClient(new MongoClientSettings
                {

                });

                var db = client.GetDatabase("");

                return db;
            }).As<IMongoDatabase>();

            container.RegisterType<PlayerRepository>().As<IPlayerRepository>();
        }
    }
}
