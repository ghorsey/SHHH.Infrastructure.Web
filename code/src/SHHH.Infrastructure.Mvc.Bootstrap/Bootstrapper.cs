using System.Collections.Generic;

namespace SHHH.Infrastructure.Mvc.Bootstrap
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
    }
}
