// <copyright file="BasicRoleProvider.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web
{
    /// <summary>
    /// The basic role provider
    /// </summary>
    /// <typeparam name="T">The type of Identity the basic role provider works with</typeparam>
    public class BasicRoleProvider<T> : IRoleProvider<T> where T : class
    {
        /// <summary>
        /// Determines whether the identity is in the specified role.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if identity is in the specified role; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRole(T identity, string role)
        {
            return true; // everyone is a winner!
        }
    }
}
