using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace JustBlog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/*.css",
                "~/Content/Assets/*.css"));


            bundles.Add(new ScriptBundle("~/bundles/jq")
            .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/clientfeaturesscripts")
            .Include(
            //"~/Scripts/jquery.validate.js",
            //"~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/tagify.js",
            "~/Scripts/jQuery.tagify.js"
            //"~/Scripts/popper.js",
            //"~/Scripts/popper-utils.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapJS")
                .Include("~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerylib")
               .Include("~/Scripts/jquery-{version}.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}