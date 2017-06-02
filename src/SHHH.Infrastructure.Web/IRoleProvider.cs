// <copyright file="IRoleProvider.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web
{
    /// <summary>
    /// A common interface for determining if an Identity is in a specific role
    /// </summary>
    /// <typeparam name="T">The class the role provider works with</typeparam>
    public interface IRoleProvider<in T> where T : class
    {
        /// <summary>
        /// Determines whether the identity is in the specified role.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if the identity is in the specified role; otherwise, <c>false</c>.
        /// </returns>
        bool IsInRole(T identity, string role);
    }
}
