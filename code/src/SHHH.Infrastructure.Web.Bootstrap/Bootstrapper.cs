using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SHHH.Infrastructure.Web.Bootstrap
{
    public class Bootstrapper
    {
        public IDependencyInjectionAdapter DependencyInjectionAdapter { get; private set; }

        public Bootstrapper(IDependencyInjectionAdapter adapter)
        {
            DependencyInjectionAdapter = adapter;
        }

        public void Run(IEnumerable<IBootstrapTask> tasks)
        {
            foreach (var task in tasks)
                task.Run(this);
        }

        public static IEnumerable<Type> GetBootstrapTasksFrom(string assemblyName)
        {
            var binPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");

            if (!assemblyName.EndsWith(".dll"))
                assemblyName += ".dll";

            var assembly = Assembly.LoadFrom(Path.Combine(binPath, assemblyName));

            return assembly.GetTypes().Where(t => typeof(IBootstrapTask).IsAssignableFrom(t));
        }
    }
}
