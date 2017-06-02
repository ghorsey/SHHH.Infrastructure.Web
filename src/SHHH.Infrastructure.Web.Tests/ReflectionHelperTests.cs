// <copyright file="ReflectionHelperTests.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infractructure.Web.Tests
{
    using System.Web.Http;
    using NUnit.Framework;
    using SHHH.Infrastructure.Web.Testing;

    /// <summary>
    /// The test fixture for the reflection helper
    /// </summary>
    [TestFixture]
    public class ReflectionHelperTests
    {
        /// <summary>
        /// Others the name.
        /// </summary>
        /// <returns>An empty string</returns>
        [ActionName("MyName")]
        public string OtherName()
        {
            return string.Empty;
        }

        /// <summary>
        /// A non the attribute.
        /// </summary>
        /// <returns>An empty string</returns>
        public string NonAttribute()
        {
            return string.Empty;
        }

        /// <summary>
        /// Tests the <c>GetMethodName</c> method
        /// </summary>
        [Test]
        public void GetMethodName_Test()
        {
            var myName = ReflectionHelper.GetMethodName((ReflectionHelperTests rht) => rht.OtherName());
            var nonAttribute = ReflectionHelper.GetMethodName((ReflectionHelperTests rht) => rht.NonAttribute());

            Assert.AreEqual("NonAttribute", nonAttribute);
            Assert.AreEqual("MyName", myName);
        }
    }
}
