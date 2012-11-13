using NUnit.Framework;
using SHHH.Infrastructure.Web;

namespace SHHH.Infrastructure.Web.Tests
{
    [TestFixture]
    public class BasicRoleProvider_TestFixture
    {
        [Test]
        public void IsInRole()
        {

            var basic = new BasicRoleProvider<string>();

            Assert.IsTrue(basic.IsInRole("ghorsey", "role"));
            Assert.IsTrue(basic.IsInRole("any","value"));
        }
    }
}
