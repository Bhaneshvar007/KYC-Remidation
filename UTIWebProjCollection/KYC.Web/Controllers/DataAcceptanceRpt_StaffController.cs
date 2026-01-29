using KYC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYC.Web.Controllers
{
    public class DataAcceptanceRpt_StaffController : Controller
    {
        // GET: DataAcceptanceRpt_Staff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridDetail(DateTime from_date, DateTime to_date)
        {
            DataAcceptanceRpt_Staff bll = new DataAcceptanceRpt_Staff();
            return new JsonNetResult(bll.GetGridDetail(from_date, to_date));
        }
    }
}