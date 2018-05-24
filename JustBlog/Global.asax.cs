using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JustBlog.Infrastructure;
using JustBlog;
using System.Web.Optimization;
using System.Data.Entity;
using JustBlog.Models;

namespace JustBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<ApplicationContext>(new AppDbInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
