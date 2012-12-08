// <copyright file="CustomPrincipal.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web
{
    using System;
    using System.Security.Principal;

    /// <summary>
    /// The common custom principal object
    /// </summary>
    /// <typeparam name="T">The type of the identity referenced by this principal implementation</typeparam>
    public class CustomPrincipal<T> : IPrincipal where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPrincipal{T}" /> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="roleProvider">The role provider.</param>
        /// <exception cref="System.ArgumentNullException">The identity or roleProvider arguments cannot be null</exception>
        public CustomPrincipal(CustomIdentity<T> identity, IRoleProvider<T> roleProvider)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            if (roleProvider == null)
            {
                throw new ArgumentNullException("roleProvider");
            }
            
            this.Identity = identity;
            this.RoleProvider = roleProvider;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.</returns>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Gets the role provider.
        /// </summary>
        /// <value>
        /// The role provider.
        /// </value>
        public IRoleProvider<T> RoleProvider { get; private set; }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role)
        {
            return this.RoleProvider.IsInRole((T)((CustomIdentity<T>)this.Identity).Reference, role);
        }
    }
}
