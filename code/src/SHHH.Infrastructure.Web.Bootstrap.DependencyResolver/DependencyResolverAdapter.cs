using System.Collections.Generic;
using System.Web.Mvc;

namespace SHHH.Infrastructure.Web.Bootstrap.DependencyResolver
{
    public class DependencyResolverAdapter : IDependencyInjectionAdapter
    {
        readonly IDependencyResolver DependencyResolver;

        public DependencyResolverAdapter(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
        }

        public T Get<T>()
        {
            return DependencyResolver.GetService<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return DependencyResolver.GetServices<T>();
        }
    }
}
