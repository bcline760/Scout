using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Scout.Core;
using Scout.Core.Repository;
using Scout.Core.Contract;
using Scout.Core.Configuration;

using Autofac;
using MongoDB.Driver;
using MongoDB.Bson;


namespace Scout.Setup
{
    class Program
    {
        static IConfiguration Configuration { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Loading configuration...");
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Configuration = config;
            Console.WriteLine("Done...");

            Console.WriteLine("Registering IoC components...");
            var builder = SetupIoc();

            IContainer container = builder.Build(Autofac.Builder.ContainerBuildOptions.IgnoreStartableComponents);
            Console.WriteLine("Done");

            var mongo = container.Resolve<IMongoDatabase>();

            Console.WriteLine("Creating tables...");
            var tableSetup = new TableSetup(mongo);
            Task.Run(async () =>
            {
                bool tableResult = await tableSetup.CreateAccountCollection();
            }).Wait();
            Console.WriteLine("Done");

            Console.WriteLine("All done.");
            Console.ReadKey();
        }

        static ContainerBuilder SetupIoc()
        {
            var builder = new ContainerBuilder();
            var config = new ScoutConfiguration
            {
                MongoConnectionString = Configuration.GetConnectionString("MongoDB"),
                MongoDatabaseName = Configuration["DatabaseName"]
            };

            builder.RegisterInstance<IScoutConfiguration>(config);
            ContainerLoader.LoadContainers(builder);

            return builder;
        }
    }
}
