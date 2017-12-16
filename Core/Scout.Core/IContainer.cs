using Microsoft.Extensions.DependencyInjection;

namespace Scout.Core
{
    public interface IContainer
    {
        /// <summary>
        /// Static instance of the unity container used to manage the container registrations.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Registers an interface TFrom with a concrete type resolution TTo.
        /// </summary>
        /// <typeparam name="TFrom">Type of the object to resolve from</typeparam>
        /// <typeparam name="TTo">Type of the object to resolve to</typeparam>
        void RegisterType<TFrom, TTo>(DependencyLifetime lifetime)
            where TFrom : class
            where TTo : class, TFrom;

        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <typeparam name="TSvc">The type of service to resolve</typeparam>
        /// <param name="required">Boolean value to indicate the given service was required</param>
        /// <returns>The service or null if not defined</returns>
        TSvc Resolve<TSvc>(bool required);
    }
}