// <copyright file="HtmlHelperExtensionsTestFixture.cs" company="SHHH Innovations LLC">
// Copyright © 2013 SHHH Innovations LLC
// </copyright>

namespace SHHH.Infractructure.Web.Tests.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using NUnit.Framework;
    using SHHH.Infrastructure.Web.Html;

    /// <summary>
    /// Html Helper Extensions Test Fixture
    /// </summary>
    [TestFixture]
    public class HtmlHelperExtensionsTestFixture
    {
        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mckHttpContext = new Mock<HttpContextBase>(MockBehavior.Strict);
            mckHttpContext.Setup(c => c.ApplicationInstance).Returns(new HttpApplication());
            HtmlHelperExtensions.HttpContextAccessor = () => mckHttpContext.Object;
        }

        /// <summary>
        /// Tests the stylesheet method.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        [Test]
        public void TestStylesheetMethod()
        {
            var resultWithoutExtension = ((HtmlHelper)null).Stylesheet("site").ToString();
            var resultWithCssExtension = ((HtmlHelper)null).Stylesheet("site.css").ToString();
            var resultWithLessExtension = ((HtmlHelper)null).Stylesheet("site.less").ToString();

                Assert.AreEqual("<link href=\"site.min.css?v=4.0.0.0\" rel=\"stylesheet\" type=\"text/css\" />", resultWithCssExtension);
                Assert.AreEqual("<link href=\"site.min.less?v=4.0.0.0\" rel=\"stylesheet/less\" type=\"text/css\" />", resultWithLessExtension);
                Assert.AreEqual("<link href=\"site.min.css?v=4.0.0.0\" rel=\"stylesheet\" type=\"text/css\" />", resultWithoutExtension);
        }

        /// <summary>
        /// Tests the javascript method.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        [Test]
        public void TestJavascriptMethod()
        {
            var resultsWithoutExtension = ((HtmlHelper)null).Javascript("awesome-script").ToString();
            var resultWithExtension = ((HtmlHelper)null).Javascript("awesome-script.js").ToString();

            const string Expectation = "<script src=\"awesome-script.min.js?v=4.0.0.0\"></script>";

            Assert.AreEqual(Expectation, resultsWithoutExtension);
            Assert.AreEqual(Expectation, resultWithExtension);
        }
    }
}
