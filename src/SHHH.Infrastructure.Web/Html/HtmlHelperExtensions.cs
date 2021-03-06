﻿// <copyright file="HtmlHelperExtensions.cs" company="SHHH Innovations LLC">
// Copyright © 2013 SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Html
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Html Helper extensions
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// The HTTP context
        /// </summary>
        internal static Func<HttpContextBase> HttpContextAccessor = () => new HttpContextWrapper(HttpContext.Current);

        /// <summary>
        /// Stylesheets the specified helper.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="href">The href to the css or less file, minus the extension (.css).</param>
        /// <param name="suffix">The suffix append to the CSS URL before the .css extension.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="includeVersion">if set to <c>true</c> the version of the calling assembly is included as a query string parameter.</param>
        /// <returns>
        /// Returns a link tag with the HREF set to <c>{filename}.css</c> when the debugger is attached; otherwise <c>{filename}{suffix}.css</c>
        /// </returns>
        /// <example>
        /// Call to <c>@Html.Stylesheet(Url.Content("~/Content/Styles/site"), "-min")</c>
        /// Will result in the following HTML <c>&lt;link type="text/css" rel="stylesheet" href="/Content/Styles/site-min.css"/&gt;</c>
        /// when the debugger is not attached.  Otherwise, if the debugger is attached, it will result in the following HTML:
        ///   <c>&lt;link type="text/css" rel="stylesheet" href="/Content/Styles/site.css"/&gt;</c>
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static HtmlString Stylesheet(this HtmlHelper helper, string href, string suffix = ".min", string extension = ".css", bool includeVersion = true)
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

            href = Debugger.IsAttached ? string.Concat(href, extension) : string.Concat(href, suffix, extension);

            if (includeVersion)
            {
                href = AppendVersion(href);
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
        /// <param name="includeVersion">if set to <c>true</c> the version of the calling assembly is included as a query string parameter.</param>
        /// <returns>
        /// Returns a script tag with the SRC set to <c>{filename}.js</c> when the debugger is attached; otherwise <c>{filename}{suffix}.js</c>
        /// </returns>
        /// <example>
        /// Call to <c>@Html.Javascript(Url.Content("~/Content/scripts/awesome"), "-min")</c>
        /// Will result in the following HTML <c>&lt;script src="/Content/scripts/awesome-min.js"&gt;&lt;/script&gt;</c>
        /// when the debugger is not attached.  Otherwise, if the debugger is attached, it will result in the following HTML:
        ///   <c>&lt;script src="/Content/Styles/awesome.js"&gt;&lt;/script&gt;</c>
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static HtmlString Javascript(this HtmlHelper helper, string src, string suffix = ".min", bool includeVersion = true)
        {
            if (src.ToUpperInvariant().EndsWith(".JS"))
            {
                src = src.Substring(0, src.Length - 3);
            }

            var debuggingSrc = string.Concat(src, ".js");
            var releaseSrc = string.Concat(src, suffix, ".js");

            return JavascriptSwitch(helper, debuggingSrc, releaseSrc, includeVersion);
        }

        /// <summary>
        /// Produced a script tag which switches the SRC attribute depending on whether the debugger is attached.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="debugingSrc">The SRC to use when the debugger is attached.</param>
        /// <param name="releaseSrc">The SRC to use when the debugger is not attached.</param>
        /// <param name="includeVersion">if set to <c>true</c> the version of the calling assembly is included as a query string parameter.</param>
        /// <returns>
        /// An HTML script tag
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static HtmlString JavascriptSwitch(this HtmlHelper helper, string debugingSrc, string releaseSrc, bool includeVersion = true)
        {
            var builder = new TagBuilder("script");

            var src = Debugger.IsAttached ? debugingSrc : releaseSrc;

            if (includeVersion)
            {
                src = AppendVersion(src);
            }

            builder.MergeAttribute("src", src);

            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// Serializes the value as a JSON object
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="setSettings">The set settings.</param>
        /// <returns>
        /// The JSON representation of the value object
        /// </returns>
        public static HtmlString Json(this HtmlHelper helper, object value, Action<JsonSerializerSettings> setSettings = null)
        {
            JsonSerializerSettings settings;
            if (GlobalConfiguration.Configuration != null)
            {
                settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings ?? new JsonSerializerSettings();
            }
            else
            {
                settings = new JsonSerializerSettings();
            }

            if (setSettings != null)
            {
                setSettings(settings);
            }

            return new HtmlString(JsonConvert.SerializeObject(value, settings));
        }

        /// <summary>
        /// Appends the version.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The URI with the version of the calling assembly appended</returns>
        private static string AppendVersion(string uri)
        {
            var type = HtmlHelperExtensions.HttpContextAccessor().ApplicationInstance.GetType();

            while (type.BaseType != null && type.Namespace == "ASP")
            {
                type = type.BaseType;
            }

            var version = type.Assembly.GetName().Version.ToString();

            return string.Concat(uri, "?v=", version);
        }
    }
}