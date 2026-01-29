using Dapper;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KYCAPP.Web.Models
{
    public class ZoneSummaryReport
    {

        // Regional Summary Report
        public List<ZoneSummaryReportModel> GetGridDetail_Zone_Summary(DateTime report_date, string p_aum_bracket)
        {
            CommonHelper.WriteLog("This is Zonesummary Rpt Function Report Date " + report_date);

            List<ZoneSummaryReportModel> Regional_Summary_List = new List<ZoneSummaryReportModel>();

            CommonHelper.WriteLog(DataAccess.DBConnectionString);
            try
            {
                using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
                {
                    try
                    {
                        CommonHelper.WriteLog("trying ot open connection");

                        conn.Open();
                        CommonHelper.WriteLog("conn opened");
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("emplid", UserManager.User.Code);
                        parameters.Add("report_dt", report_date.ToString("dd-MMM-yyyy"));
                        CommonHelper.WriteLog("This is Zonesummary Rpt Function Report Date parameters " + report_date);

                        string procedure = "mistest.KYC_DASHBOARD_DISPLAY('" + UserManager.User.Code + "','" + report_date.ToString("dd-MMM-yyyy") + "')";
                        conn.Query(procedure, commandType: CommandType.StoredProcedure).FirstOrDefault();

                        CommonHelper.WriteLog("(GetGridDetail_Zone_Summary) p_aum_bracket" + p_aum_bracket);

                        if (p_aum_bracket == "All")
                        {

                            string query1 = "select * from mistest.KYC_ZONE_DASH_DISPLAY";
                            Regional_Summary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                            CommonHelper.WriteLog("This is if condition in (GetGridDetail_Zone_Summary) and All");
                        }
                        else
                        {
                            string query2 = "select * from mistest.KYC_ZONE_AUM_DASH_DISPLAY where AUM_BRACKET = '" + p_aum_bracket + "'";
                            Regional_Summary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                            CommonHelper.WriteLog("This is else condition in GetGridDetail_Zone_Summary() ");
                        }



                        //string query1 = "select * from mistest.KYC_ZONE_DASH_DISPLAY";

                        //Regional_Summary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();

                        CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_Zone_Summary () :" + Regional_Summary_List.Count);

                        if (Regional_Summary_List.Count > 0)
                        {
                            ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                            dd.CNT_REG = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG)).ToString();
                            dd.P_CNT_ZONE = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                            dd.AUM_REG = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG)).ToString();
                            dd.P_AUM_ZONE = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                            dd.CNT_ZON_SEL = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_SEL)).ToString();
                            dd.P_CNT_ZONE_SEL = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                            dd.AUM_ZON_SEL = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_SEL)).ToString();
                            dd.P_AUM_ZONE_SEL = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                            dd.CNT_ZON_REM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_REM)).ToString();
                            dd.P_CNT_ZONE_REM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                            dd.AUM_ZON_REM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_REM)).ToString();
                            dd.P_AUM_ZONE_REM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                            dd.CNT_REG_KYC = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_KYC)).ToString();
                            dd.P_CNT_ZONE_KYC = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                            dd.AUM_REG_KYC = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_KYC)).ToString();
                            dd.P_AUM_ZONE_KYC = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                            dd.CNT_REG_BANK = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_BANK)).ToString();
                            dd.P_CNT_ZONE_BANK = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                            dd.AUM_REG_BANK = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_BANK)).ToString();
                            dd.P_AUM_ZONE_BANK = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                            dd.CNT_REG_NOM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_NOM)).ToString();
                            dd.P_CNT_ZONE_NOM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                            dd.AUM_REG_NOM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_NOM)).ToString();
                            dd.P_AUM_ZONE_NOM = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                            dd.CNT_REG_ASEED = Regional_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_ASEED)).ToString();
                            dd.P_CNT_ZONE_ASEED = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                            dd.AUM_REG_ASEED = Regional_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_ASEED)).ToString();
                            dd.P_AUM_ZONE_ASEED = Regional_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();
                            dd.REGION_NAME_UTI = "Grand Total";


                            Regional_Summary_List.Add(dd);
                        }


                    }
                    catch (Exception ex)
                    {
                        CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_Zone_Summary ()" + ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                CommonHelper.WriteLog("22 in ZoneSummaryReport> GetGridDetail_Zone_Summary ()" + e.Message);

            }
            return Regional_Summary_List;
        }

        //Zonal Summary Report
        public List<ZoneSummaryReportModel> GetGridDetail_AllIndia_Summary(DateTime report_date, string p_aum_bracket)
        {
            CommonHelper.WriteLog("This is Zonesummary Rpt Function Report Date " + report_date);

            List<ZoneSummaryReportModel> Zonal_Summary_List = new List<ZoneSummaryReportModel>();


            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {


                    conn.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("emplid", UserManager.User.Code);
                    parameters.Add("report_dt", report_date.ToString("dd-MMM-yyyy"));
                    CommonHelper.WriteLog("This is Zonesummary Rpt Function Report Date parameters " + report_date);


                    string procedure = "mistest.KYC_DASHBOARD_DISPLAY_EMPL('" + UserManager.User.Code + "','" + report_date.ToString("dd-MMM-yyyy") + "')";
                    conn.Query(procedure, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (p_aum_bracket == "All")
                    {

                        string query1 = "select * from mistest.KYC_CORP_DASH_DISPLAY" + "_" + UserManager.User.Code;
                        Zonal_Summary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                        CommonHelper.WriteLog("This is if condition and All");
                    }
                    else
                    {
                        string query2 = "select * from mistest.KYC_CORP_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where AUM_BRACKET = '" + p_aum_bracket + "'";
                        Zonal_Summary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                        CommonHelper.WriteLog("This is else condition in GetGridDetail_AllIndia_Summary()");
                    }

                    //Zonal_Summary_List = conn.Query<ZoneSummaryReportModel>(query, commandType: CommandType.StoredProcedure).ToList();


                    //Zonal_Summary_List = conn.Query<ZoneSummaryReportModel>("select * from mistest.KYC_CORP_DASH_DISPLAY").ToList();
                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_AllIndia_Summary () :" + Zonal_Summary_List.Count);

                    if (Zonal_Summary_List.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_REG = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG)).ToString();
                        dd.P_CNT_ZONE = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_REG = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG)).ToString();
                        dd.P_AUM_ZONE = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_ZON_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_ZON_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_ZON_FA = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_FA)).ToString();

                        dd.CNT_ZON_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_REM)).ToString();
                        dd.P_CNT_ZONE_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_ZON_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_REM)).ToString();
                        dd.P_AUM_ZONE_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_REG_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_REG_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_REG_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_REG_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_REG_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_REG_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_REG_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_REG_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();
                        dd.ZONE_UTI = "Grand Total";

                        Zonal_Summary_List.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_AllIndia_Summary ()" + ex.Message);
                }
            }

            return Zonal_Summary_List;
        }

        // Regional Summary Report Zone Wise
        public List<ZoneSummaryReportModel> GetGridDetail_Regional_Summary_zonewise(string zone_uti, string session_aum)
        {
            CommonHelper.WriteLog("This is GetGridDetail_Regional_Summary_zonewise  Rpt Function Zone" + zone_uti);

            List<ZoneSummaryReportModel> RegionalSummary_List = new List<ZoneSummaryReportModel>();


            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();

                    CommonHelper.WriteLog("(GetGridDetail_Regional_Summary_zonewise) Session AUM Bracket " + session_aum);

                    if (session_aum == "All")
                    {

                        string query1 = "select * from mistest.KYC_ZONE_DASH_DISPLAY" + "_" + UserManager.User.Code + " where ZONE_UTI ='" + zone_uti + "' "; ;
                        RegionalSummary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                        CommonHelper.WriteLog("This is if condition and All");
                    }
                    else
                    {
                        string query2 = "select * from mistest.KYC_ZONE_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where AUM_BRACKET = '" + session_aum + "'and ZONE_UTI = '" + zone_uti + "'";
                        RegionalSummary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                        CommonHelper.WriteLog("This is else condition in GetGridDetail_Regional_Summary_zonewise()");
                    }

                    //string query1 = "select * from mistest.KYC_ZONE_DASH_DISPLAY where ZONE_UTI ='" + zone_uti + "' ";
                    //RegionalSummary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();




                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_Regional_Summary_zonewise () : " + RegionalSummary_List.Count);

                    if (RegionalSummary_List.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_REG = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG)).ToString();
                        dd.P_CNT_ZONE = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_REG = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG)).ToString();
                        dd.P_AUM_ZONE = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_ZON_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_ZON_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_ZON_FA = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_FA)).ToString();

                        dd.CNT_ZON_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_REM)).ToString();
                        dd.P_CNT_ZONE_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_ZON_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_REM)).ToString();
                        dd.P_AUM_ZONE_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_REG_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_REG_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_REG_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_REG_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_REG_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_REG_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_REG_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_REG_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();
                        dd.REGION_NAME_UTI = "Grand Total";

                        RegionalSummary_List.Add(dd);
                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_Regional_Summary_zonewise ()" + ex.Message);
                }
            }

            return RegionalSummary_List;
        }

        // UFC Summary Report Region Wise
        public List<ZoneSummaryReportModel> GetGridDetail_UFC_Summary_RegionWise(string region_name_uti, string zone_uti, string session_aum)
        {
            CommonHelper.WriteLog("This is GetGridDetail_UFC_Summary_RegionWise  Rpt Function region_name_uti " + region_name_uti + " Zone UTI value is " + zone_uti);

            List<ZoneSummaryReportModel> UFC_Summary_List = new List<ZoneSummaryReportModel>();

            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();

                    CommonHelper.WriteLog("(GetGridDetail_UFC_Summary_RegionWise) Session AUM Bracket " + session_aum);
                    if (session_aum == "All")
                    {

                        string query1 = "select * from mistest.KYC_REG_DASH_DISPLAY" + "_" + UserManager.User.Code + " where zone_uti='" + zone_uti + "' and region_name_uti='" + region_name_uti + "'";
                        UFC_Summary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                        CommonHelper.WriteLog("This is if condition and All");
                    }
                    else
                    {
                        string query2 = "select * from mistest.KYC_REG_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where AUM_BRACKET = '" + session_aum + "' and zone_uti ='" + zone_uti + "' and region_name_uti='" + region_name_uti + "'";
                        UFC_Summary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                        CommonHelper.WriteLog("This is else condition in GetGridDetail_UFC_Summary_RegionWise()");
                    }

                    //string query1 = "select * from mistest.KYC_REG_DASH_DISPLAY where zone_uti='" + zone_uti + "' and region_name_uti='" + region_name_uti + "'";

                    //UFC_Summary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();


                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_UFC_Summary_RegionWise () : " + UFC_Summary_List.Count);

                    if (UFC_Summary_List.Count > 0)
                    {

                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_UFC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC)).ToString();
                        dd.P_CNT_REG = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG)).ToString();
                        dd.P_CNT_ZONE = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_UFC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC)).ToString();
                        dd.P_AUM_REG = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG)).ToString();
                        dd.P_AUM_ZONE = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_UFC_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_SEL)).ToString();
                        dd.P_CNT_REG_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_UFC_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_SEL)).ToString();
                        dd.P_AUM_REG_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_UFC_FA = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_FA)).ToString();

                        dd.CNT_UFC_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_REM)).ToString();
                        dd.P_CNT_REG_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_REM)).ToString();
                        dd.P_CNT_ZONE_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_UFC_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_REM)).ToString();
                        dd.P_AUM_REG_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_REM)).ToString();
                        dd.P_AUM_ZONE_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_UFC_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_KYC)).ToString();
                        dd.P_CNT_REG_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_UFC_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_KYC)).ToString();
                        dd.P_AUM_REG_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_UFC_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_BANK)).ToString();
                        dd.P_CNT_REG_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_UFC_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_BANK)).ToString();
                        dd.P_AUM_REG_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_UFC_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_NOM)).ToString();
                        dd.P_CNT_REG_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_UFC_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_NOM)).ToString();
                        dd.P_AUM_REG_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_UFC_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_ASEED)).ToString();
                        dd.P_CNT_REG_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_UFC_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_ASEED)).ToString();
                        dd.P_AUM_REG_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();

                        dd.UFC_NAME = "Grand Total";
                        UFC_Summary_List.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_UFC_Summary_RegionWise ()" + ex.Message);
                }
            }


            return UFC_Summary_List;
        }


        // Employee Summary Report UFC Wise
        public List<ZoneSummaryReportModel> GetGridDetail_Employee_Summary_UFCwise(string region_name_uti, string zone_uti, string ufc_name, string aum_bracket)
        {
            CommonHelper.WriteLog("This is GetGridDetail_Employee_Summary_UFCwise  Rpt Function region_name_uti " + region_name_uti + " Zone UTI value is " + zone_uti + " UFC Name: " + ufc_name);

            List<ZoneSummaryReportModel> Employee_Summary_list = new List<ZoneSummaryReportModel>();

            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();

                    if (aum_bracket.ToUpper() == "ALL")
                    {
                        string query1 = "select * from mistest.KYC_UFC_DASH_DISPLAY" + "_" + UserManager.User.Code + " where zone_uti='" + zone_uti + "' and region_name_uti='" + region_name_uti + "' and ufc_name ='" + ufc_name + "'";
                        CommonHelper.WriteLog("Query of Employee Smry Report SLCTED ALL: " + query1);
                        Employee_Summary_list = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                    }
                    else
                    {

                        string query2 = "select * from mistest.KYC_UFC_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where zone_uti='" + zone_uti + "' and region_name_uti='" + region_name_uti + "' and ufc_name ='" + ufc_name + "' and AUM_BRACKET ='" + aum_bracket + "'";
                        CommonHelper.WriteLog("Query of Employee Smry Report: " + query2);
                        Employee_Summary_list = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                    }

                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_Employee_Summary_UFCwise() : " + Employee_Summary_list.Count);

                    if (Employee_Summary_list.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_UFC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC)).ToString();
                        dd.P_CNT_REG = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG)).ToString();
                        dd.P_CNT_ZON = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZON)).ToString();
                        dd.AUM_UFC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC)).ToString();
                        dd.P_AUM_REG = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG)).ToString();
                        dd.P_AUM_ZONE = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_UFC_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_SEL)).ToString();
                        dd.P_CNT_REG_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_UFC_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_SEL)).ToString();
                        dd.P_AUM_REG_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_UFC_FA = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_FA)).ToString();

                        dd.CNT_UFC_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_REM)).ToString();
                        dd.P_CNT_REG_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_REM)).ToString();
                        dd.P_CNT_ZONE_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_UFC_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_REM)).ToString();
                        dd.P_AUM_REG_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_REM)).ToString();
                        dd.P_AUM_ZONE_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_UFC_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_KYC)).ToString();
                        dd.P_CNT_REG_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_UFC_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_KYC)).ToString();
                        dd.P_AUM_REG_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_UFC_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_BANK)).ToString();
                        dd.P_CNT_REG_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_UFC_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_BANK)).ToString();
                        dd.P_AUM_REG_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_UFC_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_NOM)).ToString();
                        dd.P_CNT_REG_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_UFC_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_NOM)).ToString();
                        dd.P_AUM_REG_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_UFC_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_ASEED)).ToString();
                        dd.P_CNT_REG_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_UFC_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_ASEED)).ToString();
                        dd.P_AUM_REG_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();

                        dd.NAME = "Grand Total";
                        Employee_Summary_list.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_Employee_Summary_UFCwise () " + ex.Message);
                }
            }


            return Employee_Summary_list;
        }

        // Regional Summary Report Regional Head login
        public List<ZoneSummaryReportModel> GetGridDetail_Regional_Summary_RegionalHead_Login(string session_aum)
        {
            CommonHelper.WriteLog("This is GetGridDetail_Regional_Summary_RegionalHead_Login  Rpt Function session_aum" + session_aum);

            List<ZoneSummaryReportModel> RegionalSummary_List = new List<ZoneSummaryReportModel>();


            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();

                    CommonHelper.WriteLog("(GetGridDetail_Regional_Summary_RegionalHead_Login) Session AUM Bracket " + session_aum);

                    if (session_aum == "All")
                    {

                        string query1 = "select * from mistest.KYC_ZONE_DASH_DISPLAY" + "_" + UserManager.User.Code;
                        RegionalSummary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                        CommonHelper.WriteLog("This is if condition and All GetGridDetail_Regional_Summary_RegionalHead_Login");
                    }
                    else
                    {
                        string query2 = "select * from mistest.KYC_ZONE_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where AUM_BRACKET = '" + session_aum + "'";
                        RegionalSummary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                        CommonHelper.WriteLog("This is else condition in GetGridDetail_Regional_Summary_RegionalHead_Login()");
                    }


                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_Regional_Summary_RegionalHead_Login() : " + RegionalSummary_List.Count);

                    if (RegionalSummary_List.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_REG = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG)).ToString();
                        dd.P_CNT_ZONE = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_REG = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG)).ToString();
                        dd.P_AUM_ZONE = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_ZON_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_ZON_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_ZON_FA = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_FA)).ToString();

                        dd.CNT_ZON_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_REM)).ToString();
                        dd.P_CNT_ZONE_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_ZON_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_REM)).ToString();
                        dd.P_AUM_ZONE_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_REG_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_REG_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_REG_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_REG_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_REG_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_REG_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_REG_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_REG_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();
                        dd.REGION_NAME_UTI = "Grand Total";

                        RegionalSummary_List.Add(dd);
                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_Regional_Summary_RegionalHead_Login()" + ex.Message);
                }
            }

            return RegionalSummary_List;
        }

        // UFC Summary Report CM Login
        public List<ZoneSummaryReportModel> GetGridDetail_UFC_Summary_CM_login(string session_aum)
        {
            CommonHelper.WriteLog("This is GetGridDetail_UFC_Summary_CM_login()  Rpt Function session_aum " + session_aum);

            List<ZoneSummaryReportModel> UFC_Summary_List = new List<ZoneSummaryReportModel>();

            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();

                    CommonHelper.WriteLog("(GetGridDetail_UFC_Summary_CM_login) Session AUM Bracket " + session_aum);
                    if (session_aum == "All")
                    {

                        string query1 = "select * from mistest.KYC_REG_DASH_DISPLAY" + "_" + UserManager.User.Code;
                        UFC_Summary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                        CommonHelper.WriteLog("This is if condition and All  GetGridDetail_UFC_Summary_CM_login()");
                    }
                    else
                    {
                        string query2 = "select * from mistest.KYC_REG_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where AUM_BRACKET = '" + session_aum + "'";
                        UFC_Summary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                        CommonHelper.WriteLog("This is else condition in GetGridDetail_UFC_Summary_CM_login()");
                    }



                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetGridDetail_UFC_Summary_CM_login() : " + UFC_Summary_List.Count);

                    if (UFC_Summary_List.Count > 0)
                    {

                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_UFC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC)).ToString();
                        dd.P_CNT_REG = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG)).ToString();
                        dd.P_CNT_ZONE = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_UFC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC)).ToString();
                        dd.P_AUM_REG = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG)).ToString();
                        dd.P_AUM_ZONE = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_UFC_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_SEL)).ToString();
                        dd.P_CNT_REG_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_UFC_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_SEL)).ToString();
                        dd.P_AUM_REG_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_UFC_FA = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_FA)).ToString();

                        dd.CNT_UFC_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_REM)).ToString();
                        dd.P_CNT_REG_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_REM)).ToString();
                        dd.P_CNT_ZONE_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_UFC_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_REM)).ToString();
                        dd.P_AUM_REG_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_REM)).ToString();
                        dd.P_AUM_ZONE_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_UFC_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_KYC)).ToString();
                        dd.P_CNT_REG_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_UFC_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_KYC)).ToString();
                        dd.P_AUM_REG_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_UFC_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_BANK)).ToString();
                        dd.P_CNT_REG_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_UFC_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_BANK)).ToString();
                        dd.P_AUM_REG_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_UFC_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_NOM)).ToString();
                        dd.P_CNT_REG_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_UFC_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_NOM)).ToString();
                        dd.P_AUM_REG_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_UFC_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_ASEED)).ToString();
                        dd.P_CNT_REG_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_UFC_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_ASEED)).ToString();
                        dd.P_AUM_REG_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();

                        dd.UFC_NAME = "Grand Total";
                        UFC_Summary_List.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetGridDetail_UFC_Summary_RegionWise() " + ex.Message);
                }
            }


            return UFC_Summary_List;
        }


        //Employee Summary List CM Login
        public List<ZoneSummaryReportModel> Get_EmployeeSummary_RM_Login(string aum_bracket)
        {
            CommonHelper.WriteLog("This is Get_EmployeeSummary_CM_Login  Rpt Function aum_bracket " + aum_bracket);

            List<ZoneSummaryReportModel> Employee_Summary_list = new List<ZoneSummaryReportModel>();

            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    if (aum_bracket == "All")
                    {


                        string query1 = "select * from mistest.KYC_UFC_DASH_DISPLAY" + "_" + UserManager.User.Code;
                        CommonHelper.WriteLog("Query of Employee Smry Report: " + query1);
                        Employee_Summary_list = conn.Query<ZoneSummaryReportModel>(query1).ToList();
                    }
                    else
                    {
                        string query2 = "select * from mistest.KYC_UFC_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where AUM_BRACKET ='" + aum_bracket + "'";
                        CommonHelper.WriteLog("Query of Employee Smry Report: " + query2);
                        Employee_Summary_list = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                    }


                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " Get_EmployeeSummary_CM_Login() : " + Employee_Summary_list.Count);

                    if (Employee_Summary_list.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_UFC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC)).ToString();
                        dd.P_CNT_REG = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG)).ToString();
                        dd.P_CNT_ZON = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZON)).ToString();
                        dd.AUM_UFC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC)).ToString();
                        dd.P_AUM_REG = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG)).ToString();
                        dd.P_AUM_ZONE = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_UFC_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_SEL)).ToString();
                        dd.P_CNT_REG_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_UFC_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_SEL)).ToString();
                        dd.P_AUM_REG_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_UFC_FA = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_FA)).ToString();

                        dd.CNT_UFC_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_REM)).ToString();
                        dd.P_CNT_REG_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_REM)).ToString();
                        dd.P_CNT_ZONE_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_UFC_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_REM)).ToString();
                        dd.P_AUM_REG_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_REM)).ToString();
                        dd.P_AUM_ZONE_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_UFC_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_KYC)).ToString();
                        dd.P_CNT_REG_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_UFC_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_KYC)).ToString();
                        dd.P_AUM_REG_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_UFC_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_BANK)).ToString();
                        dd.P_CNT_REG_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_UFC_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_BANK)).ToString();
                        dd.P_AUM_REG_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_UFC_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_NOM)).ToString();
                        dd.P_CNT_REG_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_UFC_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_NOM)).ToString();
                        dd.P_AUM_REG_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_UFC_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_ASEED)).ToString();
                        dd.P_CNT_REG_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_UFC_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_ASEED)).ToString();
                        dd.P_AUM_REG_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();

                        dd.NAME = "Grand Total";
                        Employee_Summary_list.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> Get_EmployeeSummary_CM_Login() " + ex.Message);
                }
            }


            return Employee_Summary_list;
        }

        //Get Aum Bracket Dropdown
        public List<DDLModel> GetAumBracket()
        {
            List<DDLModel> Aum_bracket_list = new List<DDLModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    Aum_bracket_list = conn.Query<DDLModel>(QueryMaster.Aum_Bracket_list_Query, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in ZoneSummaryReport> GetAumBracket ()" + Aum_bracket_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetAumBracket() " + ex.Message);
                }
            }
            return Aum_bracket_list;
        }

        public List<KYC_DataModel> GetGridDetail_CountWise_ZonalSummaryRept(string count_type, string p_zone, string p_region, string p_ufc_name, string p_emp_id, string aum_bracket)
        {
            List<KYC_DataModel> data_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(QueryMaster.Get_CountWise_details_Zonal);
                    sb.AppendLine(" inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = EK.EMPLOYEECODE left outer join(select ufc_code, ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode = um.ufc_code where");
                    sb.AppendLine("a.zone_uti = '" + p_zone + "' and a.region_name_uti = '" + p_region + "'");

                    DynamicParameters parameters = new DynamicParameters();

                    if (!string.IsNullOrWhiteSpace(p_ufc_name))
                    {
                        sb.AppendLine("AND a.ufc_name ='" + p_ufc_name + "'");

                    }

                    if (!string.IsNullOrWhiteSpace(count_type))
                    {
                        // For Selection Count
                        if (count_type == "S")
                        {
                            sb.AppendLine("AND selected_rec='S'");
                        }
                        // For Action Folio Count
                        else if (count_type == "A")
                        {
                            sb.AppendLine("and remark_code is not null");
                        }
                        // For Remediated Count
                        else if (count_type == "R")
                        {
                            sb.AppendLine("and selected_rec = 'R'");
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(p_emp_id))
                    {
                        if (p_emp_id.ToUpper() == "NOT SELECTED")
                        {
                            CommonHelper.WriteLog("Not Selected Employee Id");
                        }

                        sb.AppendLine("and a.selected_empid = '" + p_emp_id + "'");
                    }
                    if (aum_bracket.ToUpper() != "ALL")
                    {
                        sb.AppendLine("and a.AUM_BRACKET ='" + aum_bracket + "'");
                    }


                    CommonHelper.WriteLog("query of Count Wise Records :\n" + sb.ToString());
                    data_list = conn.Query<KYC_DataModel>(sb.ToString()).ToList();

                    CommonHelper.WriteLog("GetGridDetail_CountWise_ZonalSummaryRept() Count: " + data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZonalSummaryReport> GetGridDetail_CountWise_ZonalSummaryRept() " + ex.Message);
                }
            }
            return data_list;
        }

        //For AUM Slab (Zone Wise)

        public List<ZoneSummaryReportModel> GetPopup_ZoneSummary_Rpt_zonewise(string p_zone_uti)
        {
            CommonHelper.WriteLog("GetPopup_ZoneSummary_Rpt_zonewise(): zone_name :" + p_zone_uti);

            List<ZoneSummaryReportModel> Zonal_Summary_List = new List<ZoneSummaryReportModel>();


            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();


                    string query2 = "select * from mistest.KYC_CORP_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where ZONE_UTI = '" + p_zone_uti + "' order by order_no";
                    Zonal_Summary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();
                    CommonHelper.WriteLog("This is else condition in GetGridDetail_AllIndia_Summary()");

                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetPopup_ZoneSummary_Rpt_zonewise(): " + Zonal_Summary_List.Count);

                    if (Zonal_Summary_List.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_REG = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG)).ToString();
                        dd.P_CNT_ZONE = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_REG = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG)).ToString();
                        dd.P_AUM_ZONE = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_ZON_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_ZON_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_ZON_FA = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_FA)).ToString();

                        dd.CNT_ZON_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_REM)).ToString();
                        dd.P_CNT_ZONE_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_ZON_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_REM)).ToString();
                        dd.P_AUM_ZONE_REM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_REG_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_REG_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_REG_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_REG_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_REG_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_REG_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_REG_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_REG_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = Zonal_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();
                        dd.ZONE_UTI = "Grand Total";

                        Zonal_Summary_List.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetPopup_ZoneSummary_Rpt_zonewise() " + ex.Message);
                }
            }

            return Zonal_Summary_List;
        }


        // Regional Summary Report Popup
        public List<ZoneSummaryReportModel> GetPopup_Regional_Summary_zonewise(string p_zone_uti, string p_region_name)
        {
            CommonHelper.WriteLog("This is GetPopup_Regional_Summary_zonewise()  Rpt Function Zone" + p_zone_uti + " region_name " + p_region_name);

            List<ZoneSummaryReportModel> RegionalSummary_List = new List<ZoneSummaryReportModel>();


            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();

                    string query1 = "select * from mistest.KYC_ZONE_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where  ZONE_UTI = '" + p_zone_uti + "'and region_name_uti ='" + p_region_name + "' order by order_no";
                    RegionalSummary_List = conn.Query<ZoneSummaryReportModel>(query1).ToList();

                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " GetPopup_Regional_Summary_zonewise() : " + RegionalSummary_List.Count);

                    if (RegionalSummary_List.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_REG = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG)).ToString();
                        dd.P_CNT_ZONE = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_REG = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG)).ToString();
                        dd.P_AUM_ZONE = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_ZON_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_ZON_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_ZON_FA = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_FA)).ToString();

                        dd.CNT_ZON_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_ZON_REM)).ToString();
                        dd.P_CNT_ZONE_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_ZON_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_ZON_REM)).ToString();
                        dd.P_AUM_ZONE_REM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_REG_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_REG_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_REG_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_REG_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_REG_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_REG_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_REG_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_REG_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = RegionalSummary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();
                        dd.REGION_NAME_UTI = "Grand Total";

                        RegionalSummary_List.Add(dd);
                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetPopup_Regional_Summary_zonewise() " + ex.Message);
                }
            }

            return RegionalSummary_List;
        }


        // UFC Summary Popup
        public List<ZoneSummaryReportModel> Get_PopupDetail_UFC_Summary(string p_zone_uti, string p_region_name, string p_ufc_name)
        {

            List<ZoneSummaryReportModel> UFC_Summary_List = new List<ZoneSummaryReportModel>();

            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();


                    string query2 = "select * from mistest.KYC_REG_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where ZONE_UTI = '" + p_zone_uti + "'and region_name_uti ='" + p_region_name + "' and UFC_NAME = '" + p_ufc_name + "' order by order_no";
                    UFC_Summary_List = conn.Query<ZoneSummaryReportModel>(query2).ToList();



                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " Get_PopupDetail_UFC_Summary() : " + UFC_Summary_List.Count);

                    if (UFC_Summary_List.Count > 0)
                    {

                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_UFC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC)).ToString();
                        dd.P_CNT_REG = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG)).ToString();
                        dd.P_CNT_ZONE = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE)).ToString();
                        dd.AUM_UFC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC)).ToString();
                        dd.P_AUM_REG = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG)).ToString();
                        dd.P_AUM_ZONE = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_UFC_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_SEL)).ToString();
                        dd.P_CNT_REG_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_UFC_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_SEL)).ToString();
                        dd.P_AUM_REG_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_UFC_FA = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_FA)).ToString();

                        dd.CNT_UFC_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_REM)).ToString();
                        dd.P_CNT_REG_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_REM)).ToString();
                        dd.P_CNT_ZONE_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_UFC_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_REM)).ToString();
                        dd.P_AUM_REG_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_REM)).ToString();
                        dd.P_AUM_ZONE_REM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_UFC_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_KYC)).ToString();
                        dd.P_CNT_REG_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_UFC_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_KYC)).ToString();
                        dd.P_AUM_REG_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_UFC_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_BANK)).ToString();
                        dd.P_CNT_REG_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_UFC_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_BANK)).ToString();
                        dd.P_AUM_REG_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_UFC_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_NOM)).ToString();
                        dd.P_CNT_REG_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_UFC_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_NOM)).ToString();
                        dd.P_AUM_REG_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_UFC_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.CNT_UFC_ASEED)).ToString();
                        dd.P_CNT_REG_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_UFC_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.AUM_UFC_ASEED)).ToString();
                        dd.P_AUM_REG_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = UFC_Summary_List.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();

                        dd.UFC_NAME = "Grand Total";
                        UFC_Summary_List.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> Get_PopupDetail_UFC_Summary() " + ex.Message);
                }
            }


            return UFC_Summary_List;
        }


        //Employee Summary Popup
        public List<ZoneSummaryReportModel> Get_POP_EmployeeSummary(string p_zone_uti, string p_region_name, string p_ufc_name, string p_emp_id)
        {

            List<ZoneSummaryReportModel> Employee_Summary_list = new List<ZoneSummaryReportModel>();

            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();

                    string query2 = "select * from mistest.KYC_UFC_AUM_DASH_DISPLAY" + "_" + UserManager.User.Code + " where ZONE_UTI = '" + p_zone_uti + "'and region_name_uti ='" + p_region_name + "' and UFC_NAME = '" + p_ufc_name + "' and EMPLOYEEID = '" + p_emp_id + "' order by order_no";
                    CommonHelper.WriteLog("Query of Employee Smry Report Popup: " + query2);
                    Employee_Summary_list = conn.Query<ZoneSummaryReportModel>(query2).ToList();


                    CommonHelper.WriteLog("Count of ZoneSummaryReport>" + " Get_POP_EmployeeSummary() : " + Employee_Summary_list.Count);

                    if (Employee_Summary_list.Count > 0)
                    {
                        ZoneSummaryReportModel dd = new ZoneSummaryReportModel();
                        dd.CNT_UFC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC)).ToString();
                        dd.P_CNT_REG = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG)).ToString();
                        dd.P_CNT_ZON = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZON)).ToString();
                        dd.AUM_UFC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC)).ToString();
                        dd.P_AUM_REG = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG)).ToString();
                        dd.P_AUM_ZONE = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE)).ToString();

                        dd.CNT_UFC_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_SEL)).ToString();
                        dd.P_CNT_REG_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_SEL)).ToString();
                        dd.P_CNT_ZONE_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_SEL)).ToString();
                        dd.AUM_UFC_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_SEL)).ToString();
                        dd.P_AUM_REG_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_SEL)).ToString();
                        dd.P_AUM_ZONE_SEL = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_SEL)).ToString();

                        dd.CNT_UFC_FA = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_FA)).ToString();

                        dd.CNT_UFC_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_REM)).ToString();
                        dd.P_CNT_REG_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_REM)).ToString();
                        dd.P_CNT_ZONE_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_REM)).ToString();
                        dd.AUM_UFC_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_REM)).ToString();
                        dd.P_AUM_REG_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_REM)).ToString();
                        dd.P_AUM_ZONE_REM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_REM)).ToString();

                        dd.CNT_UFC_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_KYC)).ToString();
                        dd.P_CNT_REG_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_KYC)).ToString();
                        dd.P_CNT_ZONE_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_KYC)).ToString();
                        dd.AUM_UFC_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_KYC)).ToString();
                        dd.P_AUM_REG_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_KYC)).ToString();
                        dd.P_AUM_ZONE_KYC = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_KYC)).ToString();

                        dd.CNT_UFC_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_BANK)).ToString();
                        dd.P_CNT_REG_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_BANK)).ToString();
                        dd.P_CNT_ZONE_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_BANK)).ToString();
                        dd.AUM_UFC_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_BANK)).ToString();
                        dd.P_AUM_REG_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_BANK)).ToString();
                        dd.P_AUM_ZONE_BANK = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_BANK)).ToString();

                        dd.CNT_UFC_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_NOM)).ToString();
                        dd.P_CNT_REG_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_NOM)).ToString();
                        dd.P_CNT_ZONE_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_NOM)).ToString();
                        dd.AUM_UFC_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_NOM)).ToString();
                        dd.P_AUM_REG_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_NOM)).ToString();
                        dd.P_AUM_ZONE_NOM = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_NOM)).ToString();

                        dd.CNT_UFC_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.CNT_UFC_ASEED)).ToString();
                        dd.P_CNT_REG_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_REG_ASEED)).ToString();
                        dd.P_CNT_ZONE_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_CNT_ZONE_ASEED)).ToString();
                        dd.AUM_UFC_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.AUM_UFC_ASEED)).ToString();
                        dd.P_AUM_REG_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_REG_ASEED)).ToString();
                        dd.P_AUM_ZONE_ASEED = Employee_Summary_list.Sum(s => Convert.ToDouble(s.P_AUM_ZONE_ASEED)).ToString();

                        dd.NAME = "Grand Total";
                        Employee_Summary_list.Add(dd);

                    }

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> Get_POP_EmployeeSummary()" + ex.Message);
                }
            }


            return Employee_Summary_list;
        }

        public List<KYC_DataModel> GetHistory_popup_acno_wise(string p_acno)
        {
            List<KYC_DataModel> History_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_acno", UserManager.User.Code);
                    History_list = conn.Query<KYC_DataModel>(QueryMaster.GetHistoryPopup_Query, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in ZoneSummaryReport> GetHistory_popup_acno_wise() " + History_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ZoneSummaryReport> GetHistory_popup_acno_wise() " + ex.Message);
                }
            }
            return History_list;
        }

    }
}