using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;
using SHHH.Infrastructure.Web.Testing;

namespace SHHH.Infractructure.Web.Tests
{
    [TestFixture]
    public class ReflectionHelperTests
    {
        [ActionName("MyName")]
        public string OtherName() { return ""; }

        public string NonAttribute() { return ""; }

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
