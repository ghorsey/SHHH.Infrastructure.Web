
namespace SHHH.Infrastructure.Mvc
{
    public class BasicRoleProvider<T> : IRoleProvider<T> where T: class
    {
        public bool IsInRole(T identity, string role)
        {
            return true; // everyone is a winner!
        }
    }
}
