using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class DataAcceptanceFullReportController : Controller
    {
        // GET: DataAcceptanceFullReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridDetail(KYC_DETAILS_SEARCH objSearch)
        {
            DataAcceptanceFullReport bll = new DataAcceptanceFullReport();
            return new JsonNetResult(bll.GetGridDetail(objSearch));
        }
    }
}