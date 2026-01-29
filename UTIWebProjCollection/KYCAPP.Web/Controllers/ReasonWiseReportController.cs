using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class ReasonWiseReportController : Controller
    {
        // GET: ReasonWiseReport

        //Reason Wise Report Zone
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Region()
        {
            return View();
        }


        ReasonWiseReport objReasonWiseReport = new ReasonWiseReport();
        public ActionResult GetGridDetail(DateTime report_date)
        {

            CommonHelper.WriteLog("ReallocationUFCReportController Report_dt: " + report_date);

            return new JsonNetResult(objReasonWiseReport.GetGridDetailReasonwiseRept(report_date));
        }
        public ActionResult GetGridDetail_Region(DateTime report_date, string p_zone)
        {

            CommonHelper.WriteLog("GetGridDetail_Region > Controller Report_dt: " + report_date + "p_zone " + p_zone);

            return new JsonNetResult(objReasonWiseReport.GetGridDetailReasonwiseRept_Region(report_date, p_zone));
        }
        public ActionResult GetGridDetail_UFC(DateTime report_date, string p_zone, string p_region)
        {

            CommonHelper.WriteLog("ReallocationUFCReportController Report_dt: " + report_date + "p_zone " + p_zone + "p_region " + p_region);

            return new JsonNetResult(objReasonWiseReport.GetGridDetailReasonwiseRept_UFC(report_date, p_zone, p_region));
        }
        public ActionResult GetGridDetail_Emp(DateTime report_date, string p_zone, string p_region, string p_ufc)
        {

            CommonHelper.WriteLog("ReallocationUFCReportController Report_dt: " + report_date + "p_zone " + p_zone + "p_region " + p_region);

            return new JsonNetResult(objReasonWiseReport.GetGridDetailReasonwiseRept_Emp(report_date, p_zone, p_region, p_ufc));
        }

        public ActionResult GetPopupReasonwiseRept(DateTime report_date, string p_zone, string p_region, string p_ufc, string p_empId, string p_remark_code)
        {

           // CommonHelper.WriteLog("ReallocationUFCReportController Report_dt: " + report_date);

            return new JsonNetResult(objReasonWiseReport.GetGridPopupReasonwiseRept(report_date, p_zone, p_region, p_ufc, p_empId, p_remark_code));
        }
    }
}

