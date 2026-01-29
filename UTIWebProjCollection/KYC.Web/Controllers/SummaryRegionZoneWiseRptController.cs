using KYC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYC.Web.Controllers
{
    public class SummaryRegionZoneWiseRptController : Controller
    {
        // GET: SummaryRegionZoneWiseRpt
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridDetail(string zone, string region, string ufc, DateTime from_date, DateTime to_date)
        {
            SummaryRegionZoneWise bll = new SummaryRegionZoneWise();
            return new JsonNetResult(bll.GetGridDetail(zone, region, ufc, from_date, to_date));
        }
    }
}