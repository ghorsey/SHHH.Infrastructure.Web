using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SHHH.Infrastructure.Mvc.Testing
{
    public static class FakeRequestMessageExtensions
    {
        const string HttpConfigurationProperty = "MS_HttpConfiguration";
        public static void SetFakeRequest(this ApiController controller, Func<HttpConfiguration> configuration = null)
        {
            var request = new HttpRequestMessage();
            if (configuration == null)
                request.Properties[HttpConfigurationProperty] = new HttpConfiguration();
            else
                request.Properties[HttpConfigurationProperty] = configuration();

            controller.Request = request;
        }

        public static void SetFakeRequest(this ApiController controller, HttpMethod method, Uri requestUri, Func<HttpConfiguration> configuration = null)
        {
            var request = new HttpRequestMessage(method, requestUri);
            if (configuration == null)
                request.Properties[HttpConfigurationProperty] = new HttpConfiguration();
            else
                request.Properties[HttpConfigurationProperty] = configuration();

            controller.Request = request;
        }
        public static void SetFakeRequest(this ApiController controller, HttpMethod method, string requestUri, Func<HttpConfiguration> configuration = null)
        {
            SetFakeRequest(controller, method, new Uri(requestUri), configuration);
        }
    }
}