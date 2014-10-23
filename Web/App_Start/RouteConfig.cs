using System.Web.Mvc;
using System.Web.Routing;

namespace MediaCommMvc.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("forum", "forum/{page}", defaults: MVC.Forum.Index().AddRouteValue("page", 1), constraints: new { page = @"\d+" });

            routes.MapRoute("topic", "forum/topic/{id}/{name}/{page}", MVC.Forum.Topic().AddRouteValue("page", 1));

            routes.MapRoute(
                name: "Default", 
                url: "{controller}/{action}/{id}", 
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = 1 });
        }
    }
}