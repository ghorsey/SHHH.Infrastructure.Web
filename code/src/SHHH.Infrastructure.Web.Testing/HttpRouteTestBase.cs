// <copyright file="HttpRouteTestBase.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Testing
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Routing;
    using Moq;
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
        /// <exception cref="System.ArgumentNullException">Function to return routes and HTTP configuration cannot be null!</exception>
        protected HttpRouteTestBase(Func<HttpConfiguration> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("Function to return routes and HTTP configuration cannot be null!");
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
        /// <typeparam name="U">The result</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <param name="expression">The expression.</param>
        /// <exception cref="System.InvalidOperationException">HttpConfiguration cannot be null!</exception>
        /// <exception cref="System.Exception"></exception>
        protected void TestHttpRoute<T, U>(HttpMethod method, string url, Type controllerType, Expression<Func<T, U>> expression)
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

        #region Private Methods
        /// <summary>
        /// Creates the mock context.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The Mock of the HTTP Context</returns>
        private Mock<HttpContextBase> CreateMockContext(string url)
        {
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();

            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);

            mockContext.Setup(x => x.Response).Returns(mockResponse.Object);
            mockContext.Setup(x => x.Request).Returns(mockRequest.Object);

            return mockContext;
        }

        /// <summary>
        /// Creates the request context.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The request context</returns>
        private RequestContext CreateRequestContext(string url)
        {
            return new RequestContext(this.CreateMockContext(url).Object, new RouteData());
        }
        #endregion
    }
}
