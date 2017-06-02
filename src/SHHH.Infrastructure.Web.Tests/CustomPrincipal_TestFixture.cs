// <copyright file="CustomPrincipal_TestFixture.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Tests
{
    using System;
    using NUnit.Framework;
    using SHHH.Infrastructure.Web.Tests.Shims;

    /// <summary>
    /// The custom principal test fixture
    /// </summary>
    [TestFixture]
    public class CustomPrincipal_TestFixture
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void New_Test()
        {
            var user = new User();
            var identity = new CustomIdentity<User>(user, user.Name, true);
            var roleProvider = new BasicRoleProvider<User>();
            var principal = new CustomPrincipal<User>(identity, roleProvider);
            
            Assert.AreSame(identity, principal.Identity);
            Assert.AreSame(roleProvider, principal.RoleProvider);
            Assert.IsTrue(principal.IsInRole("any string"));

            Assert.Throws<ArgumentNullException>(() => new CustomPrincipal<User>(null, roleProvider));
            Assert.Throws<ArgumentNullException>(() => new CustomPrincipal<User>(identity, null));
        }
    }
}
