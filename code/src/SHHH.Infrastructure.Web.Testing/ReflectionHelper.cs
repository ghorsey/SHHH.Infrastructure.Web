// <copyright file="ReflectionHelper.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Testing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;

    /// <summary>
    /// Code taken from blog: http://www.strathweb.com/2012/08/testing-routes-in-asp-net-web-api/
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public class ReflectionHelper
    {
        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <typeparam name="U">The expression</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The method name</returns>
        /// <exception cref="System.ArgumentException">Expression is wrong</exception>
        public static string GetMethodName<T, U>(Expression<Func<T, U>> expression)
        {
            var method = expression.Body as MethodCallExpression;
            if (method == null)
            {
                throw new ArgumentException("Expression is wrong");
            }

            var attr = method.Method.CustomAttributes.FirstOrDefault(x => typeof(ActionNameAttribute).IsAssignableFrom(x.AttributeType));

            if (attr != null && attr.ConstructorArguments.Count == 1)
            {
                return (string)attr.ConstructorArguments[0].Value;
            }

            return method.Method.Name;
        }
    }
}
