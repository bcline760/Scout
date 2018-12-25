using Autofac;
namespace Scout.Core
{
    public interface IRegister
    {
        void Register(ContainerBuilder container);
    }
}
