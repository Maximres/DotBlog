using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Routing.Constraints;

namespace JustBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapMvcAttributeRoutes();

            //AreaRegistration.RegisterAllAreas();



            routes.MapRoute(
                name: "Category",
                url: "GetCategory/{category}",
                defaults: new { controller = "Blog", action = "Category" },
                constraints: new { category = new LengthRouteConstraint(1, 50) }
                );

            routes.MapRoute(
                name: "Tag",
                url: "GetTag/{tag}",
                defaults: new { controller = "Blog", action = "Tag" },
                constraints: new { tag = new LengthRouteConstraint(1, 50) }
                );

            routes.MapRoute(
                "Post",
                "Archive/{year}/{month}/{title}",
                new { controller = "Blog", action = "Post" },
                new { year = @"\d{4}", month = @"\d{1,2}", title = new LengthRouteConstraint(1,200) }
            );



            //routes.MapRoute(
            //   "Admin",
            //   "Admin/Panel/{action}/{id}",
            //   new
            //   {
            //       controller = "Admin",
            //       action = "Index",
            //       id = UrlParameter.Optional
            //   }
            //   );

            //routes.MapRoute(
            //    "AdminPage",
            //    "Admin/Panel/{action}/{page}",
            //    new
            //    {
            //        controller = "Admin",
            //        action = "Index"
            //    },
            //    new { page = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Blog", action = "Posts", page = UrlParameter.Optional },
                constraints: null
                );

           
        }
    }
}
