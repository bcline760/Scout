using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Scout.Web.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices((IServiceCollection obj) => obj.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
