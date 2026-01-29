using KYC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYC.Web.Controllers
{
    public class KYCRemediationController : Controller
    {
        // GET: KYCRemediation
        KYCRemediation kyc = new KYCRemediation();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridDetail(string Search_Text)
        {
            return new JsonNetResult(kyc.GetGridDetail(Search_Text));
        }
        public ActionResult Save(List<KYC_DataModel> model)
        {
            return new JsonNetResult(kyc.Save(model));
        }
    }
}