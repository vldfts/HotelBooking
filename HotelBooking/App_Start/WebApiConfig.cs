using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace HotelBooking
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "MyRouteApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            //    );

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
                 );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}