using System.Web;
using System.Web.Optimization;

namespace KYCAPP.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.7.0.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick  the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/jqueryui-1.13.2.min.js",
                      "~/Scripts/bootstrap_3.3.7.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-animate.min.js",
                      "~/Scripts/toaster.js",
                      "~/Assets/app.js",
                       "~/Scripts/ng/angular-validator.js",
                      "~/Scripts/ng/angular-validator-rules.js",
                      "~/Scripts/ng/ui-bootstrap-tpls-2.1.3.js",
                      "~/Assets/menu.js",
                      "~/Assets/common.js",
                      "~/Assets/kycRemediation.js",
                      "~/Assets/dataAcceptanceFullReport.js",
                      "~/Assets/summaryRegionZoneWise.js",
                      "~/Assets/dataAcceptanceReportStaff.js",
                      "~/Assets/dataDownload.js",
                      "~/Assets/kycRemediationCM.js",
                      "~/Assets/UpdateStatusFolio.js",
                      "~/Assets/KYCRecordStatusAbridgedReport.js",
                      "~/Assets/RegionalSummaryReport.js",
                      "~/Assets/ZonalSummaryReport.js",
                      "~/Assets/ReasonWiseReport.js",
                      "~/Assets/RealloactionUFCReport.js",
                      "~/Assets/PanSearchModule.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.min.css",
                      "~/Content/toaster.css"
                      ));
        }
    }
}
