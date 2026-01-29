using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KYC.Web.Models;

namespace KYC.Web.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        [CustomAuthorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
