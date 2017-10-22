using System.Web.Mvc;
using System.Web.Routing;

namespace GameFeed.Web {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "ToggleFollow",
                url: "game/togglefollow",
                defaults: new { controller = "Game", action = "ToggleFollow" }
            );

            routes.MapRoute(
                name: "Detail",
                url: "game/{id}",
                defaults: new { controller = "Game", action = "Detail" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
