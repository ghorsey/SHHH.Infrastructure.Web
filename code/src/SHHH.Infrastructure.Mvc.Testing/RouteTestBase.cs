using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace SHHH.Infrastructure.Mvc.Testing
{
    public abstract class RouteTestBase
    {
        protected RouteCollection Routes { get; private set; }
        protected HttpConfiguration HttpConfiguration { get; private set; }

        protected RouteTestBase(Func<RouteCollection> f)
        {
            if (f == null) throw new ArgumentNullException("Function to return routes cannot be null!");

            Routes = f();
        }

        protected RouteTestBase(Func<Tuple<RouteCollection, HttpConfiguration>> f)
        {
            if (f == null) throw new ArgumentNullException("Function to return routes and HTTP configuration cannot be null!");

            var t = f();
            Routes = t.Item1;
            HttpConfiguration = t.Item2;
        }


        protected RouteData TestRouteFromUrl(string url, string expectedController, string expectedAction)
        {
            if (url.StartsWith("/"))
                url = "~" + url;
            if (!url.StartsWith("~/"))
                url = "~/" + url;

            RouteData rd = GenerateRouteDataFromUrl(url);

            CommonRouteDataAsserts(rd, expectedController, expectedAction);

            return rd;
        }

        private void CommonRouteDataAsserts(RouteData actual, string expectedController, string expectedAction)
        {
            Assert.AreEqual(expectedController, actual.Values["controller"]);
            Assert.AreEqual(expectedAction, actual.Values["action"]);
        }

        protected void TestRouteFromRouteData(object routeData, string expectedUrl)
        {
            VirtualPathData vpd = GenerateUrlFromRouteData(routeData);
            Assert.AreEqual(expectedUrl, vpd.VirtualPath);
        }

        private RouteData GenerateRouteDataFromUrl(string url)
        {
            var mockContext = CreateMockContext(url);

            RouteData routeData = Routes.GetRouteData(mockContext.Object);

            Assert.IsNotNull(routeData, "Null RouteData was returned");
            Assert.IsNotNull(routeData.Route, "No Route was matched");

            return routeData;
        }

        private VirtualPathData GenerateUrlFromRouteData(object values)
        {   
            var mockContext = CreateMockContext(null);
            RequestContext requestContext = CreateRequestContext(null);
            VirtualPathData vpd = Routes.GetVirtualPath(requestContext, new RouteValueDictionary(values));

            StackTrace st = new StackTrace();

            Console.WriteLine("{0}: {1}", st.GetFrame(1).GetMethod().Name, vpd.VirtualPath);

            return vpd;
        }

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

        private RequestContext CreateRequestContext(string url)
        {
            return new RequestContext(CreateMockContext(url).Object, new RouteData());
        }
    }
}
