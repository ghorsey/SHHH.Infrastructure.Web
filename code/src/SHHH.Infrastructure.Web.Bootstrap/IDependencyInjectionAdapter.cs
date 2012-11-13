using System.Collections.Generic;

namespace SHHH.Infrastructure.Web.Bootstrap
{
    public interface IDependencyInjectionAdapter
    {
        T Get<T>();

        IEnumerable<T> GetAll<T>();
    }
}
