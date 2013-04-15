// <copyright file="HtmlHelperExtensions.cs" company="SHHH Innovations LLC">
// Copyright © 2013 SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Html
{
    using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

    /// <summary>
    /// Html Helper extensions
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Stylesheets the specified helper.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="href">The href to the css or less file, minus the extension (.css).</param>
        /// <param name="suffix">The suffix append to the CSS URL before the .css extension.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>
        /// Returns a link tag with the HREF set to <c>{filename}.css</c> when the debugger is attached; otherwise <c>{filename}{suffix}.css</c>
        /// </returns>
        /// <example>
        /// Call to <c>@Html.Stylesheet(Url.Content("~/Content/Styles/site"), "-min")</c>
        /// Will result in the following HTML <c>&lt;link type="text/css" rel="stylesheet" href="/Content/Styles/site-min.css"/&gt;</c>
        /// when the debugger is not attached.  Otherwise, if the debugger is attached, it will result in the following HTML:
        /// <c>&lt;link type="text/css" rel="stylesheet" href="/Content/Styles/site.css"/&gt;</c>
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static HtmlString Stylesheet(this HtmlHelper helper, string href, string suffix = ".min", string extension = ".css")
        {
            if (href.ToUpperInvariant().EndsWith(".CSS"))
            {
                href = href.Substring(0, href.Length - 4);
                extension = ".css";
            }

            if (href.ToUpperInvariant().EndsWith(".LESS"))
            {
                href = href.Substring(0, href.Length - 5);
                extension = ".less";
            }

            var rel = "stylesheet";

            if (extension.ToUpperInvariant() == ".LESS")
            {
                rel = "stylesheet/less";
            }

            var builder = new TagBuilder("link");
            
            builder.MergeAttribute("type", "text/css");
            builder.MergeAttribute("rel", rel);

            if (Debugger.IsAttached)
            {
                href = string.Concat(href, extension);
            }
            else
            {
                href = string.Concat(href, suffix, extension);
            }

            builder.MergeAttribute("href", href);

            return new HtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Javascripts the specified helper.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="src">The SRC.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>Returns a script tag with the SRC set to <c>{filename}.js</c> when the debugger is attached; otherwise <c>{filename}{suffix}.js</c></returns>
        /// <example>
        /// Call to <c>@Html.Javascript(Url.Content("~/Content/scripts/awesome"), "-min")</c>
        /// Will result in the following HTML <c>&lt;script src="/Content/scripts/awesome-min.js"&gt;&lt;/script&gt;</c>
        /// when the debugger is not attached.  Otherwise, if the debugger is attached, it will result in the following HTML:
        ///   <c>&lt;script src="/Content/Styles/awesome.js"&gt;&lt;/script&gt;</c>
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static HtmlString Javascript(HtmlHelper helper, string src, string suffix = ".min")
        {
            if (src.ToUpperInvariant().EndsWith(".JS"))
            {
                src = src.Substring(0, src.Length - 3);
            }

            var builder = new TagBuilder("script");

            if (Debugger.IsAttached)
            {
                src = string.Concat(src, ".js");
            }
            else
            {
                src = string.Concat(src, suffix, ".js");
            }

            builder.MergeAttribute("src", src);

            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Jsons the specified helper.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="setSettings">The set settings.</param>
        /// <returns>
        ///   <see cref="System.Web.HtmlString" />
        /// </returns>
        public static HtmlString Json(this HtmlHelper helper, object value, Action<JsonSerializerSettings> setSettings = null)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            if (setSettings != null)
            {
                setSettings(settings);
            }

            return new HtmlString(JsonConvert.SerializeObject(value));
        }
    }
}
