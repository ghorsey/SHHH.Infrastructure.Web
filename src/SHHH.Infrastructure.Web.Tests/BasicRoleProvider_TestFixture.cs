// <copyright file="BasicRoleProvider_TestFixture.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// The basic role provider test fixture
    /// </summary>
    [TestFixture]
    public class BasicRoleProvider_TestFixture
    {
        /// <summary>
        /// Determines whether [is in role].
        /// </summary>
        [Test]
        public void IsInRole()
        {
            var basic = new BasicRoleProvider<string>();

            Assert.IsTrue(basic.IsInRole("ghorsey", "role"));
            Assert.IsTrue(basic.IsInRole("any", "value"));
        }
    }
}
