using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SearchFabric.API
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "docs/{id}",
                    defaults: new { controller = "Default", id = RouteParameter.Optional }
                );
            routes.MapHttpRoute(
                    name: "Default2Api",
                    routeTemplate: "{controller}/{id}",
                    defaults: new { controller = "Default", id = RouteParameter.Optional }
                );

        }
    }
}
