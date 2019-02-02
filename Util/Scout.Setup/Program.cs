using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Scout.Core;
using Scout.Core.Configuration;
using Scout.Core.Service;
using Scout.Core.Contract;
using Scout.Core.Security;

using Autofac;
using MongoDB.Driver;
using AutoMapper;
using Scout.Model.DB.Mongo;

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
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DatabaseModelMap());
            });
            Console.WriteLine("Done");

            var mongo = container.Resolve<IMongoDatabase>();

            Console.WriteLine("Creating tables...");
            var tableSetup = new TableSetup(mongo);
            Task.Run(async () =>
            {
                bool tableResult = await tableSetup.CreateAccountCollection();
                if (tableResult)
                    tableResult = await tableSetup.CreatePlayerCollection();
                if (tableResult)
                    tableResult = await tableSetup.CreateScoutingReportCollection();
                if (tableResult)
                    tableResult = await tableSetup.CreateTeamCollection();

                if (!tableResult)
                    throw new InvalidProgramException("Table creation failed. Review table(s) not created and exceptions generated");
            }).Wait();
            Console.WriteLine("Done");

            Console.WriteLine("Creating master account...");
            var svc = container.Resolve<IAccountService>();
            var masterAcct = new AccountRegister
            {
                EmailAddress = "sys.admin@scoutr.com",
                FirstName = "System",
                LastName = "Admin",
                Password = "",
                SsoProvider = SingleSignOnProvider.None
            };
            Task.Run(async () =>
            {
                var cuenta = await svc.LoadByEmail(masterAcct.EmailAddress);
                if (cuenta == null)
                {
                    var authentication = await svc.RegisterAsync(masterAcct);
                    Console.WriteLine(authentication.AuthenticationMessage);
                }
            }).Wait();
            Console.WriteLine("All done.");
            Console.ReadKey();
        }

        static ContainerBuilder SetupIoc()
        {
            var builder = new ContainerBuilder();

            var config = Configuration.Get<ScoutConfiguration>();
            config.MongoConnectionString = Configuration.GetConnectionString("MongoDB");

            var enc = new ScoutEncryption(config);
            builder.RegisterInstance<IScoutConfiguration>(config);
            builder.RegisterInstance<IScoutEncryption>(enc);
            ContainerLoader.LoadContainers(builder);

            return builder;
        }
    }
}
