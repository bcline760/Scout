using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Scout.Core
{
    public static class ContainerLoader
    {
        public static void LoadContainers(IContainer container)
        {
            string dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            dirPath = dirPath.Substring(6);
            string[] assemblies = Directory.GetFiles(dirPath, "Scout.*.dll");

            //Assembly assembly = Assembly.
            Type registerType = typeof(IRegister);
            Type containerType = null;

            IRegister register = null;

            foreach (string file in assemblies)
            {
                Assembly assembly = Assembly.LoadFile(file);

                //Find the class that implements IRegister
                foreach (var type in assembly.ExportedTypes)
                {
                    if (registerType.IsAssignableFrom(type))
                    {
                        containerType = type;
                        break;
                    }
                }

                //Assembly doesn't have any registering classes, exit
                if (containerType == null)
                    continue;

                register = Activator.CreateInstance(containerType) as IRegister;
                register.Register(container);

                containerType = null;
            }
        }
    }
}
