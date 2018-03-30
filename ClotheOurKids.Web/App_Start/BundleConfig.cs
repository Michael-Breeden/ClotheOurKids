using System.Web.Optimization;

namespace ClotheOurKids.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/requiredJs").Include(
                        "~/Content/js/jquery-3.2.1.min.js",
                        "~/Content/js/popper.min.js",
                        "~/Content/js/jquery.waypoints.min.js",
                        "~/Content/js/jquery.validate.min.js",
                        "~/Content/js/jquery.validate.unobtrusive.min.js",
                        "~/Content/js/bootstrap.js",
                        "~/Content/js/mdb.js",
                        "~/Content/plugins/select2-4.0.3/js/select2.full.min.js",
                        "~/Content/plugins/mmenu/dist/jquery.mmenu.all.js",
                        "~/Content/plugins/mmenu/dist/wrappers/bootstrap/jquery.mmenu.bootstrap4.js",
                        "~/Content/js/jquery.animateNumber.min.js",
                        "~/Content/js/vfs_fonts.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/js/modernizr-3.5.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie9").Include(
                      "~/assets/plugins/respond.js",
                      "~/assets/plugins/html5shiv.js",
                      "~/assets/plugins/placeholder-IE-fixes.js"));

            bundles.Add(new StyleBundle("~/Content/css/bundles")
                .Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/mdb.min.css",
                "~/Content/css/hamburgers-min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/plugins/mMenu/dist/bundles")
                .Include(
                "~/Content/plugins/mMenu/dist/jquery.mmenu.all.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css/myBundles")
                .Include(
                "~/Content/css/kidsSkin.css",
                "~/Content/css/myHeader.css",
                "~/Content/css/style.css"
                ));
        }
    }
}
