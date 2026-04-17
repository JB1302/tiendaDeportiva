using Microsoft.AspNetCore.Cors;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PracticaProgramada.Api
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/id",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}
