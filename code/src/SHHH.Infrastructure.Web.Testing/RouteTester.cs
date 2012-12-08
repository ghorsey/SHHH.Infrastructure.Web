// <copyright file="RouteTester.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Testing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;

    /// <summary>
    /// Code taken from: http://www.strathweb.com/2012/08/testing-routes-in-asp-net-web-api/
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public class RouteTester
    {
        /// <summary>
        /// The config
        /// </summary>
        private HttpConfiguration config;

        /// <summary>
        /// The request
        /// </summary>
        private HttpRequestMessage request;

        /// <summary>
        /// The route data
        /// </summary>
        private IHttpRouteData routeData;

        /// <summary>
        /// The controller selector
        /// </summary>
        private IHttpControllerSelector controllerSelector;

        /// <summary>
        /// The controller context
        /// </summary>
        private HttpControllerContext controllerContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTester" /> class.
        /// </summary>
        /// <param name="config">The HTTP configuration.</param>
        /// <param name="request">The request message.</param>
        public RouteTester(HttpConfiguration config, HttpRequestMessage request)
        {
            this.config = config;
            this.request = request;
            this.routeData = this.config.Routes.GetRouteData(this.request);
            this.request.Properties[HttpPropertyKeys.HttpRouteDataKey] = this.routeData;
            this.controllerSelector = new DefaultHttpControllerSelector(this.config);
            this.controllerContext = new HttpControllerContext(this.config, this.routeData, this.request);
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <returns>The action name</returns>
        public string GetActionName()
        {
            if (this.controllerContext.ControllerDescriptor == null)
            {
                this.GetControllerType();
            }

            var actionSelector = new ApiControllerActionSelector();
            var descriptor = actionSelector.SelectAction(this.controllerContext);
            return descriptor.ActionName;
        }

        /// <summary>
        /// Gets the type of the controller.
        /// </summary>
        /// <returns>A <see cref="System.Type"/></returns>
        public Type GetControllerType()
        {
            var descriptor = this.controllerSelector.SelectController(this.request);
            this.controllerContext.ControllerDescriptor = descriptor;
            return descriptor.ControllerType;
        }
    }
}
