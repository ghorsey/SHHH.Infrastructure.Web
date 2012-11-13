
namespace SHHH.Infrastructure.Web
{
    public interface IRoleProvider<T> where T : class
    {
        bool IsInRole(T identity, string role);
    }
}
