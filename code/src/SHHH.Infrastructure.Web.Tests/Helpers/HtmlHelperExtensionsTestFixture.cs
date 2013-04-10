// <copyright file="HtmlHelperExtensionsTestFixture.cs" company="SHHH Innovations LLC">
// Copyright © 2013 SHHH Innovations LLC
// </copyright>

namespace SHHH.Infractructure.Web.Tests.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using NUnit.Framework;
    using SHHH.Infrastructure.Web.Html;

    /// <summary>
    /// Html Helper Extensions Test Fixture
    /// </summary>
    [TestFixture]
    public class HtmlHelperExtensionsTestFixture
    {
        /// <summary>
        /// Tests the stylesheet method.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        [Test]
        public void TestStylesheetMethod()
        {
            var resultWithoutExtension = HtmlHelperExtensions.Stylesheet((HtmlHelper)null, "site").ToString();
            var resultWithCssExtension = HtmlHelperExtensions.Stylesheet((HtmlHelper)null, "site.css").ToString();
            var resultWithLessExtension = HtmlHelperExtensions.Stylesheet((HtmlHelper)null, "site.less").ToString();

            if (!Debugger.IsAttached)
            {
                Assert.AreEqual("<link href=\"site.min.css\" rel=\"stylesheet\" type=\"text/css\" />", resultWithCssExtension);
                Assert.AreEqual("<link href=\"site.min.less\" rel=\"stylesheet/less\" type=\"text/css\" />", resultWithLessExtension);
                Assert.AreEqual("<link href=\"site.min.css\" rel=\"stylesheet\" type=\"text/css\" />", resultWithoutExtension);
                Console.WriteLine("Success while the debugger is NOT attached");
            }
            else
            {
                Assert.AreEqual("<link href=\"site.css\" rel=\"stylesheet\" type=\"text/css\" />", resultWithCssExtension);
                Assert.AreEqual("<link href=\"site.less\" rel=\"stylesheet/less\" type=\"text/css\" />", resultWithLessExtension);
                Assert.AreEqual("<link href=\"site.css\" rel=\"stylesheet\" type=\"text/css\" />", resultWithoutExtension);
                Console.WriteLine("Success whiel the debugger IS attached");
            }
        }

        /// <summary>
        /// Tests the javascript method.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        [Test]
        public void TestJavascriptMethod()
        {
            var resultsWithoutExtension = HtmlHelperExtensions.Javascript((HtmlHelper)null, "awesome-script").ToString();
            var resultWithExtension = HtmlHelperExtensions.Javascript((HtmlHelper)null, "awesome-script.js").ToString();

            var expectation = "<script src=\"awesome-script.min.js\"></script>";
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Debugger IS attached");
                expectation = "<script src=\"awesome-script.js\"></script>";
            }

            Assert.AreEqual(expectation, resultsWithoutExtension);
            Assert.AreEqual(expectation, resultWithExtension);
        }
    }
}
