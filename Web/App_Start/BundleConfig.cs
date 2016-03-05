using System.Web.Optimization;

namespace MediaCommMvc.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/js").Include(
                    "~/Scripts/bootstrap3-wysihtml5.all.min.js",
                    "~/Scripts/bootstrap3-wysihtml5.de-DE.js",
                    "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/upload").Include(
                    "~/Scripts/tmpl.js",
                    "~/Scripts/load-image.all.min.js",
                    "~/Scripts/canvas-to-blob.js",
                    "~/Scripts/jquery.ui.widget.js",
                    "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload-image.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload-audio.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload-video.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                    "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js",
                    "~/Scripts/bootstrap3-typeahead.min.js"

                    ));

            bundles.Add(
                new ScriptBundle("~/bundles/images")
                    .Include(
                        "~/Scripts/jquery.lazyload.min.js",
                        "~/Scripts/photoswipe.min.js",
                        "~/Scripts/photoswipe-ui-default.min.js"
                    ));

            bundles.Add(
                new StyleBundle("~/Content/css/images")
                    .Include("~/Content/photoswipe/photoswipe.css", new CssRewriteUrlTransform())
                    .Include("~/Content/photoswipe/default-skin/default-skin.css", new CssRewriteUrlTransform())
                );

            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap3-wysihtml5.min.css",
                    "~/Content/bootstrap.cerulean.css",
                    "~/Content/bootstrap-multiselect.css",
                    "~/Content/site.css"));


            bundles.Add(
                new StyleBundle("~/Content/css/upload")
                    .Include("~/Content/jQuery.FileUpload/css/*.css", new CssRewriteUrlTransform()));

            #if DEBUG
                        BundleTable.EnableOptimizations = false;
            #else
                BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}
