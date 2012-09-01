using System;
using NUnit.Framework;
using SHHH.Infractructure.Mvc.Tests.Shims;
using SHHH.Infrastructure.Mvc;

namespace SHHH.Infractructure.Mvc.Tests
{
    [TestFixture]
    public class CustomPrincipal_TestFixture
    {
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
