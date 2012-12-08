// <copyright file="BasicFormsAuthentication.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.FormsAuthentication
{
    /// <summary>
    /// The default forms authentication class
    /// </summary>
    public class BasicFormsAuthentication : IFormsAuthenticationAdapter
    {
        /// <summary>
        /// Gets the login URL.
        /// </summary>
        /// <value>
        /// The login URL.
        /// </value>
        public string LoginUrl
        {
            get { return System.Web.Security.FormsAuthentication.LoginUrl; }
        }

        /// <summary>
        /// Signs the user out.
        /// </summary>
        public void SignOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Signs the user in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> create persistent cookie.</param>
        public void SignIn(string username, bool createPersistentCookie)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        /// <returns>
        /// The redirect URL
        /// </returns>
        public string GetRedirectUrl(string username, bool createPersistentCookie)
        {
            return System.Web.Security.FormsAuthentication.GetRedirectUrl(username, createPersistentCookie);
        }
    }
}
