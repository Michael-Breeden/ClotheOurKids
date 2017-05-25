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
                        "~/Content/js/bootstrap.js",
                        "~/Content/js/mdb.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie9").Include(
                      "~/assets/plugins/respond.js",
                      "~/assets/plugins/html5shiv.js",
                      "~/assets/plugins/placeholder-IE-fixes.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/plugins/font-awesome/css/font-awesome.css",
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/mdb.css",
                      "~/Content/css/mySkin.css",
                      "~/Content/css/style.css"
                      ));
        }
    }
}
