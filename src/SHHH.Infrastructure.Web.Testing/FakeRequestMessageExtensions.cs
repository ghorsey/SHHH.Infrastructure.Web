// <copyright file="FakeRequestMessageExtensions.cs" company="SHHH Innovations LLC">
// Copyright SHHH Innovations LLC
// </copyright>

namespace SHHH.Infrastructure.Web.Testing
{
    using System;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Extension method to help creating fake requests
    /// </summary>
    public static class FakeRequestMessageExtensions
    {
        /// <summary>
        /// The HTTP configuration property
        /// </summary>
        private const string HttpConfigurationProperty = "MS_HttpConfiguration";

        /// <summary>
        /// Sets the fake request.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="configuration">The configuration.</param>
        public static void SetFakeRequest(this ApiController controller, Func<HttpConfiguration> configuration = null)
        {
            var request = new HttpRequestMessage();
            if (configuration == null)
            {
                request.Properties[HttpConfigurationProperty] = new HttpConfiguration();
            }
            else
            {
                request.Properties[HttpConfigurationProperty] = configuration();
            }

            controller.Request = request;
        }

        /// <summary>
        /// Sets the fake request.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="method">The method.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="configuration">The configuration.</param>
        public static void SetFakeRequest(this ApiController controller, HttpMethod method, Uri requestUri, Func<HttpConfiguration> configuration = null)
        {
            var request = new HttpRequestMessage(method, requestUri);
            if (configuration == null)
            {
                request.Properties[HttpConfigurationProperty] = new HttpConfiguration();
            }
            else
            {
                request.Properties[HttpConfigurationProperty] = configuration();
            }

            controller.Request = request;
        }

        /// <summary>
        /// Sets the fake request.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="method">The method.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="configuration">The configuration.</param>
        public static void SetFakeRequest(this ApiController controller, HttpMethod method, string requestUri, Func<HttpConfiguration> configuration = null)
        {
            SetFakeRequest(controller, method, new Uri(requestUri), configuration);
        }
    }
}