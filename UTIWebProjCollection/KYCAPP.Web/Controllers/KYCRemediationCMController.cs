using iTextSharp.text.pdf;
using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class KYCRemediationCMController : Controller
    {
        // GET: KYCRemediationCM
        KYCRemediation kyc = new KYCRemediation();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Download()
        {
            return View();
        }
        public ActionResult DownloadPDF(ExportParaModel model, string Search_Text)
        {
            CommonHelper.WriteLog("url :format-" + model.format_types + " | from_Date :" + model.from_date + " | todate :" + model.to_date + " | search_text: " + Search_Text);

            string temp = KYCRemediation.GetHtmlStringCM(model.from_date, model.to_date, Search_Text);
            byte[] pdfBytes = KYCRemediation.PdfTempNocopy(temp);
            return File(pdfBytes, "application/pdf", "KYC_SELECTED_DATA_" + DateTime.Now.ToString("dd_MMM_yyyy") + ".pdf");

        }

        public ActionResult GetGridDetail(string Search_Text, string P_AUM_BRACKET)
        {
            return new JsonNetResult(kyc.Get_CM_GridDetail(Search_Text, P_AUM_BRACKET));
        }

        public ActionResult GetEmpCodeDrodown()
        {
            return new JsonNetResult(kyc.GetEmpCodeName());
        }

        public ActionResult Save(List<KYC_DataModel> model, string selected_employee_code)
        {
            //CommonHelper.WriteLog("Selected empcode in cntrl :" + selected_employee_code);
            return new JsonNetResult(kyc.SaveRemediationCM(model, selected_employee_code));
        }
    }
}