using Dapper;
using KYCAPP.Web.Controllers;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;




namespace KYCAPP.Web.Models
{
    public class KYCAbridgedRpt
    {

        // NEW ONE
        //public List<KYC_DataModel> GetGridDetailKYCRecordStatusAbridged(DateTime report_dt)
        //{
        //    List<KYC_DataModel> record_Status_list = new List<KYC_DataModel>();
        //    using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            DynamicParameters parameters = new DynamicParameters();
        //            //parameters.Add("p_employee_code", UserManager.User.Code);
        //            parameters.Add("P_REPORTDT", report_dt);
        //            //parameters.Add("p_to_date", to_date);
        //            record_Status_list = conn.Query<KYC_DataModel>(QueryMaster.Get_grid_KYC_Record_Status_Abridged, parameters).ToList();
        //            CommonHelper.WriteLog("Count of KYCRemediation> GetGridDetailKYCRecordStatusAbridged () :" + record_Status_list.Count);

        //        }
        //        catch (Exception ex)
        //        {
        //            CommonHelper.WriteLog("error in KYCRemediation> GetGridDetailKYCRecordStatusAbridged ()" + ex.Message);
        //        }
        //    }
        //    return record_Status_list;
        //}

        //Old one
        public List<KYC_DataModel> GetGridDetailKYCRecordStatusAbridged(KYC_ABRIDGED_SEARCH objSe)
        {
            //CommonHelper.WriteLog("AbridgedReport GETGRIDDETAIL IN MEthod Search_text: " + objSe.P_SEARCH_TEXT + " EMP_NAME_SEARCH: " + objSe.EMP_NAME_SEARCH + " UFC_NAME_SEARCH: " + objSe.UFC_NAME_SEARCH + " AUM_BRACKET_SEARCH: " + objSe.AUM_BRACKET_SEARCH + " BANK_FLAG_SEARCH: " + objSe.BANK_FLAG_SEARCH + " AADHARSEEDINGFLAG_SEARCH: " + objSe.AADHARSEEDINGFLAG_SEARCH + " KYCFLAG_SEARCH: " + objSe.KYCFLAG_SEARCH + " NOMINEEFLAG_SEARCH: " + objSe.NOMINEEFLAG_SEARCH);

            //if (objSe.BANK_FLAG_SEARCH.Count > 0)
            //{

            //    for (int i = 0; i < objSe.BANK_FLAG_SEARCH.Count; i++)
            //    {
            //        CommonHelper.WriteLog("Bank Flag Values: " + objSe.BANK_FLAG_SEARCH[i]);
            //    }
            //}
            //if (objSe.AADHARSEEDINGFLAG_SEARCH.Count > 0)
            //{
            //    for (int i = 0; i < objSe.AADHARSEEDINGFLAG_SEARCH.Count; i++)
            //    {
            //        CommonHelper.WriteLog("AADHARSEEDINGFLAG_SEARCH Values: " + objSe.AADHARSEEDINGFLAG_SEARCH[i]);
            //    }
            //}
            //if (objSe.AUM_BRACKET_SEARCH.Count > 0)
            //{
            //    for (int i = 0; i < objSe.AUM_BRACKET_SEARCH.Count; i++)
            //    {
            //        CommonHelper.WriteLog("AUM_BRACKET_SEARCH Values: " + objSe.AUM_BRACKET_SEARCH[i]);
            //    }
            //}
            List<KYC_DataModel> record_Status_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(QueryMaster.Get_grid_KYC_Record_Status_Abridged);
                    sb.AppendLine(" left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = EK.EMPLOYEECODE left outer join EMPLOYEE_LIST_FOR_KYC ELK1 on a.selected_empid = ELK1.EMPLOYEECODE");
                    sb.AppendLine(" left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id left outer join(select ufc_code, ufc_name, region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location = um.ufc_code left outer join(select region_code, region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code = rmz.region_code left outer join mistest.KYC_AUM_BRACKET AB on a.AUM_BRACKET = ab.aum_bracket");
                    sb.AppendLine(" where case When ek.employeecode in ('2372', '2824', '1287', '3010', '3195', '8500', '4426', '4517', '4228') then 'VALID' WHEN ek.employeecode in ('8000', '4237') and a.ZONE_UTI in ('SOUTH', 'CORP') then 'VALID' WHEN ek.employeecode in ('4488', '4594', '2179')  and a.ZONE_UTI IN('WEST', 'NORTH', 'GULF', 'CORP')  then 'VALID' WHEN ek.employeecode in ('4471', '1073')  and a.ZONE_UTI IN('EAST', 'NORTH', 'WEST', 'GULF', 'CORP') then 'VALID' when((upper(trim(ek.emp_role)) = 'ZH' and a.zone_uti = cu.zone) or");
                    sb.AppendLine(" (upper(trim(ek.emp_role))='RH' and  a.region_name_uti=rmz.region_name and a.zone_uti=cu.zone) or(upper(trim(ek.emp_role)) = 'CM' and a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone  and a.ufc_NAME = um.UFC_NAME) or nvl(upper(trim(ek.employeecode)), 'ZZZ') = a.selected_empid) then 'VALID' else 'ALL' end = 'VALID'");
                    //old
                    //sb.AppendLine(" inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = EK.EMPLOYEECODE left outer join EMPLOYEE_LIST_FOR_KYC ELK1 on a.selected_empid = ELK1.EMPLOYEECODE");
                    //sb.AppendLine("left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.mis_cuser_logs where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id");
                    //sb.AppendLine(" left outer join (select ufc_code,ufc_name,region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location=um.ufc_code");
                    //sb.AppendLine("left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code left outer join mistest.KYC_AUM_BRACKET AB on a.AUM_BRACKET = ab.aum_bracket");
                    //sb.AppendLine("where case When ek.employeecode in ('2824', '1287', '3010', '3195', '8500', '4426', '4517', '4228') then 'VALID' WHEN ek.employeecode in ('8000', '4237') and a.ZONE_UTI = 'SOUTH' then 'VALID' WHEN ek.employeecode in ('4488', '4594', '2179')  and a.ZONE_UTI IN('WEST', 'NORTH')  then 'VALID' WHEN ek.employeecode in ('2372','1073','4471')  and ZONE_UTI = 'EAST' then 'VALID'");
                    //sb.AppendLine(" when ((upper(trim(ek.emp_role))='ZH' and a.zone_uti=cu.zone) or (upper(trim(ek.emp_role)) = 'RH' and  a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone) or(upper(trim(ek.emp_role)) = 'CM' and a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone  and a.ufc_NAME = um.UFC_NAME)");
                    //sb.AppendLine("or nvl(upper(trim(ek.employeecode)),'ZZZ')=a.selected_empid) then 'VALID' else 'ALL' end='VALID'");
                    if (!string.IsNullOrWhiteSpace(objSe.P_SEARCH_TEXT))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.ACNO||a.INVNAME||a.AGENT||a.CONCAT_ADD)),upper(trim('" + objSe.P_SEARCH_TEXT + "')))");
                    }
                    if (!string.IsNullOrWhiteSpace(objSe.EMP_NAME_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.SELECTED_EMPID||ELK1.NAME)),upper(trim('" + objSe.EMP_NAME_SEARCH + "')))");
                    }
                    if (!string.IsNullOrWhiteSpace(objSe.UFC_NAME_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(replace(replace(a.UFC_NAME,'(',''),')',''))),upper(trim(replace(replace('" + objSe.UFC_NAME_SEARCH + "','(',''),')','')))) ");
                        //sb.AppendLine("and REGEXP_LIKE(upper(trim(a.UFC_NAME)),upper(trim('" + objSe.UFC_NAME_SEARCH + "')))");
                    }
                    if (objSe.AUM_BRACKET_SEARCH != null)
                    {
                        if (objSe.AUM_BRACKET_SEARCH.Count > 0)
                        {
                            var strAum_Br = String.Join(",", objSe.AUM_BRACKET_SEARCH.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and a.AUM_BRACKET IN (" + strAum_Br + ")");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(objSe.KYCFLAG_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.KYCFLAG)),upper(trim('" + objSe.KYCFLAG_SEARCH + "')))");
                    }
                    if (!string.IsNullOrWhiteSpace(objSe.NOMINEEFLAG_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.NOMINEEFLAG)),upper(trim('" + objSe.NOMINEEFLAG_SEARCH + "')))");
                    }
                    if (objSe.AADHARSEEDINGFLAG_SEARCH != null)
                    {
                        if (objSe.AADHARSEEDINGFLAG_SEARCH.Count > 0)
                        {
                            var strAadhar_flag = String.Join(",", objSe.AADHARSEEDINGFLAG_SEARCH.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and a.AADHARSEEDINGFLAG IN (" + strAadhar_flag + ")");
                        }
                    }
                    if (objSe.BANK_FLAG_SEARCH != null)
                    {
                        if (objSe.BANK_FLAG_SEARCH.Count > 0)
                        {
                            var strBank_flag = String.Join(",", objSe.BANK_FLAG_SEARCH.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and a.BANK_FLAG IN (" + strBank_flag + ")");
                        }
                    }
                    if (objSe.FOLIO_STATUS != null)
                    {
                        if (objSe.FOLIO_STATUS.Count > 0)
                        {
                            var strFolioStatus = String.Join(",", objSe.FOLIO_STATUS.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and upper(trim(a.selected_rec)) IN (" + strFolioStatus + ")");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(objSe.FOLIONO_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(a.acno,'" + objSe.FOLIONO_SEARCH + "')");
                    }
                    if (!string.IsNullOrWhiteSpace(objSe.PRE_POST_2008))
                    {
                        sb.AppendLine("and REGEXP_LIKE(a.PRE_POST_2008, '" + objSe.PRE_POST_2008 + "')");
                    }


                    CommonHelper.WriteLog("query of> GetGridDetailKYCRecordStatusAbridged ()" + sb.ToString());
                    record_Status_list = conn.Query<KYC_DataModel>(sb.ToString()).ToList();
                    CommonHelper.WriteLog("Count of KYCRemediation> GetGridDetailKYCRecordStatusAbridged () :" + record_Status_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in KYCRemediation> GetGridDetailKYCRecordStatusAbridged ()" + ex.Message);
                }
            }
            return record_Status_list;
        }


        #region Letter Download
        public static string Get_letter_Format(List<string> p_folio_no)
        {
            // For checking the folio value
            //for (int i = 0; i < p_folio_no.Count; i++)
            //{
            //    CommonHelper.WriteLog("account selected to download pdf: " + p_folio_no[i]);
            //}

            List<KYC_DataModel> data_list = new List<KYC_DataModel>();
            StringBuilder sb = new StringBuilder();

            string Html_Template_Dir = ConfigurationManager.AppSettings.Get("html_template_location");
            string htmlstr = string.Empty;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    //parameters.Add("P_EMPID", UserManager.User.Code);
                    parameters.Add("p_folio_no", p_folio_no);

                    data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_DATA_DOWNLOAD_QUERY_FolioWise, parameters).ToList();
                    CommonHelper.WriteLog("total record found GetHtmlStringAbridged_Report() : " + data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("ERROR IN KYCRemediation> GetHtmlStringAbridged_Report() : " + ex.Message);
                }
            }

            if (data_list.Count > 0)
            {
                //string line = string.Empty;

                //using (StreamReader sr = new StreamReader(Html_Template_Dir + "\\" + "Cover_Letter.html"))
                //{



                //while ((line = sr.ReadLine()) != null && data_list.Count > 0)
                //{
                //    CommonHelper.WriteLog("line :" + line);
                //if (line.Trim() == "<br />")
                //{
                int running_count = 0;
                // sb.AppendLine(line);

                //sb.AppendLine("<!DOCTYPE html> <head><title>Cover Letter</title> </head>");
                foreach (KYC_DataModel kyc_data in data_list)
                {
                    sb.AppendLine("<img src='https://www.utimf.com/static/assets/images/uti_logo.png' width='100' style='align-item:center;'><hr/>");
                    sb.AppendLine("<div width='100 %' style='font-family: Georgia, serif; font-size: 15px; padding: 15px; margin - left: 5px; height: 100%; text-align: justify; text-justify: inter-word; height:900px;'><p><b> " + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</b></p>");
                    sb.AppendLine("<p>Dear Investor,</p><p style='margin-left: 50px;'><b> Sub: Completion of Know Your Client(KYC) and other formalities </b></p><br> <p> Greetings from UTI Mutual Fund.</p>");
                    sb.AppendLine("<p>It is observed that for your investment with UTI Mutual Fund, there are few important information that are not registered with us.Kindly refer to the attached Data Enrichment form for details of the missing information. </p>");
                    sb.AppendLine("<p> We, therefore, request you to kindly submit a duly filled in and signed Data Enrichment form together with supporting documents which are mentioned in the form, at the nearest UTI Financial Centre.For updation of KYC, please fill up the KYC form attached herewith, (along with a recent passport sized photograph) and submit it at the nearest UTI Financial Centre along with copies of: </p>");
                    sb.AppendLine(" <ol> <li> Pan Card </li> <li> Address proof </li> </ol>");
                    sb.AppendLine("<p> (Any one valid document – masked Aadhaar / proof of possession of Aadhaar, Passport, Driving License, Voter Id, NREGA Job Card, National Population Register(NPR) Letter. </p>");
                    sb.AppendLine(" <p> For any further assistance, please visit us at the below mentioned address of UTI Financial Centre or log on to https://www.utimf.com/ or call us at 18002661230 or write to us at uti@kfintech.com. </p>");
                    sb.AppendLine("<p> Our Office Address <ul> " + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</ul> </p>");
                    sb.AppendLine("<p> Yours sincerely, </p> <p> UTI Mutual Fund </p> </div>");
                    running_count++;
                }
                //CommonHelper.WriteLog("line " + running_count + ":" + line);
                //        }
                //        else
                //        {
                //            sb.AppendLine(line);
                //        }
                //    }
                //}
            }
            // CommonHelper.WriteLog(sb.ToString());
            return sb.ToString();
        }
        #endregion


        #region Get Pdf Folio wise in Abridged Report
        public static string GetHtmlStringAbridged_Report(List<string> p_folio_no)
        {
            // For checking the folio value
            //for (int i = 0; i < p_folio_no.Count; i++)
            //{
            //    CommonHelper.WriteLog("account selected to download pdf: " + p_folio_no[i]);
            //}

            List<KYC_DataModel> data_list = new List<KYC_DataModel>();
            StringBuilder sb = new StringBuilder();

            string Html_Template_Dir = ConfigurationManager.AppSettings.Get("html_template_location");
            string htmlstr = string.Empty;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    //parameters.Add("P_EMPID", UserManager.User.Code);
                    parameters.Add("p_folio_no", p_folio_no);

                    data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_DATA_DOWNLOAD_QUERY_FolioWise, parameters).ToList();
                    CommonHelper.WriteLog("total record found GetHtmlStringAbridged_Report() : " + data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("ERROR IN KYCRemediation> GetHtmlStringAbridged_Report() : " + ex.Message);
                }
            }

            if (data_list.Count > 0)
            {
                string line = string.Empty;

                using (StreamReader sr = new StreamReader(Html_Template_Dir + "\\" + "KYC_Processed_data.html"))
                {



                    while ((line = sr.ReadLine()) != null && data_list.Count > 0)
                    {
                        line = line.Replace("#{emp_code}", UserManager.User.Code);
                        line = line.Replace("#{emp_name}", UserManager.User.Name);
                        line = line.Replace("#{location}", string.IsNullOrWhiteSpace(data_list[0].LOCATION) ? "" : data_list[0].LOCATION);

                        CommonHelper.WriteLog("line :" + line);
                        if (line.Trim() == "<br />")
                        {
                            int running_count = 0;
                            int running_count1 = 0;
                            int running_count2 = 0;
                            var page_no = 0;
                            sb.AppendLine(line);
                            foreach (KYC_DataModel kyc_data in data_list)
                            {
                                running_count1++;
                                if (running_count1 == 0 || running_count1 == 1 || running_count1 == 2)
                                {
                                    //CommonHelper.WriteLog("Running Count1 in running_count1 == 0 || running_count1 == 1 || running_count1 == 2 : " + running_count1);

                                    sb.AppendLine("<table style='border:1px solid black; border-collapse: collapse;' width='100%; height:250px;'>");
                                    //sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SLNO :</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;' colspan='7'>" + kyc_data.SRNO + "</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ACNO </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>FIRST HOLDER</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SECOND HOLDER</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>THIRD HOLDER</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>DONOR NAME</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ADDRESS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>EMPLOYEE CODE</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ARN CODE</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b> " + kyc_data.ACNO + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.FIRST_HOLDER_NAME + "</b> </td>");
                                    sb.AppendLine("<span style='overflow:hidden; margin: 200px 10px 149px 200px; position: absolute; font-size: 25px; -webkit-transform: rotate(-45deg); - moz - transform: rotate(-45deg); -o-transform: rotate(-45deg); color: #dd9191; opacity: 0.5;'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");

                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.SECOND_HOLDER_NAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.THIRD_HOLDER_NAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.DONOR_NAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.EMPLOYEECODE + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.ARNCODE + "</b> </td>");
                                    sb.AppendLine("</tr>");

                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>FIRST HOLDER PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SECOND HOLDER PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>THIRD HOLDER PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>DONOR PAN</td>");
                                    //sb.AppendLine("<span style='overflow:hidden; margin: 200px 10px 149px 200px; position: absolute; font-size: 25px; -webkit-transform: rotate(-45deg); - moz - transform: rotate(-45deg); -o-transform: rotate(-45deg); color: #dd9191; opacity: 0.5;'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>GAURDIAN PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>EMPLOYEE NAME</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>EMAIL</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.FIRST_HOLDER_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.SECOND_HOLDER_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.THIRD_HOLDER_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.DONOR_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.GAURDIAN_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.EMPLOYEENAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.EMAIL + "</b> </td>");
                                    sb.AppendLine("</tr>");


                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>FIRST HOLDER KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SECOND HOLDER KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>THIRD HOLDER KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>DONOR KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SCHEMES INVESTED IN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ARN NAME</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.SECOND_HOLDER_KYC_STATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.DNR_PAN_KYCSTATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.INVESTED_SCHEMES + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.AGENTNAME + "</b> </td>");
                                    sb.AppendLine("</tr>");


                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>AUM BRACKET</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>AADHARSEEDING_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>KYC_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>BANK_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>NOMINEE_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>MOBILE</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.AUM_BRACKET + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.AADHARSEEDINGFLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.KYCFLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.BANK_FLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.NOMINEEFLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.MOBILE + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("</tr>");

                                    sb.AppendLine("</table>");
                                    sb.AppendLine("</br>");
                                }

                                running_count++;
                                running_count2++;

                                if (running_count >= 3)
                                {
                                    //CommonHelper.WriteLog("running_count in running_count >= 3: " + running_count);
                                    sb.AppendLine("<table style='border:1px solid black; border-collapse: collapse;' width='100%; height:266px;'>");
                                    //sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SLNO :</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;' colspan='7'>" + kyc_data.SRNO + "</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ACNO </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>FIRST HOLDER</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SECOND HOLDER</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>THIRD HOLDER</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>DONOR NAME</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ADDRESS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>EMPLOYEE CODE</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ARN CODE</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b> " + kyc_data.ACNO + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.FIRST_HOLDER_NAME + "</b> </td>");
                                    sb.AppendLine("<span style='overflow:hidden; margin: 200px 10px 149px 200px; position: absolute; font-size: 25px; -webkit-transform: rotate(-45deg); - moz - transform: rotate(-45deg); -o-transform: rotate(-45deg); color: #dd9191; opacity: 0.5;'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");

                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.SECOND_HOLDER_NAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.THIRD_HOLDER_NAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.DONOR_NAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.EMPLOYEECODE + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.ARNCODE + "</b> </td>");
                                    sb.AppendLine("</tr>");

                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>FIRST HOLDER PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SECOND HOLDER PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>THIRD HOLDER PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>DONOR PAN</td>");
                                    //sb.AppendLine("<span style='overflow:hidden; margin: 200px 10px 149px 200px; position: absolute; font-size: 25px; -webkit-transform: rotate(-45deg); - moz - transform: rotate(-45deg); -o-transform: rotate(-45deg); color: #dd9191; opacity: 0.5;'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>GAURDIAN PAN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>EMPLOYEE NAME</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>EMAIL</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.FIRST_HOLDER_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.SECOND_HOLDER_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.THIRD_HOLDER_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.DONOR_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.GAURDIAN_PAN + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.EMPLOYEENAME + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.EMAIL + "</b> </td>");
                                    sb.AppendLine("</tr>");


                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>FIRST HOLDER KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SECOND HOLDER KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>THIRD HOLDER KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>DONOR KYC STATUS</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>SCHEMES INVESTED IN</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>ARN NAME</td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.SECOND_HOLDER_KYC_STATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.DNR_PAN_KYCSTATUS + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.INVESTED_SCHEMES + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.AGENTNAME + "</b> </td>");
                                    sb.AppendLine("</tr>");


                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>AUM BRACKET</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>AADHARSEEDING_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>KYC_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>BANK_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>NOMINEE_FLAG</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>MOBILE</td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("</tr>");
                                    sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.AUM_BRACKET + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.AADHARSEEDINGFLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.KYCFLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.BANK_FLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.NOMINEEFLAG + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'> <b>" + kyc_data.MOBILE + "</b> </td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                    sb.AppendLine("</tr>");



                                    sb.AppendLine("</table>");

                                    sb.AppendLine("</br>");





                                    //if (running_count2 % 3 == 0)
                                    //{
                                    //    CommonHelper.WriteLog("Running Count: " + running_count);
                                    //    sb.AppendLine("<p style='page-break-after: always;> &nbsp; </p>");
                                    //    sb.AppendLine("<p style = 'page-break-before: always;' > &nbsp;</ p > ");

                                    //    CommonHelper.WriteLog("Html code running count2: " + sb.ToString());
                                    //}

                                    if (running_count % 3 == 0)
                                    {
                                        page_no++;
                                        //CommonHelper.WriteLog("Running Count in : running_count % 3 == 0" + running_count);

                                        //sb.AppendLine("<center><span><b> Page No: " + page_no + "</b> </span></center>");
                                        sb.AppendLine("<p style='page-break-after: always;'>  </p>");
                                        sb.AppendLine("<p style = 'page-break-before: always;' > </p> ");

                                        //CommonHelper.WriteLog("Html code running count: " + sb.ToString());
                                    }
                                }
                            }

                        }
                        else
                        {
                            sb.AppendLine(line);
                        }
                    }
                }
            }
            // CommonHelper.WriteLog(sb.ToString());
            return sb.ToString();
        }
        #endregion;



        #region UFC Dropdown

        public List<UpdateReallocationUFC_MODEL> Get_UFC_List()
        {
            List<UpdateReallocationUFC_MODEL> ufc_list = new List<UpdateReallocationUFC_MODEL>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", UserManager.User.Code);

                    ufc_list = conn.Query<UpdateReallocationUFC_MODEL>(QueryMaster.Get_UFC_list_empwise, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in KYCAbridgedRpt> GetUFC_List() " + ufc_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in KYCAbridgedRpt> GetUFC_List() " + ex.Message);
                }
            }
            return ufc_list;
        }

        #endregion

        #region Employee Dropdown UFC wise

        public List<DDLModel> Get_Employee_List(string p_ufc_name)
        {
            List<DDLModel> emp_list = new List<DDLModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_ufc_name", p_ufc_name);

                    emp_list = conn.Query<DDLModel>(QueryMaster.Emp_list_ufcwise, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in KYCAbridgedRpt> Get_Employee_List() " + emp_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in KYCAbridgedRpt> Get_Employee_List() " + ex.Message);
                }
            }
            return emp_list;
        }

        #endregion

        public List<KYC_DataModel> GetGridDetailKYCRecordStatusAbridgedwithPagination(DataTableParams objSearch)
        {
            List<KYC_DataModel> record_Status_list = new List<KYC_DataModel>();
            int totalRecords = 0;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                //List<UpdateReallocationUFC_MODEL> ufc_list = new List<UpdateReallocationUFC_MODEL>();
                //using (OracleConnection objConn = new OracleConnection(DataAccess.DBConnectionString)
                using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
                {
                    // It is working
                    //OracleCommand objCmd = new OracleCommand("get_data_acnowise", objConn);
                    //objCmd.CommandType = CommandType.StoredProcedure;
                    //objCmd.Parameters.Add("p_zone", OracleType.VarChar).Value = "EAST";
                    //objCmd.Parameters.Add("get_all", OracleType.Cursor).Direction = ParameterDirection.Output;

                    // It is working
                    OracleCommand objCmd = new OracleCommand("YourProcedureName", objConn);
                    //OracleCommand objCmd = new OracleCommand("USP_SEARCH_EKYC_ABRIDGED_DATA", objConn);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.AddWithValue("p_employee_code", UserManager.User.Code);// "'"+UserManager.User.Code+"'");
                    objCmd.Parameters.AddWithValue("p_search_text", objSearch.searchText == null ? "" : objSearch.searchText);
                    objCmd.Parameters.AddWithValue("p_page_number", (objSearch.start + 1));
                    objCmd.Parameters.AddWithValue("p_page_size", objSearch.length);
                    objCmd.Parameters.AddWithValue("p_sort_column", objSearch.sortColumn);
                    objCmd.Parameters.AddWithValue("p_sort_direction", objSearch.sortDirection);
                    objCmd.Parameters.Add("p_cursor", OracleType.Cursor).Direction = ParameterDirection.Output;
                    objCmd.Parameters.Add("p_total_records", OracleType.Number).Direction = ParameterDirection.Output;

                    try
                    {
                        objConn.Open();
                        CommonHelper.WriteLog("Connection Opened");
                        OracleDataReader objReader = objCmd.ExecuteReader();


                        if (objReader.HasRows)
                        {
                            CommonHelper.WriteLog("DataReader Count");

                            int intTotalRecords = Convert.ToInt32(objCmd.Parameters["p_total_records"].Value);


                            while (objReader.Read())
                            {
                                KYC_DataModel Obj = new KYC_DataModel();
                                Obj.AUM_BRACKET = objReader["AUM_BRACKET"] != DBNull.Value ? Convert.ToString(objReader["AUM_BRACKET"]) : "";
                                Obj.KYCFLAG = objReader["KYCFLAG"] != DBNull.Value ? Convert.ToString(objReader["KYCFLAG"]) : "";
                                Obj.NOMINEEFLAG = objReader["NOMINEEFLAG"] != DBNull.Value ? Convert.ToString(objReader["NOMINEEFLAG"]) : "";
                                Obj.AADHARSEEDINGFLAG = objReader["AADHARSEEDINGFLAG"] != DBNull.Value ? Convert.ToString(objReader["AADHARSEEDINGFLAG"]) : "";
                                Obj.BANK_FLAG = objReader["BANK_FLAG"] != DBNull.Value ? Convert.ToString(objReader["BANK_FLAG"]) : "";
                                Obj.SELECTED_EMPID = objReader["SELECTED_EMPID"] != DBNull.Value ? Convert.ToString(objReader["SELECTED_EMPID"]) : "";
                                Obj.EMPLOYEENAME = objReader["EMPLOYEENAME"] != DBNull.Value ? Convert.ToString(objReader["EMPLOYEENAME"]) : "";
                                Obj.SELECTION_DATE = objReader["SELECTION_DATE"] != DBNull.Value ? Convert.ToString(objReader["SELECTION_DATE"]) : "";
                                Obj.UFC_NAME = objReader["UFC_NAME"] != DBNull.Value ? Convert.ToString(objReader["UFC_NAME"]) : "";
                                Obj.REGIONDESC = objReader["REGIONDESC"] != DBNull.Value ? Convert.ToString(objReader["REGIONDESC"]) : "";
                                Obj.ZONEDESC = objReader["ZONEDESC"] != DBNull.Value ? Convert.ToString(objReader["ZONEDESC"]) : "";
                                Obj.FOLIO_STATUS = objReader["FOLIO_STATUS"] != DBNull.Value ? Convert.ToString(objReader["FOLIO_STATUS"]) : "";
                                Obj.DAYS_OF_SELECTION = objReader["DAYS_OF_SELECTION"] != DBNull.Value ? Convert.ToString(objReader["DAYS_OF_SELECTION"]) : "";
                                Obj.ACNO = objReader["ACNO"] != DBNull.Value ? Convert.ToString(objReader["ACNO"]) : "";
                                Obj.ADDRESS_OF_FIRST_HOLDER = objReader["ADDRESS_OF_FIRST_HOLDER"] != DBNull.Value ? Convert.ToString(objReader["ADDRESS_OF_FIRST_HOLDER"]) : "";
                                Obj.FIRST_HOLDER_NAME = objReader["FIRST_HOLDER_NAME"] != DBNull.Value ? Convert.ToString(objReader["FIRST_HOLDER_NAME"]) : "";
                                Obj.ARN_CODE = objReader["ARN_CODE"] != DBNull.Value ? Convert.ToString(objReader["ARN_CODE"]) : "";
                                Obj.ARN_NAME = objReader["ARN_NAME"] != DBNull.Value ? Convert.ToString(objReader["ARN_NAME"]) : "";
                                Obj.ARN_MOBILE = objReader["ARN_MOBILE"] != DBNull.Value ? Convert.ToString(objReader["ARN_MOBILE"]) : "";
                                Obj.ARN_EMAIL = objReader["ARN_EMAIL"] != DBNull.Value ? Convert.ToString(objReader["ARN_EMAIL"]) : "";
                                Obj.SRNO = Convert.ToInt32(objReader["SERIAL_NO"]);
                                Obj.TOTAL_RECORDS = intTotalRecords;

                                record_Status_list.Add(Obj);
                            }
                            CommonHelper.WriteLog("Count of kyc ABRIDGED REPORT Using pagination: " + record_Status_list.Count());
                        }
                    }
                    catch (Exception ex)
                    {
                        CommonHelper.WriteLog("Exception found " + ex.ToString());

                    }
                    objConn.Close();
                }

            }
            return record_Status_list;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (pro.PropertyType.Name == "String")
                        {
                            pro.SetValue(obj, Convert.ToString(dr[column.ColumnName]), null);
                        }
                        else if (pro.PropertyType.Name == "Int32")
                        {
                            pro.SetValue(obj, string.IsNullOrEmpty(Convert.ToString(dr[column.ColumnName])) ? 0 : Convert.ToInt32(dr[column.ColumnName]), null);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dr[column.ColumnName])))
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }

                        }


                    }
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}