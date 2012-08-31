
namespace SHHH.Infrastructure.Mvc
{
    public interface IRoleProvider<T> where T : class
    {
        bool IsInRole(T identity, string role);
    }
}
