using KYC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYC.Web.Controllers
{
    public class DataAcceptanceFullReportController : Controller
    {
        // GET: DataAcceptanceFullReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridDetail(DateTime from_date, DateTime to_date)
        {
            DataAcceptanceFullReport bll = new DataAcceptanceFullReport();
            return new JsonNetResult(bll.GetGridDetail(from_date, to_date));
        }
    }
}