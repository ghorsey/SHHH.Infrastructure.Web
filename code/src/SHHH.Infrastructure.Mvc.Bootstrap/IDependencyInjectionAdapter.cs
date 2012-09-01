using System.Collections.Generic;

namespace SHHH.Infrastructure.Mvc.Bootstrap
{
    public interface IDependencyInjectionAdapter
    {
        T Get<T>();

        IEnumerable<T> GetAll<T>();
    }
}
