using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    public class PanSearchController : Controller
    {
        // GET: PanSearch

        PanSearchModule ObjpanSearch = new PanSearchModule();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGridDetails(string PanNo)
        {
            var result = ObjpanSearch.GetGridDetail(PanNo);
            return new JsonNetResult(result);
        }
    }
}