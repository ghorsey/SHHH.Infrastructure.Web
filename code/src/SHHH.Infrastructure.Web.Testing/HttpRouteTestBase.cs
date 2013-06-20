// <copyright file="HttpRouteTestBase.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Testing
{
    using System;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Web.Http;
    using NUnit.Framework;

    /// <summary>
    /// A base class for testing Web API routes
    /// </summary>
    public abstract class HttpRouteTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRouteTestBase" /> class.
        /// </summary>
        /// <param name="f">The function.</param>
        /// <exception cref="System.ArgumentNullException">f;Function to return routes and HTTP configuration cannot be null!</exception>
        protected HttpRouteTestBase(Func<HttpConfiguration> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("f", "Function to return routes and HTTP configuration cannot be null!");
            }

            var h = f();

            HttpConfiguration = h;
        }

        /// <summary>
        /// Gets or sets the HTTP configuration.
        /// </summary>
        /// <value>
        /// The HTTP configuration.
        /// </value>
        private HttpConfiguration HttpConfiguration { get; set; }

        /// <summary>
        /// Tests the HTTP route.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <typeparam name="TExpression">The result</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <param name="expression">The expression.</param>
        /// <exception cref="System.InvalidOperationException">HttpConfiguration cannot be null!</exception>
        /// <exception cref="System.Exception"></exception>
        protected void TestHttpRoute<T, TExpression>(
            HttpMethod method, string url, Type controllerType, Expression<Func<T, TExpression>> expression)
        {
            if (HttpConfiguration == null)
            {
                throw new InvalidOperationException("HttpConfiguration cannot be null!");
            }

            if (!url.StartsWith("http") && !url.StartsWith("/"))
            {
                url = "/" + url;
            }

            if (!url.StartsWith("http"))
            {
                url = "http://localhost" + url;
            }

            var request = new HttpRequestMessage(method, url);

            var tester = new RouteTester(this.HttpConfiguration, request);
            try
            {
                Assert.AreEqual(controllerType, tester.GetControllerType());
                Assert.AreEqual(ReflectionHelper.GetMethodName(expression), tester.GetActionName());
            }
            catch (HttpResponseException x)
            {
                throw new Exception(x.Response.ToString(), x);
            }
        }
    }
}