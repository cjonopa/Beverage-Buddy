using System.Web.Optimization;

namespace BeverageBuddy.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/dist/jquery.min.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Validation/dist/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Content/Bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new Bundle("~/Content/css").Include(
                      "~/Content/Bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/fontawesome-free/css/all.min.css",
                      "~/Content/site.css"));
        }
    }
}
