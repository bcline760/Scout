using System;
using Microsoft.Extensions.Configuration;

namespace Scout.Core.Configuration
{
    public abstract class ScoutConfiguration
    {
        public ScoutConfiguration()
        {
        }

        /// <summary>
        /// Loads a configuration section from the configuration file
        /// </summary>
        /// <returns>Object representing the configuration section</returns>
        /// <param name="sectionName">Section name.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T LoadConfig<T>(string sectionName) where T : class
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var section = config.GetSection(sectionName);
            return section.Get<T>();
        }
    }
}
