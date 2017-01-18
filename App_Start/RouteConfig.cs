using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GFR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // routes are checked, top to bottom so any custom routes go above default
            // example of route with multiple params
            //            routes.MapRoute(
            //              name: "multipleparams",
            //              url: "{controller}/{action}/{param1}/{param2}",
            //              defaults: new { controller = "Home", action = "Action2", param1 = UrlParameter.Optional, param2 = UrlParameter.Optional }
            //          );

        }
    }
}
