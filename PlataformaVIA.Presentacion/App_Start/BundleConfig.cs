using System.Web;
using System.Web.Optimization;

namespace PlataformaVIA.Presentacion
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui-1.12.1.min.js",
                        "~/Scripts/notify.min.js",
                        "~/Scripts/accounting.min.js"
                       ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/moment.min.js",
                       "~/Scripts/bootstrap-switch.min.js",
                       "~/Scripts/helper.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                       "~/Content/font-awesome.min.css",
                        "~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.min.css",
                       "~/Content/themes/base/jquery-ui.min.css",
                       "~/Content/helper.css",
                       "~/Content/bootstrap-select.css",
                        "~/Content/summernote/summernote.css"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/DataTables/jquery.dataTables.min.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js",
                      "~/Scripts/DataTables/dataTables.buttons.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/bundledatatables").Include(
                      //"~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",                    
                      "~/Content/DataTables/css/buttons.dataTables.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jszip").Include(
                        "~/Scripts/bootstrap-select.min.js",
                       "~/Scripts/jszip.min.js"                                      
                       ));
            bundles.Add(new ScriptBundle("~/bundles/buttonshtml5").Include(
                       "~/Scripts/DataTables/buttons.html5.min.js",
                      "~/Scripts/DataTables/buttons.print.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/dataTablesResponsive").Include(
                       "~/Scripts/DataTables/dataTables.rowReorder.js",
                      "~/Scripts/DataTables/dataTables.responsive.js",
                       "~/Scripts/summernote/summernote.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/firebase").Include(
                     "~/Scripts/firebase-app.js",
                     "~/Scripts/firebase-messaging.js",
                     "~/firebase-messaging-sw.js"
                     ));

        }
    }
}
