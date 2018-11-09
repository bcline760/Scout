using System;
using System.Collections.Generic;
using System.Text;

using Autofac;
namespace Scout.Core
{
    public interface IRegister
    {
        void Register(ContainerBuilder container);
    }
}
