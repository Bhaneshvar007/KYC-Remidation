using KYCAPP.Web.Models;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class KYCRemediationController : Controller
    {
        // GET: KYCRemediation 
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

            string temp = KYCRemediation.GetHtmlString(model.from_date, model.to_date, Search_Text);
            byte[] pdfBytes = KYCRemediation.PdfTempNocopy(temp);
            return File(pdfBytes, "application/pdf", "KYC_SELECTED_DATA_" + DateTime.Now.ToString("dd_MMM_yyyy") + ".pdf");

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