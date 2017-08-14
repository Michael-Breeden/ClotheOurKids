using System.Web.Optimization;

namespace ClotheOurKids.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/requiredJs").Include(
                        "~/Content/js/jquery-3.1.1.js",
                        "~/Content/js/tether.js",
                        "~/Content/js/jquery.waypoints.min.js",
                        "~/Content/js/jquery.validate.min.js",
                        "~/Content/js/jquery.validate.unobtrusive.min.js",
                        "~/Content/js/bootstrap.js",
                        "~/Content/js/mdb.js",
                        "~/Content/plugins/select2-4.0.3/js/select2.full.min.js",
                        "~/Content/plugins/mmenu-6.1/dist/jquery.mmenu.all.js",
                        "~/Content/plugins/mmenu-6.1/dist/wrappers/bootstrap/jquery.mmenu.bootstrap.js",
                        "~/Content/js/jquery.animateNumber.min.js"
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

            bundles.Add(new StyleBundle("~/Content/plugins/mMenu-6.1/dist/bundles")
                .Include(
                "~/Content/plugins/mMenu-6.1/dist/jquery.mmenu.all.css",
                "~/Content/plugins/mMenu-6.1/dist/wrappers/bootstrap/jquery.mmenu.bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css/myBundles")
                .Include(
                "~/Content/css/mySkin.css",
                "~/Content/css/myHeader.css",
                "~/Content/css/style.css"
                ));
        }
    }
}
