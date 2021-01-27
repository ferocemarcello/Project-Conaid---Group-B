using System.Net.Http;
using System.Web;
using System.Web.Http.WebHost;

namespace WebServer.Controllers.api
{
    public class CustomWebHostBufferPolicySelector : WebHostBufferPolicySelector
    {
        // Check incoming requests and modify their buffer policy
        public override bool UseBufferedInputStream(object hostContext)
        {
            var contextBase = hostContext as HttpContextBase;

            if (contextBase != null
                && contextBase.Request.ContentType != null
                && contextBase.Request.ContentType.Contains("multipart"))
            {
                // we are enabling streamed mode here
                return false;
            }

            // let the default behavior(buffered mode) to handle the scenario
            return base.UseBufferedInputStream(hostContext);
        }

        // You could also chnage the response behavior too...but for this example, we are not
        // going to do anything here...I overrode this method just to demonstrate the availability
        // of this method.
        public override bool UseBufferedOutputStream(HttpResponseMessage response)
        {
            return base.UseBufferedOutputStream(response);
        }
    }
}