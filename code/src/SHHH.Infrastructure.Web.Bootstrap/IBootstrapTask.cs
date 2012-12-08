// <copyright file="IBootstrapTask.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Bootstrap
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A boostrap task
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public interface IBootstrapTask
    {
        /// <summary>
        /// Runs the specified bootstrapper.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        void Run(Bootstrapper bootstrapper);
    }
}
