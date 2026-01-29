using ClosedXML.Excel;
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
    public class ZoneSummaryReportController : Controller
    {
        // GET: ZoneSummaryReport

        //Regional Summary Report
        public ActionResult Index()
        {
            return View();
        }

        //All India Summary Report (Zonal Sumary Report)
        public ActionResult AllIndiaSummaryReport()
        {
            return View();
        }


        //Regional Summary Report
        public ActionResult GetGridDetail(DateTime report_date, string p_aum_bracket)
        {
            if (p_aum_bracket != null)
            {
                Session["aum_bracket"] = p_aum_bracket;

            }

            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_Zone_Summary(report_date, p_aum_bracket));
        }

        //Zonal Summary Report
        public ActionResult GetGridDetail_AllIndiaSummaryRpt(DateTime report_date, string p_aum_bracket)
        {
            if (p_aum_bracket != null)
            {
                Session["aum_bracket"] = p_aum_bracket;
            }

            CommonHelper.WriteLog("(Zonal Summary Report) Controller: report_date value is: " + report_date + " " + p_aum_bracket);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            objZoneSummary.GetGridDetail_AllIndia_Summary(report_date, p_aum_bracket);
            var result = objZoneSummary.GetGridDetail_AllIndia_Summary(report_date, p_aum_bracket);

            return new JsonNetResult(result);
        }
        public ActionResult GetGridDetail_Regional_Summary_zonewise(string zone_uti)
        {
            var session_aum = Session["aum_bracket"].ToString();

            CommonHelper.WriteLog("(GetGridDetail_Regional_Summary_zonewise) Controller: zone_uti value is: " + zone_uti);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_Regional_Summary_zonewise(zone_uti, session_aum));
        }
        public ActionResult GetGridDetail_UFC_Summary_RegionWise(string region_name_uti, string zone_uti)
        {
            var session_aum = Session["aum_bracket"].ToString();

            CommonHelper.WriteLog("(GetGridDetail_UFC_Summary_RegionWise) Controller: region_name_uti value is: " + region_name_uti + "Zone UTI value is " + zone_uti);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_UFC_Summary_RegionWise(region_name_uti, zone_uti, session_aum));
        }
        public ActionResult getGridDetailEmployeeSummaryReport_UFCWise(string region_name_uti, string zone_uti, string ufc_name)
        {
            var aum_bracket = Session["aum_bracket"].ToString();

            CommonHelper.WriteLog("(getGridDetailEmployeeSummaryReport_UFCWise) Controller: region_name_uti value is: " + region_name_uti + "Zone UTI value is " + zone_uti + "UFC Name: " + ufc_name);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_Employee_Summary_UFCwise(region_name_uti, zone_uti, ufc_name, aum_bracket));
        }

        public ActionResult Get_RegionalSummary_RH_Login()
        {
            var session_aum = Session["aum_bracket"].ToString();

            CommonHelper.WriteLog("(Get_RegionalSummary_RH_Login) Controller : session_aum Value: " + session_aum);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_Regional_Summary_RegionalHead_Login(session_aum));
        }

        public ActionResult Get_UFCSummary_CM_Login()
        {
            var session_aum = Session["aum_bracket"].ToString();

            //CommonHelper.WriteLog("(Get_UFCSummary_CM_Login) Controller : session_aum Value: " + session_aum);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_UFC_Summary_CM_login(session_aum));
        }

        public ActionResult Get_EmployeeSummary_RM_Login()
        {
            var session_aum = Session["aum_bracket"].ToString();

            CommonHelper.WriteLog("(Get_UFCSummary_CM_Login) Controller : session_aum Value: " + session_aum);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.Get_EmployeeSummary_RM_Login(session_aum));
        }

        public ActionResult GetAumBracketList()
        {

            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetAumBracket());
        }


        public ActionResult GetGridDetail_CountWise_ZonalSummaryRept(string count_type, string p_zone, string p_region, string p_ufc_name, string p_emp_id)
        {
            var aum_bracket = Session["aum_bracket"].ToString();
            CommonHelper.WriteLog("Counttype : " + count_type + "zone " + p_zone + "region " + p_region + " ufc Name " + p_ufc_name);

            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetGridDetail_CountWise_ZonalSummaryRept(count_type, p_zone, p_region, p_ufc_name, p_emp_id, aum_bracket));
        }

       


        // Zone Summary Rpt Popup zonewise
        public ActionResult GetPopup_ZoneSummary_Rpt_zonewise(string p_zone_uti)
        {
            CommonHelper.WriteLog("Controller GetPopup_ZoneSummary_Rpt_zonewise(" + p_zone_uti + ")");
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetPopup_ZoneSummary_Rpt_zonewise(p_zone_uti));
        }

        // Regional Summary rpt Popup Zone wise
        public ActionResult GetPopup_Regional_Summary_zonewise(string p_zone_uti, string p_region_name)
        {
            CommonHelper.WriteLog("Controller GetPopup_Regional_Summary_zonewise(): " + p_zone_uti + " " + p_region_name);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetPopup_Regional_Summary_zonewise(p_zone_uti, p_region_name));
        }

        // UFC Summary Report Popup region wise
        public ActionResult Get_PopupDetail_UFC_Summary(string p_zone_uti, string p_region_name, string p_ufc_name)
        {
            CommonHelper.WriteLog("Controller Get_PopupDetail_UFC_Summary(): " + p_zone_uti + " " + p_region_name + " " + p_ufc_name);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.Get_PopupDetail_UFC_Summary(p_zone_uti, p_region_name, p_ufc_name));
        }

        // RM Report Popup ufc wise
        public ActionResult Get_POP_EmployeeSummary(string p_zone_uti, string p_region_name, string p_ufc_name, string p_emp_id)
        {
            CommonHelper.WriteLog("Controller Get_POP_EmployeeSummary(): " + p_zone_uti + " " + p_region_name + " " + p_ufc_name + " " + p_emp_id);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.Get_POP_EmployeeSummary(p_zone_uti, p_region_name, p_ufc_name, p_emp_id));
        }

        //HISTORY POPUP ACNO WISE
        public ActionResult GetHistory_popup_acno_wise(string p_acno)
        {
            CommonHelper.WriteLog("Controller GetHistory_popup_acno_wise(): " + p_acno);
            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            return new JsonNetResult(objZoneSummary.GetHistory_popup_acno_wise(p_acno));
        }


        public FileResult DownloadExcelDashboard(DateTime report_date, string p_aum_bracket)
        {
            if (p_aum_bracket == null || p_aum_bracket == "")
            {
                p_aum_bracket = "All";
            }

            ZoneSummaryReport objZoneSummary = new ZoneSummaryReport();
            var b = objZoneSummary.GetGridDetail_Zone_Summary(report_date, p_aum_bracket);

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("DASHBOARD REPORT");




            var currentRow = 2;
            worksheet.Cell(currentRow, 1).Value = "ZONE";

            //STARTING UNIVERSE
            worksheet.Cell(currentRow, 2).Value = "Count";
            worksheet.Cell(currentRow, 3).Value = "%Count of All India";
            worksheet.Cell(currentRow, 4).Value = "AUM(in Cr)";
            worksheet.Cell(currentRow, 5).Value = "%AUM Share of All India";

            //NOT REMEDIATED / PENDING UNIVERSE
            worksheet.Cell(currentRow, 6).Value = "Count";
            worksheet.Cell(currentRow, 7).Value = "%Count of All India";
            worksheet.Cell(currentRow, 8).Value = "AUM(in Cr)";
            worksheet.Cell(currentRow, 9).Value = "%AUM Share of All India";

            //ACTIONED FOLIO
            worksheet.Cell(currentRow, 10).Value = "Count";

            //REMEDIATED UNIVERSE
            worksheet.Cell(currentRow, 11).Value = "Count";
            worksheet.Cell(currentRow, 12).Value = "%Count of All India";
            worksheet.Cell(currentRow, 13).Value = "AUM(in Cr)";
            worksheet.Cell(currentRow, 14).Value = "%AUM Share of All India";

            //KYC COMPLETED
            worksheet.Cell(currentRow, 15).Value = "Count";
            worksheet.Cell(currentRow, 16).Value = "AUM(in Cr)";

            //VALID BANK DETAILS
            worksheet.Cell(currentRow, 17).Value = "Count";
            worksheet.Cell(currentRow, 18).Value = "AUM(in Cr)";

            //NOMINEE UPDATED
            worksheet.Cell(currentRow, 19).Value = "Count";
            worksheet.Cell(currentRow, 20).Value = "AUM(in Cr)";

            //AADHAR SEEDED
            worksheet.Cell(currentRow, 21).Value = "Count";
            worksheet.Cell(currentRow, 22).Value = "AUM(in Cr)";

            //worksheet.Cell("A1").Value = "Title";
            worksheet.Range("B1:E1").Merge();
            worksheet.Range("B1:E1").Value = "Starting Universe";

            worksheet.Range("F1:I1").Merge();
            worksheet.Range("F1:I1").Value = "Not Remediated/ Pending Universe";

            //worksheet.Range("J1").Merge();
            worksheet.Range("J1").Value = "Actioned Count";
            //range.Merge().Style.Font.SetBold().Font.FontSize = 16;

            worksheet.Range("K1:N1").Merge();
            worksheet.Range("K1:N1").Value = "Remediated Universe";

            worksheet.Range("O1:P1").Merge();
            worksheet.Range("O1:P1").Value = "KYC Completed";

            worksheet.Range("Q1:R1").Merge();
            worksheet.Range("Q1:R1").Value = "Valid Bank Details";

            worksheet.Range("S1:T1").Merge();
            worksheet.Range("S1:T1").Value = "Nominee Updated";

            worksheet.Range("U1:V1").Merge();
            worksheet.Range("U1:V1").Value = "Nominee Updated";

            foreach (var item in b)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.ZONE_UTI;

                worksheet.Cell(currentRow, 2).Value = item.CNT_REG;
                worksheet.Cell(currentRow, 3).Value = item.P_CNT_ZONE;
                worksheet.Cell(currentRow, 4).Value = item.AUM_REG;
                worksheet.Cell(currentRow, 5).Value = item.P_AUM_ZONE;

                worksheet.Cell(currentRow, 6).Value = item.CNT_ZON_SEL;
                worksheet.Cell(currentRow, 7).Value = item.P_CNT_ZONE_SEL;
                worksheet.Cell(currentRow, 8).Value = item.AUM_ZON_SEL;
                worksheet.Cell(currentRow, 9).Value = item.P_AUM_ZONE_SEL;

                worksheet.Cell(currentRow, 10).Value = item.CNT_ZON_FA;

                worksheet.Cell(currentRow, 11).Value = item.CNT_ZON_REM;
                worksheet.Cell(currentRow, 12).Value = item.P_CNT_ZONE_REM;
                worksheet.Cell(currentRow, 13).Value = item.AUM_ZON_REM;
                worksheet.Cell(currentRow, 14).Value = item.P_AUM_ZONE_REM;

                worksheet.Cell(currentRow, 15).Value = item.CNT_REG_KYC;
                worksheet.Cell(currentRow, 16).Value = item.AUM_REG_KYC;

                worksheet.Cell(currentRow, 17).Value = item.CNT_REG_BANK;
                worksheet.Cell(currentRow, 18).Value = item.AUM_REG_BANK;

                worksheet.Cell(currentRow, 19).Value = item.CNT_REG_NOM;
                worksheet.Cell(currentRow, 20).Value = item.AUM_REG_NOM;

                worksheet.Cell(currentRow, 21).Value = item.CNT_REG_ASEED;
                worksheet.Cell(currentRow, 22).Value = item.AUM_REG_ASEED;


            }
            worksheet.Columns().AdjustToContents();
            worksheet.Rows().AdjustToContents();
            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DASHBOARD_REPORT.xlsx");
        }
    }
}