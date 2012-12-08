// <copyright file="IDependencyInjectionAdapter.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Bootstrap
{
    using System.Collections.Generic;

    /// <summary>
    /// The dependency injection resolver
    /// </summary>
    public interface IDependencyInjectionAdapter
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>An instance of the type</returns>
        T Get<T>();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of the type</returns>
        IEnumerable<T> GetAll<T>();
    }
}
