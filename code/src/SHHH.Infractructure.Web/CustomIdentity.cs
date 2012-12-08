// <copyright file="CustomIdentity.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web
{
    using System;
    using System.Security.Principal;

    /// <summary>
    /// A custom identity object
    /// </summary>
    /// <typeparam name="T">The type of the referenced identity</typeparam>
    public class CustomIdentity<T> : MarshalByRefObject, IIdentity where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomIdentity{T}" /> class.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="name">The name.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> the identity is authenticated.</param>
        /// <exception cref="System.ArgumentNullException">The references parameter cannot be null</exception>
        /// <exception cref="System.ArgumentException">Cannot be null or empty;name</exception>
        public CustomIdentity(T reference, string name, bool isAuthenticated)
        {
            if (reference == null)
            {
                throw new ArgumentNullException("references");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Cannot be null or empty", "name");
            }

            this.Reference = reference;
            this.Name = name;
            this.IsAuthenticated = isAuthenticated;
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>The type of authentication used to identify the user.</returns>
        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user has been authenticated.
        /// </summary>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public T Reference { get; private set; }
    }
}