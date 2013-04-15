﻿/*
 * Code based on: http://blogs.msdn.com/b/yaohuang1/archive/2012/05/21/asp-net-web-api-generating-a-web-api-help-page-using-apiexplorer.aspx
 * Original author: Yao
 */
namespace SHHH.Infrastructure.Web.Http.Documentation
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;
    using System.Xml.XPath;

    /// <summary>
    /// Xml Comment Document Provider
    /// </summary>
    public class XmlCommentDocumentationProvider : IDocumentationProvider
    {
        /// <summary>
        /// The method expression
        /// </summary>
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";

        /// <summary>
        /// The nullable type name regex
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        private static Regex nullableTypeNameRegex = new Regex(@"(.*\.Nullable)" + Regex.Escape("`1[[") + "([^,]*),.*");

        /// <summary>
        /// The generic type name regex
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        private static Regex GenericTypeNameRegex = new Regex(@"^(.*?)`(\d+)\[(?:(?:,\s)?\[(.*?),[^\]]*\]){1,}\]", RegexOptions.IgnoreCase);

        /// <summary>
        /// The document navigator
        /// </summary>
        private XPathNavigator documentNavigator;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCommentDocumentationProvider"/> class.
        /// </summary>
        /// <param name="documentPath">The document path.</param>
        public XmlCommentDocumentationProvider(string documentPath)
        {
            XPathDocument xpath = new XPathDocument(documentPath);
            this.documentNavigator = xpath.CreateNavigator();
        }

        /// <summary>
        /// Gets the documentation based on <see cref="T:System.Web.Http.Controllers.HttpParameterDescriptor" />.
        /// </summary>
        /// <param name="parameterDescriptor">The parameter descriptor.</param>
        /// <returns>
        /// The documentation for the controller.
        /// </returns>
        public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            ReflectedHttpParameterDescriptor reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
            if (reflectedParameterDescriptor != null)
            {
                XPathNavigator memberNode = this.GetMemberNode(reflectedParameterDescriptor.ActionDescriptor);
                if (memberNode != null)
                {
                    string parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
                    XPathNavigator parameterNode = memberNode.SelectSingleNode(string.Format("param[@name='{0}']", parameterName));
                    if (parameterNode != null)
                    {
                        return parameterNode.Value.Trim();
                    }
                }
            }

            return "No Documentation Found.";
        }

        /// <summary>
        /// Gets the documentation based on <see cref="T:System.Web.Http.Controllers.HttpActionDescriptor" />.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>
        /// The documentation for the controller.
        /// </returns>
        public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            XPathNavigator memberNode = this.GetMemberNode(actionDescriptor);
            if (memberNode != null)
            {
                XPathNavigator summaryNode = memberNode.SelectSingleNode("summary");
                if (summaryNode != null)
                {
                    return summaryNode.Value.Trim();
                }
            }

            return "No Documentation Found.";
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The member name</returns>
        private static string GetMemberName(MethodInfo method)
        {
            string name = string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
            var parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                string[] parameterTypeNames = parameters.Select(param => ProcessTypeName(param.ParameterType.FullName)).ToArray();
                name += string.Format("({0})", string.Join(",", parameterTypeNames));
            }

            return name;
        }

        /// <summary>
        /// Processes the name of the type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The process type name</returns>
        private static string ProcessTypeName(string typeName)
        {
            // handle nullable
            var result = nullableTypeNameRegex.Match(typeName);
            if (result.Success)
            {
                return string.Format("{0}{{{1}}}", result.Groups[1].Value, result.Groups[2].Value);
            }

            result = GenericTypeNameRegex.Match(typeName);
            if (result.Success)
            {
                var count = Convert.ToInt32(result.Groups[2].Value);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}{{", result.Groups[1].Value);
                sb.Append(result.Groups[3].Value);
                if (count > 1)
                {
                    for (int i = 1; i <= count; i++)
                    {
                        sb.AppendFormat(", {0}", result.Groups[3 + i].Value);
                    }
                }
                sb.Append("}");

                return sb.ToString();
            }

            return typeName;
        }

        /// <summary>
        /// Gets the member node.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The <see cref="XPathNavigator"/></returns>
        private XPathNavigator GetMemberNode(HttpActionDescriptor actionDescriptor)
        {
            ReflectedHttpActionDescriptor reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (reflectedActionDescriptor != null)
            {
                string selectExpression = string.Format(MethodExpression, GetMemberName(reflectedActionDescriptor.MethodInfo));
                XPathNavigator node = this.documentNavigator.SelectSingleNode(selectExpression);
                if (node != null)
                {
                    return node;
                }
            }

            return null;
        }
    }
}