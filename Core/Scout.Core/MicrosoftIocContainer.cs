using System;

using Microsoft.Extensions.DependencyInjection;

namespace Scout.Core
{
    public class MicrosoftIocContainer : IContainer
    {
        private readonly IServiceCollection _container;

        public MicrosoftIocContainer(IServiceCollection collection)
        {
            _container = collection;
        }

        /// <summary>
        /// Static instance of the unity container used to manage the container registrations.
        /// </summary>
        public IServiceCollection Services
        {
            get { return _container; }
        }

        public void RegisterType<TFrom, TTo>(DependencyLifetime lifetime) 
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

        public TSvc Resolve<TSvc>(bool required)
        {
            IServiceProvider provider = Services.BuildServiceProvider();

            if (required)
                return provider.GetRequiredService<TSvc>();
            else
                return provider.GetService<TSvc>();
        }
    }
}
