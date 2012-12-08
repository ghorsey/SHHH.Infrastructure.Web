// <copyright file="Bootstrapper.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Bootstrap
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A bootstrapper object
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public class Bootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper" /> class.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        public Bootstrapper(IDependencyInjectionAdapter adapter)
        {
            this.DependencyInjectionAdapter = adapter;
        }

        /// <summary>
        /// Gets the dependency injection adapter.
        /// </summary>
        /// <value>
        /// The dependency injection adapter.
        /// </value>
        public IDependencyInjectionAdapter DependencyInjectionAdapter { get; private set; }

        /// <summary>
        /// Gets the bootstrap tasks from.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>An Enumerable list of Type objects</returns>
        public static IEnumerable<Type> GetBootstrapTasksFrom(string assemblyName)
        {
            var binPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");

            if (!assemblyName.EndsWith(".dll"))
            {
                assemblyName += ".dll";
            }

            var assembly = Assembly.LoadFrom(Path.Combine(binPath, assemblyName));

            return assembly.GetTypes().Where(t => typeof(IBootstrapTask).IsAssignableFrom(t));
        }

        /// <summary>
        /// Runs the specified tasks.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        public void Run(IEnumerable<IBootstrapTask> tasks)
        {
            foreach (var task in tasks)
            {
                task.Run(this);
            }
        }
    }
}
