// <copyright file="DependencyResolverAdapter.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Bootstrap.DependencyResolver
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// The Dependency resolver adapter
    /// </summary>
    public class DependencyResolverAdapter : IDependencyInjectionAdapter
    {
        /// <summary>
        /// The dependency resolver
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolverAdapter" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public DependencyResolverAdapter(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T">The type of service</typeparam>
        /// <returns>An instance of the service type</returns>
        public T Get<T>()
        {
            return this.dependencyResolver.GetService<T>();
        }

        /// <summary>
        /// Gets all implementations of the service.
        /// </summary>
        /// <typeparam name="T">The type of service to return </typeparam>
        /// <returns>The <see cref="IEnumerable{T}"/> of the requested service types</returns>
        public IEnumerable<T> GetAll<T>()
        {
            return this.dependencyResolver.GetServices<T>();
        }
    }
}
