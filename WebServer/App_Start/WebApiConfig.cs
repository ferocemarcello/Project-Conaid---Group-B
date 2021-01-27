using System.Web.Http;
using System.Web.Http.Hosting;
using WebServer.Controllers;
using WebServer.Controllers.api;

namespace WebServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // not really sure about this
            config.Services.Replace(typeof(IHostBufferPolicySelector), new CustomWebHostBufferPolicySelector());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}