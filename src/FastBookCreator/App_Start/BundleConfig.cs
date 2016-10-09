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
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/site.js",
                        "~/Scripts/dropzone/dropzone.js",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/bootstrap-dialog.min.js",
                        "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/gridmvc.js",
                     "~/Scripts/bootstrap-datepicker.js",
                     "~/Scripts/gridmvc.lang.fa.js",
                     "~/Scripts/ckeditor/ckeditor.js"));
 
 

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/toastr.min.css",
                      "~/Scripts/dropzone/basic.css",
                     "~/Scripts/dropzone/dropzone.css",
                     "~/Content/bootstrap-dialog.min.css"
                      ));

     

            bundles.Add(new StyleBundle("~/Content/Gridmvc").Include(
                     "~/Content/Gridmvc.css",
                     "~/Content/gridmvc.datepicker.min.css"));


            bundles.Add(new StyleBundle("~/Content/errors").Include(
                   "~/Content/errors.css"));

            bundles.Add(new ScriptBundle("~/bundles/clipboard", "https://cdn.jsdelivr.net/clipboard.js/1.5.10/clipboard.min.js").Include("~/Scripts/clipboard.min.js"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = !Debugger.IsAttached;
        }
    }
}
