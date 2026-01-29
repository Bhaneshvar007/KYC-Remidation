using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class SummaryRegionZoneWiseRptController : Controller
    {
        // GET: SummaryRegionZoneWiseRpt
        SummaryRegionZoneWise bll = new SummaryRegionZoneWise();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SummaryRpt()
        {
            return View();
        }

        public ActionResult PivotTables()
        {
            DateTime report_dt = DateTime.Now;
            var data = bll.GetGridDetail(report_dt);

            CommonHelper.WriteLog("Pivot data count: " + data.Count);
            ViewBag.jsondata = JsonConvert.SerializeObject(data);

            return View();
        }

        public ActionResult GetGridDetail(DateTime report_date)
        {
            return new JsonNetResult(bll.GetGridDetail(report_date));
        }
    }
}