using System.Web.Mvc;
using System.Web.Routing;

namespace mvcOrEf
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GetCommonController",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GetCommon", action = "checkingCodeLogin", id = UrlParameter.Optional }
            );
        }
    }
}
