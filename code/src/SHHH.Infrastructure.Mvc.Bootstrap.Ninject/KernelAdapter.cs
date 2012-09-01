using System.Collections.Generic;
using Ninject;

namespace SHHH.Infrastructure.Mvc.Bootstrap.Ninject
{
    public class KernelAdapter : IDependencyInjectionAdapter
    {
        readonly IKernel Kernel;
        public KernelAdapter(IKernel kernel)
        {
            Kernel = kernel;
        }

        public T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return Kernel.GetAll<T>();
        }
    }
}
