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
            /*STYLES*/

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/*.css",
                "~/Content/Assets/*.css"));

            bundles.Add(new StyleBundle("~/bundle/font-awesome").Include(
                   "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundle/froalaEditorCSS").Include(
                    "~/Scripts/froala-editor/css/froala_editor.pkgd.min.css",
                    "~/Scripts/froala-editor/css/froala_editor.min.css",

                    "~/Scripts/froala-editor/css/froala_style.min.css"));

            bundles.Add(new StyleBundle("~/Content/Dashbord").Include("~/Content/Dashbord.css"));

            /*SCRIPTS*/

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

            bundles.Add(new ScriptBundle("~/bundle/froalaEditorJS")
                .Include("~/Scripts/froala-editor/js/froala_editor.pkgd.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapJS")
                .Include("~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerylib")
               .Include("~/Scripts/jquery-{version}.js"));

            //bundles.IgnoreList.Ignore("*/Content/loadme.css", OptimizationMode.Always);

            BundleTable.EnableOptimizations = false;
        }
    }
}