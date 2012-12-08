// <copyright file="IFormsAuthenticationAdapter.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.FormsAuthentication
{
    /// <summary>
    /// The interface that captures all supported authentication methods
    /// </summary>
    public interface IFormsAuthenticationAdapter
    {
        /// <summary>
        /// Gets the login URL.
        /// </summary>
        /// <value>
        /// The login URL.
        /// </value>
        string LoginUrl { get; }

        /// <summary>
        /// Signs the user out.
        /// </summary>
        void SignOut();

        /// <summary>
        /// Signs the user in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> create persistent cookie.</param>
        void SignIn(string username, bool createPersistentCookie);

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        /// <returns>The redirect URL</returns>
        string GetRedirectUrl(string username, bool createPersistentCookie);
    }
}
