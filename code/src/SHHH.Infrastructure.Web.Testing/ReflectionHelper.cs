using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SHHH.Infrastructure.Web.Testing
{
    /// <summary>
    /// Code taken from blog: http://www.strathweb.com/2012/08/testing-routes-in-asp-net-web-api/
    /// </summary>
    public class ReflectionHelper
    {
        public static string GetMethodName<T, U>(Expression<Func<T, U>> expression)
        {
            var method = expression.Body as MethodCallExpression;
            if (method == null) throw new ArgumentException("Expression is wrong");

            var attr = method.Method.CustomAttributes.Where(x => typeof(ActionNameAttribute).IsAssignableFrom(x.AttributeType)).FirstOrDefault();
            
            if (attr != null && attr.ConstructorArguments.Count == 1)
                return (string)attr.ConstructorArguments[0].Value;
                

            return method.Method.Name;
        }
    }
}
