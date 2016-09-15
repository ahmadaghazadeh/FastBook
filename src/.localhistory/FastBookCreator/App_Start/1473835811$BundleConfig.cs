using System.Diagnostics;
using System.Web;
using System.Web.Optimization;

namespace FastBookCreator
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-select.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Gridmvc").Include(
                     "~/Scripts/gridmvc.min.js",
                     "~/Scripts/bootstrap-datepicker.js",
                     "~/Scripts/gridmvc.lang.fa.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/bootstrap-select.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Gridmvc").Include(
                     "~/Content/Gridmvc.css",
                     "~/Content/gridmvc.datepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/Gridmvc").Include(
                    "~/Content/Gridmvc.css",
                    "~/Content/gridmvc.datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = !Debugger.IsAttached;
        }
    }
}
