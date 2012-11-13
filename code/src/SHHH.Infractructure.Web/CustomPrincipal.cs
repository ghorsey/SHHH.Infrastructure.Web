using System;
using System.Security.Principal;

namespace SHHH.Infrastructure.Web
{
    public class CustomPrincipal<T> : IPrincipal where T: class
    {
        public CustomPrincipal(CustomIdentity<T> identity, IRoleProvider<T> roleProvider)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            if (roleProvider == null) throw new ArgumentNullException("roleProvider");
            this.Identity = identity;
            this.RoleProvider = roleProvider;
        }
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return RoleProvider.IsInRole((T)((CustomIdentity<T>)Identity).Reference, role);
        }

        public IRoleProvider<T> RoleProvider { get; private set; }
    }
}
