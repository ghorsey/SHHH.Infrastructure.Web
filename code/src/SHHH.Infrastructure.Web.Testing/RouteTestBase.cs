// <copyright file="RouteTestBase.cs" company="SHHH Innovations LLC">
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
    /// A base class for testing web API routes
    /// </summary>
    public abstract class RouteTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTestBase" /> class.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <exception cref="System.ArgumentNullException">Function to return routes cannot be null!</exception>
        protected RouteTestBase(Func<RouteCollection> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("Function to return routes cannot be null!");
            }

            this.Routes = f();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTestBase" /> class.
        /// </summary>
        /// <param name="f">The function.</param>
        /// <exception cref="System.ArgumentNullException">Function to return routes and HTTP configuration cannot be null!</exception>
        protected RouteTestBase(Func<Tuple<RouteCollection, HttpConfiguration>> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("Function to return routes and HTTP configuration cannot be null!");
            }

            var t = f();
            this.Routes = t.Item1;
            HttpConfiguration = t.Item2;
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        protected RouteCollection Routes { get; private set; }

        /// <summary>
        /// Gets the HTTP configuration.
        /// </summary>
        /// <value>
        /// The HTTP configuration.
        /// </value>
        protected HttpConfiguration HttpConfiguration { get; private set; }

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
                url = "http://localhost/" + url;
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

        /// <summary>
        /// Tests the route from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="expectedController">The expected controller.</param>
        /// <param name="expectedAction">The expected action.</param>
        /// <returns><see cref="RouteData"/></returns>
        protected RouteData TestRouteFromUrl(string url, string expectedController, string expectedAction)
        {
            if (url.StartsWith("/"))
            {
                url = "~" + url;
            }

            if (!url.StartsWith("~/"))
            {
                url = "~/" + url;
            }

            RouteData rd = this.GenerateRouteDataFromUrl(url);

            this.CommonRouteDataAsserts(rd, expectedController, expectedAction);

            return rd;
        }

        /// <summary>
        /// Tests the route from route data.
        /// </summary>
        /// <param name="routeData">The route data.</param>
        /// <param name="expectedUrl">The expected URL.</param>
        protected void TestRouteFromRouteData(object routeData, string expectedUrl)
        {
            VirtualPathData vpd = this.GenerateUrlFromRouteData(routeData);
            Assert.AreEqual(expectedUrl, vpd.VirtualPath);
        }

        /// <summary>
        /// Commons the route data asserts.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="expectedController">The expected controller.</param>
        /// <param name="expectedAction">The expected action.</param>
        private void CommonRouteDataAsserts(RouteData actual, string expectedController, string expectedAction)
        {
            Assert.AreEqual(expectedController, actual.Values["controller"]);
            Assert.AreEqual(expectedAction, actual.Values["action"]);
        }

        /// <summary>
        /// Generates the route data from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The route data</returns>
        private RouteData GenerateRouteDataFromUrl(string url)
        {
            var mockContext = this.CreateMockContext(url);

            RouteData routeData = this.Routes.GetRouteData(mockContext.Object);

            Assert.IsNotNull(routeData, "Null RouteData was returned");
            Assert.IsNotNull(routeData.Route, "No Route was matched");

            return routeData;
        }

        /// <summary>
        /// Generates the URL from route data.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The virtual path data</returns>
        private VirtualPathData GenerateUrlFromRouteData(object values)
        {   
            var mockContext = this.CreateMockContext(null);
            RequestContext requestContext = this.CreateRequestContext(null);
            VirtualPathData vpd = this.Routes.GetVirtualPath(requestContext, new RouteValueDictionary(values));

            StackTrace st = new StackTrace();

            Console.WriteLine("{0}: {1}", st.GetFrame(1).GetMethod().Name, vpd.VirtualPath);

            return vpd;
        }

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
    }
}
