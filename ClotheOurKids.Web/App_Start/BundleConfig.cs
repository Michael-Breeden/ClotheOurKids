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

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/plugins/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/mdb.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/plugins/mMenu-6.1/dist/jquery.mmenu.all.css", new CssRewriteUrlTransform())
                .Include("~/Content/plugins/mMenu-6.1/dist/wrappers/bootstrap/jquery.mmenu.bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/hamburgers-min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/mySkin.css", new CssRewriteUrlTransform())
                .Include("~/Content/plugins/select2-4.0.3/css/select2.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/myHeader.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/style.css", new CssRewriteUrlTransform())
            );
        }
    }
}
