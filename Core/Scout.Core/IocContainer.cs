using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Scout.Core
{
    public class IocContainer
    {
        private static readonly IServiceCollection _container = new ServiceCollection();

        /// <summary>
        /// Static instance of the unity container used to manage the container registrations.
        /// </summary>
        public static IServiceCollection Services
        {
            get { return _container; }
        }


        /// <summary>
        /// Registers an interface TFrom with a concrete type resolution TTo.
        /// </summary>
        /// <typeparam name="TFrom">Type of the object to resolve from</typeparam>
        /// <typeparam name="TTo">Type of the object to resolve to</typeparam>
        public static void RegisterType<TFrom, TTo>(DependencyLifetime lifetime) 
            where TTo : class, TFrom
            where TFrom: class
        {
            switch (lifetime)
            {
                case DependencyLifetime.Scoped:
                    Services.AddScoped<TFrom, TTo>();
                    break;
                case DependencyLifetime.Transient:
                    Services.AddTransient<TFrom, TTo>();
                    break;
                case DependencyLifetime.Singleton:
                    Services.AddSingleton<TFrom, TTo>();
                    break;
            }
        }

        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <typeparam name="TSvc">The type of service to resolve</typeparam>
        /// <param name="required">Boolean value to indicate the given service was required</param>
        /// <returns>The service or null if not defined</returns>
        public static TSvc Resolve<TSvc>(bool required)
        {
            IServiceProvider provider = Services.BuildServiceProvider();

            if (required)
                return provider.GetRequiredService<TSvc>();
            else
                return provider.GetService<TSvc>();
        }
    }
}
