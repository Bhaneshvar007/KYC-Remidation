using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class UpdateStatusFolioController : Controller
    {
        // GET: UpdateStatusFolio

        UpdateStatusFolio ups = new UpdateStatusFolio();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetGridDetail(DateTime P_Selected_dt, string P_ACNO, string P_INVNAME)
        {
            //CommonHelper.WriteLog("This is Update Status Folio GETGRIDDETAIL IN Controller " + "Selected date:" + P_Selected_dt + " | ACNO: " + P_ACNO + " | INVNAME: " + P_INVNAME);

            return new JsonNetResult(ups.GetGridDetailStatusFolio(P_Selected_dt, P_ACNO, P_INVNAME));
        }
        public ActionResult update_remark(KYC_DataModel model)
        {
            //CommonHelper.WriteLog("This is Update Status Folio Save IN Controller");
            return new JsonNetResult(ups.SaveUpdateStatusFolio(model));
        }

        public ActionResult GetRemarks()
        {
            //CommonHelper.WriteLog("This is GET GetRemarks() METHOD IN Controller");
            return new JsonNetResult(ups.GetRemarkList());
        }

      
    }
}
