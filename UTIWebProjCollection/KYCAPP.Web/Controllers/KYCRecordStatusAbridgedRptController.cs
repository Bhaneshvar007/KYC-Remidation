using DocumentFormat.OpenXml.EMMA;
using iTextSharp.text.pdf;
using KYCAPP.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class KYCRecordStatusAbridgedRptController : Controller
    {
        // GET: KYCRecordStatusAbridgedRpt
        KYCAbridgedRpt objabr = new KYCAbridgedRpt();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bind_data(List<string> P_FOLIO_NO)
        {
            if (P_FOLIO_NO != null)
            {
                Session["Pdf_details"] = P_FOLIO_NO;
                //var a = (List<KYC_DataModel>)Session["Pdf_details"];
                var a = (List<string>)Session["Pdf_details"];
                CommonHelper.WriteLog("Count of session value " + a.Count());
            }
            return new JsonNetResult("Success");
        }

        public ActionResult DownloadPDF()
        {
            //var a = (List<KYC_DataModel>)Session["Pdf_details"];
            var a = (List<string>)Session["Pdf_details"];
            CommonHelper.WriteLog("Count of session pdf_details: " + a.Count());

            //CommonHelper.WriteLog("Count of pdf download for Abridged Report - " + a.Count());

            string temp = KYCAbridgedRpt.GetHtmlStringAbridged_Report(a);
            byte[] pdfBytes = KYCRemediation.PdfTempNocopy(temp);
            return File(pdfBytes, "application/pdf", "KYC_SELECTED_DATA_" + DateTime.Now.ToString("dd_MMM_yyyy") + ".pdf");

        }

        public ActionResult DownloadCoverLetter(List<string> P_FOLIO_NO)
        {
            //var a = (List<string>)Session["Pdf_details"];
            CommonHelper.WriteLog("Count of CoverLetter_details: " + P_FOLIO_NO.Count());

            string temp = KYCAbridgedRpt.Get_letter_Format(P_FOLIO_NO);
            //byte[] bytes = KYCRemediation.Convert2(temp.ToString());
            //return File(bytes, "application/pdf", "Cover_Letter" + DateTime.Now.ToString("dd_MMM_yyyy") + ".pdf");
            return Json(temp);
        }

        public ActionResult GetGridDetail(KYC_ABRIDGED_SEARCH objSe)
        {
            CommonHelper.WriteLog("AbridgedReport GETGRIDDETAIL IN Controller Search_text " + objSe.P_SEARCH_TEXT + "EMP_NAME_SEARCH " + objSe.EMP_NAME_SEARCH + "UFC_NAME_SEARCH " + objSe.UFC_NAME_SEARCH + "AUM_BRACKET_SEARCH " + objSe.AUM_BRACKET_SEARCH + "BANK_FLAG_SEARCH " + objSe.BANK_FLAG_SEARCH + "AADHARSEEDINGFLAG_SEARCH " + objSe.AADHARSEEDINGFLAG_SEARCH + "KYCFLAG_SEARCH " + objSe.KYCFLAG_SEARCH + "NOMINEEFLAG_SEARCH " + objSe.NOMINEEFLAG_SEARCH);

            return new JsonNetResult(objabr.GetGridDetailKYCRecordStatusAbridged(objSe));
        }

        public ActionResult Get_UFC_List()
        {
            return new JsonNetResult(objabr.Get_UFC_List());
        }

        public ActionResult Get_Employee_List(string p_ufc_name)
        {
            return new JsonNetResult(objabr.Get_Employee_List(p_ufc_name));
        }

        public ActionResult GetEKYCAbrdigedDataListing(DataTableParams dataTableParams)
        {

            try
            {


                CommonHelper.WriteLog("GetEKYCAbrdigedDataListing Controller: " + dataTableParams.start);
                string employeeCode = Convert.ToString(UserManager.User.Code);
                //string searchText = dataTableParams.searchText;
                //int? filterStatus = dataTableParams.filterStatus;
                //string sortColumn = dataTableParams.sortColumn;
                //string sortDirection = dataTableParams.sortDirection;
                //int start = dataTableParams.start;
                //int length = dataTableParams.length;

                //dataTableParams.emp


                List<KYC_DataModel> lstKYCModel = objabr.GetGridDetailKYCRecordStatusAbridgedwithPagination(dataTableParams);

                //return new JsonNetResult();


                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(CommonHelper.GetBaseURL);

                //    searchText = Convert.ToString((searchText == null || searchText == "") ? "nodata" : searchText);

                //    var responseTask = client.GetAsync("api/Employee/GetTourListing/" + start + "/" + length + "/" + searchText + "/" + filterStatus + "/" + sortColumn + "/" + sortColumn + "/" + sortDirection + "/" + employeeCode);

                //    responseTask.Wait();
                //    var result = responseTask.Result;
                //    if (result.IsSuccessStatusCode)
                //    {
                //        var readTask = result.Content.ReadAsAsync<List<KYC_DataModel>>();
                //        readTask.Wait();
                //        lstTourRequest = readTask.Result;
                //    }
                //}

                return Json(new
                {

                    iTotalRecords = lstKYCModel.Count > 0 ? lstKYCModel[0].TOTAL_RECORDS : 0,
                    iTotalDisplayRecords = lstKYCModel.Count > 0 ? lstKYCModel[0].TOTAL_RECORDS : 0,
                    aaData = lstKYCModel
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("GetEKYCAbrdigedDataListing Controller: " + ex.Message);
                throw ex;
            }


        }


    }

    public class DataTableParams
    {
        public string searchText { get; set; }
        public int? filterStatus { get; set; }
        public string sortColumn { get; set; }
        public string sortDirection { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string p_search_text { get; set; }
    }
}