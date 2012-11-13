using System;
using NUnit.Framework;
using SHHH.Infrastructure.Web.Tests.Shims;
using SHHH.Infrastructure.Web;

namespace SHHH.Infrastructure.Web.Tests
{
    [TestFixture]
    public class CustomIdentity_TestFixture
    {
        
        [Test]
        public void New_Test()
        {
            var user = new User();
            var customIdentity = new CustomIdentity<User>(user, user.Name, true);


            Assert.AreSame(user, customIdentity.Reference);
            Assert.AreEqual("Custom", customIdentity.AuthenticationType);
            Assert.AreEqual(user.Name, customIdentity.Name);

            Assert.IsTrue(customIdentity.IsAuthenticated);

            Assert.Throws<ArgumentNullException>(() => new CustomIdentity<User>(null, "bob", true));
            Assert.Throws<ArgumentException>(() => new CustomIdentity<User>(user, "   ", true));
        }
    }
}
