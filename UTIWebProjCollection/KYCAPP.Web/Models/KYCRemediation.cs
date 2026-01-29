using Dapper;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Text;
using iTextSharp.text.pdf;

namespace KYCAPP.Web.Models
{
    public class KYCRemediation
    {
        string con_string = string.Empty;
        public List<KYC_DataModel> GetGridDetail(string Search_Text)
        {
            List<KYC_DataModel> kyc_data_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("P_EMPCODE", UserManager.User.Code);
                    parameters.Add("P_SEARCH_TEXT", string.IsNullOrWhiteSpace(Search_Text) ? "" : Search_Text.ToUpper());
                    kyc_data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_REMEDIATION_GRID, parameters).ToList();
                    CommonHelper.WriteLog("Count of KYCRemediation> GetGridDetail () :" + kyc_data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in KYCRemediation> GetGridDetail ()" + ex.Message);
                }
            }
            return kyc_data_list;
        }

        public List<KYC_DataModel> Get_CM_GridDetail(string Search_Text, string P_AUM_BRACKET)
        {
            KYC_DataModel OBJKYC = new KYC_DataModel();


            List<KYC_DataModel> kyc_data_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    string cmd_query = string.Empty;
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    if (!string.IsNullOrWhiteSpace(Search_Text) && P_AUM_BRACKET.ToUpper() != "ALL")
                    {
                        parameters.Add("P_EMPCODE", UserManager.User.Code);
                        parameters.Add("P_SEARCH_TEXT", string.IsNullOrWhiteSpace(Search_Text) ? "" : Search_Text.ToUpper());
                        parameters.Add("P_AUM_BRACKET", string.IsNullOrWhiteSpace(P_AUM_BRACKET) ? "" : P_AUM_BRACKET);
                        cmd_query = QueryMaster.KYC_REMEDIATION_GRID_CM;
                    }
                    else
                    {
                        parameters.Add("P_EMPCODE", UserManager.User.Code);
                        if (P_AUM_BRACKET.ToUpper() == "ALL")
                        {
                            if (!string.IsNullOrWhiteSpace(Search_Text))
                            {
                                parameters.Add("P_SEARCH_TEXT", string.IsNullOrWhiteSpace(Search_Text) ? "" : Search_Text.ToUpper());
                                cmd_query = QueryMaster.KYC_REMEDIATION_GRID_CM__AUMBRCKT_ALL_And_Search_Text;
                                CommonHelper.WriteLog("P_AUM_BRACKET and SearchText value are: " + P_AUM_BRACKET + "" + Search_Text);
                            }
                            else
                            {
                                cmd_query = QueryMaster.KYC_REMEDIATION_GRID_CM__AUMBRCKT_ALL;
                                CommonHelper.WriteLog("P_AUM_BRACKET  value is: " + P_AUM_BRACKET);

                            }


                        }
                        else
                        {

                            parameters.Add("P_AUM_BRACKET", string.IsNullOrWhiteSpace(P_AUM_BRACKET) ? "" : P_AUM_BRACKET);
                            cmd_query = QueryMaster.KYC_REMEDIATION_GRID_CM__AUMBRCKT;
                        }
                    }



                    kyc_data_list = conn.Query<KYC_DataModel>(cmd_query, parameters).ToList();

                    CommonHelper.WriteLog("Data Count in KYCRemediationCM> Get_CM_GridDetail ()" + kyc_data_list.Count);


                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in KYCRemediationCM> Get_CM_GridDetail ()" + ex.Message);
                }
            }
            return kyc_data_list;
        }

        public List<DDLModel> GetEmpCodeName()
        {
            List<DDLModel> kyc_data_list = new List<DDLModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("P_EMPCODE", UserManager.User.Code);

                    kyc_data_list = conn.Query<DDLModel>(QueryMaster.GetEmpcodeName, parameters).ToList();
                    // CommonHelper.WriteLog("Data Count in KYCRemediationCM> GetEmpCodeName ()" + kyc_data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in KYCRemediationCM> GetEmpCodeName ()" + ex.Message);
                }
            }
            return kyc_data_list;
        }


        public ResponseModel Save(List<KYC_DataModel> model_list)
        {
            int monthly_update_limit = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Monthly_Update_Limits"));
            ResponseModel res = new ResponseModel();
            List<KYC_DataModel> kyc_data = new List<KYC_DataModel>();
            string Response = string.Empty;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                conn.Open();

                DateTime now = DateTime.Now;
                DateTime from_date = new DateTime(now.Year, now.Month, 1);
                DateTime to_date = from_date.AddMonths(1).AddDays(-1);


                DynamicParameters para = new DynamicParameters();
                para.Add("p_employee_code", UserManager.User.Code);
                para.Add("p_from_date", from_date.ToString("dd-MMM-yyyy"));
                para.Add("p_to_date", to_date.ToString("dd-MMM-yyyy"));
                int monnthly_updates = conn.Query<int>(QueryMaster.KYC_DATA_LIMIT_CHECK_QUERY_FOR_UPDATE, para).FirstOrDefault();

                CommonHelper.WriteLog("employee code :" + UserManager.User.Code + " | current update count :" + monnthly_updates);

                if ((monnthly_updates + model_list.Count(n => n.SELECT_STATUS == true)) > monthly_update_limit && (monnthly_updates < monthly_update_limit))
                {
                    int remaining_count = monthly_update_limit - monnthly_updates;
                    res.msg = "Warning \n You can not update more than " + monthly_update_limit + " for a month & your remaining count is : " + remaining_count;
                    res.is_success = false;
                    return res;
                }
                CommonHelper.WriteLog("Monthly updates: " + monnthly_updates);
                CommonHelper.WriteLog("Monthly update limit: " + monthly_update_limit);

                if (monnthly_updates >= monthly_update_limit)
                {
                    res.msg = "Warning \n You can not update more than " + monthly_update_limit + " for a month !";
                    res.is_success = false;
                    return res;
                }

                OracleTransaction trans = conn.BeginTransaction();
                try
                {

                    CommonHelper.WriteLog("Count of selected Rows in KYCRemediation: " + model_list.Count);

                    foreach (KYC_DataModel model in model_list)
                    {

                        if (model.SELECT_STATUS)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("p_employee_code", UserManager.User.Code);
                            parameters.Add("p_SRNO", model.SRNO);
                            conn.Query(QueryMaster.KYC_REMEDIATION_UPDATE_CMD, parameters, trans);
                            CommonHelper.WriteLog("Selected_sr_NO : " + model.SRNO);
                        }
                    }
                    trans.Commit();
                    res.msg = "Data updated successfully!";
                    res.is_success = true;
                }
                catch (Exception ex)
                {
                    res.msg = "Something went wrong ,Please try after sometime !";
                    CommonHelper.WriteLog("error in KYCRemedition> Save() " + ex.Message);
                    trans.Rollback();
                }
            }
            return res;
        }

        public ResponseModel SaveRemediationCM(List<KYC_DataModel> model_list, string selected_employee_code)
        {
            CommonHelper.WriteLog("| selected_employee_code :" + selected_employee_code + "LOGIN EMPLOYEE CODE : " + UserManager.User.Code);
            ResponseModel res = new ResponseModel();

            List<KYC_DataModel> kyc_data = new List<KYC_DataModel>();
            string Response = string.Empty;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                conn.Open();

                OracleTransaction trans = conn.BeginTransaction();
                try
                {
                    CommonHelper.WriteLog("Count of selected Rows in KYCRemediationCM: " + model_list.Count);

                    foreach (KYC_DataModel model in model_list)
                    {

                        if (model.SELECT_STATUS)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("P_LOGIN_EMPCODE", UserManager.User.Code);
                            parameters.Add("p_employee_code", selected_employee_code);
                            parameters.Add("p_SRNO", model.SRNO);
                            conn.Query(QueryMaster.KYC_REMEDIATION_CM_UPDATE_CMD, parameters, trans);
                            CommonHelper.WriteLog("data updated Sr no :" + model.SRNO + " | selected_employee_code :" + selected_employee_code + "LOGIN EMPLOYEE CODE : " + UserManager.User.Code);
                        }
                    }
                    trans.Commit();
                    res.msg = "Data updated successfully!";
                    res.is_success = true;
                }
                catch (Exception ex)
                {
                    res.msg = "Something went wrong ,Please try after sometime !";
                    CommonHelper.WriteLog("error in KYCRemediationCM> SaveRemediationCM()" + ex.Message);
                    trans.Rollback();
                }
            }
            return res;
        }

        public static byte[] PdfTempNocopy(string temp)
        {
            // Create a PDF document using iTextSharp
            byte[] pdfBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Get the PDF bytes from the HTML content generated by wkhtmltopdf
                pdfBytes = Convert2(temp);

                // Set permissions on the existing PDF document using iTextSharp
                PdfReader reader = new PdfReader(pdfBytes);
                using (MemoryStream outputMemoryStream = new MemoryStream())
                {

                    int permissions = ~(PdfWriter.ALLOW_COPY | PdfWriter.ALLOW_MODIFY_CONTENTS | PdfWriter.ALLOW_MODIFY_ANNOTATIONS);
                    PdfStamper stamper = new PdfStamper(reader, outputMemoryStream);
                    stamper.SetEncryption(null, null, permissions, PdfWriter.ENCRYPTION_AES_256);
                    //stamper.SetEncryption(null, null,PdfWriter.AllowPrinting | PdfWriter.AllowCopy, PdfWriter.STRENGTH40BITS);
                    stamper.Close();

                    // Get the modified PDF bytes with permissions
                    pdfBytes = outputMemoryStream.ToArray();
                }
            }
            return pdfBytes;
        }
        public static byte[] Convert2(string source)
        {
            try
            {
                Process p;
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = ConfigurationManager.AppSettings["wkhtml"];

                // run the conversion utility
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                // note: that we tell wkhtmltopdf to be quiet and not run scripts
                string args = "-q -n ";
                args += "--disable-smart-shrinking ";
                args += "";
                args += "--outline-depth 0 ";
                args += "--page-size A4 ";
                args += " - -";
                //args += "--no-copy -";

                psi.Arguments = args;
                CommonHelper.WriteLog("process start");
                p = Process.Start(psi);

                try
                {
                    using (StreamWriter stdin = p.StandardInput)
                    {
                        stdin.AutoFlush = true;
                        stdin.Write(source);
                    }

                    //read output
                    byte[] buffer = new byte[32768];
                    byte[] file;
                    using (var ms = new MemoryStream())
                    {
                        while (true)
                        {
                            int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);
                            if (read <= 0)
                                break;
                            ms.Write(buffer, 0, read);
                        }
                        file = ms.ToArray();
                    }

                    p.StandardOutput.Close();
                    // wait or exit
                    p.WaitForExit(60000);

                    // read the exit code, close process
                    int returnCode = p.ExitCode;
                    p.Close();
                    CommonHelper.WriteLog("Convert2 ():");
                    return file;
                    //if (returnCode == 1)
                    //    return file;
                    //else
                    //  CommonHelper.write_log("Could not create PDF, returnCode:" + returnCode);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Could not create PDF :-" + ex.Message);
                }
                finally
                {
                    p.Close();
                    p.Dispose();
                }
            }
            catch (Exception exx2)
            {

                CommonHelper.WriteLog("error in :" + exx2.Message);
            }
            return null;
        }
        public static string GetHtmlString(DateTime from_date, DateTime to_date, string Search_Text)
        {
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
                    parameters.Add("P_EMPID", UserManager.User.Code);
                    parameters.Add("p_from_date", from_date.ToString("dd-MMM-yyyy"));
                    parameters.Add("p_to_date", to_date.ToString("dd-MMM-yyyy"));
                    if (!string.IsNullOrWhiteSpace(Search_Text))
                    {
                        parameters.Add("p_search_text", Search_Text.ToUpper());
                    }
                    else
                    {
                        parameters.Add("p_search_text", Search_Text);
                    }

                    data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_DATA_DOWNLOAD_QUERY, parameters).ToList();
                    CommonHelper.WriteLog("total record found GetHtmlString() : " + data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("KYCRemediation >GetHtmlString() : " + ex.Message);
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
                            sb.AppendLine(line);
                            foreach (KYC_DataModel kyc_data in data_list)
                            {
                                sb.AppendLine("<table style='border:1px solid black; border-collapse: collapse;' width='100%'>");
                                //sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SLNO :</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;' colspan='7'>" + kyc_data.SRNO + "</td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ACNO</b> </td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR NAME</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ADDRESS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMPLOYEE CODE</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ARN CODE</b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ACNO + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_NAME + "</td>");
                                sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");

                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_NAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_NAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DONOR_NAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMPLOYEECODE + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ARNCODE + "</td>");
                                sb.AppendLine("</tr>");

                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR PAN</b></td>");
                                //sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>GAURDIAN PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMPLOYEE NAME</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMAIL</b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DONOR_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.GAURDIAN_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMPLOYEENAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMAIL + "</td>");
                                sb.AppendLine("</tr>");


                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SCHEMES INVESTED IN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ARN NAME</b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_KYC_STATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DNR_PAN_KYCSTATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.INVESTED_SCHEMES + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AGENTNAME + "</td>");
                                sb.AppendLine("</tr>");


                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>AUM BRACKET</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>AADHARSEEDING_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>KYC_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>BANK_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>NOMINEE_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>MOBILE</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AUM_BRACKET + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AADHARSEEDINGFLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.KYCFLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.BANK_FLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.NOMINEEFLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.MOBILE + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("</tr>");



                                sb.AppendLine(" </table>");
                                sb.AppendLine("</br>");






                                //sb.AppendLine("<table border='1' style='border - spacing: 0px 0px;'>");
                                //sb.AppendLine("<tr><td></td><td>EMPLOYEE CODE</td>" + data_list[0].SELECTED_EMPID + "<td></td><td></td><td>EMPLOYEE NAME</td><td>" + UserManager.User.Name + "</td><td>LOCATION </td><td>Corporate Office</td></tr>");
                                //sb.AppendLine("<br>");
                                //sb.AppendLine("<tr style='border: 0; '><td>SLNO :</td><td>SRNO (" + kyc_data.SRNO + ")</td></tr>");
                                //sb.AppendLine("<tr style='border:0;'><td>SLNO:</td><td>SRNO(1)</td></tr><tr><td>ACNO</td><td>FIRSTHOLDER</td><td>SECONDHOLDER</td><td>THIRDHOLDER</td><td>DONORNAME</td><td>ADDRESSOFFIRSTHOLDER</td><td>AUM(inRs)</td><td>ARNCODE</td></tr>");
                                //sb.AppendLine("<tr><td>" + kyc_data.ACNO + "</td><td>" + kyc_data.FIRST_HOLDER_NAME + "</td><td>" + kyc_data.SECOND_HOLDER_NAME + "</td><td>" + kyc_data.THIRD_HOLDER_NAME + "</td><td></td><td>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</td><td>" + kyc_data.AUM_OF_THE_FOLIO + "/td><td></td></tr>");
                                //sb.AppendLine("<tr><td></td><td>FIRST_HOLDER_KYC_STATUS</td><td>SECOND_HOLDER_KYC_STATUS</td><td>THIRD_HOLDER_KYC_STATUS</td><td>DONOR_KYC_STATUS</td><td>SCHEMES_INVESTED_IN</td><td></td><td>ARN_NAME</td></tr>");
                                //sb.AppendLine("<tr><td></td><td>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</td><td> " + kyc_data.SECOND_HOLDER_KYC_STATUS + "</td><td>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</td><td>" + kyc_data.DONOR_KYC_STATUS + "</td><td>" + kyc_data.INVESTED_SCHEMES + "</td><td></td><td>" + kyc_data.ARNCODE + "</td></tr>");
                                //sb.AppendLine("</table>");
                                running_count++;
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

        public static string GetHtmlStringCM(DateTime from_date, DateTime to_date, string Search_Text)
        {
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
                    parameters.Add("P_EMPID", UserManager.User.Code);
                    parameters.Add("p_from_date", from_date);
                    parameters.Add("p_to_date", to_date);
                    parameters.Add("P_search_Text", !string.IsNullOrWhiteSpace(Search_Text) ? Search_Text.ToUpper() : Search_Text);



                    data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_DATA_DOWNLOAD_QUERY_CM, parameters).ToList();
                    CommonHelper.WriteLog("total record found  GetHtmlStringCM(): " + data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("KYCRemediationCM >GetHtmlStringCM() :" + ex.Message);
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
                            sb.AppendLine(line);
                            foreach (KYC_DataModel kyc_data in data_list)
                            {

                                //sb.AppendLine("<div style='background: url('~/images/logo.jpg') center center no-repeat; opacity:0.1; position:absolute; width:100%; height:100%; '>");
                                sb.AppendLine("<table style='border:1px solid black; border-collapse: collapse;' width='100%'>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SLNO :</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;' colspan='7'>" + kyc_data.SRNO + "</td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ACNO</b> </td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER</b></td>");
                                sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(200deg); color: #dd9191; opacity: 0.5'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR NAME</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ADDRESS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMPLOYEE CODE</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ARN CODE</b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ACNO + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_NAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_NAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_NAME + "</td>");
                                sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DONOR_NAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMPLOYEECODE + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ARNCODE + "</td>");
                                sb.AppendLine("</tr>");

                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>GAURDIAN PAN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMPLOYEE NAME</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMAIL</b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_PAN + "</td>");
                                sb.AppendLine("<span style='margin: 3rem; position: absolute; font-size: 25px; transform: rotate(322deg); color: #dd9191; opacity: 0.6'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DONOR_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.GAURDIAN_PAN + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMPLOYEENAME + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMAIL + "</td>");
                                sb.AppendLine("</tr>");


                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR KYC STATUS</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SCHEMES INVESTED IN</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ARN NAME</b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_KYC_STATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DNR_PAN_KYCSTATUS + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.INVESTED_SCHEMES + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AGENTNAME + "</td>");
                                sb.AppendLine("</tr>");

                                // added on 12_APR_2023
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>AUM BRACKET</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>AADHARSEEDING_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>KYC_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>BANK_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>NOMINEE_FLAG</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>MOBILE</b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
                                sb.AppendLine("</tr>");
                                sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AUM_BRACKET + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AADHARSEEDINGFLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.KYCFLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.BANK_FLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.NOMINEEFLAG + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.MOBILE + "</td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
                                sb.AppendLine("</tr>");



                                sb.AppendLine(" </table>");
                                //sb.AppendLine("</div>");
                                sb.AppendLine("</br>");






                                //sb.AppendLine("<table border='1' style='border - spacing: 0px 0px;'>");
                                //sb.AppendLine("<tr><td></td><td>EMPLOYEE CODE</td>" + data_list[0].SELECTED_EMPID + "<td></td><td></td><td>EMPLOYEE NAME</td><td>" + UserManager.User.Name + "</td><td>LOCATION </td><td>Corporate Office</td></tr>");
                                //sb.AppendLine("<br>");
                                //sb.AppendLine("<tr style='border: 0; '><td>SLNO :</td><td>SRNO (" + kyc_data.SRNO + ")</td></tr>");
                                //sb.AppendLine("<tr style='border:0;'><td>SLNO:</td><td>SRNO(1)</td></tr><tr><td>ACNO</td><td>FIRSTHOLDER</td><td>SECONDHOLDER</td><td>THIRDHOLDER</td><td>DONORNAME</td><td>ADDRESSOFFIRSTHOLDER</td><td>AUM(inRs)</td><td>ARNCODE</td></tr>");
                                //sb.AppendLine("<tr><td>" + kyc_data.ACNO + "</td><td>" + kyc_data.FIRST_HOLDER_NAME + "</td><td>" + kyc_data.SECOND_HOLDER_NAME + "</td><td>" + kyc_data.THIRD_HOLDER_NAME + "</td><td></td><td>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</td><td>" + kyc_data.AUM_OF_THE_FOLIO + "/td><td></td></tr>");
                                //sb.AppendLine("<tr><td></td><td>FIRST_HOLDER_KYC_STATUS</td><td>SECOND_HOLDER_KYC_STATUS</td><td>THIRD_HOLDER_KYC_STATUS</td><td>DONOR_KYC_STATUS</td><td>SCHEMES_INVESTED_IN</td><td></td><td>ARN_NAME</td></tr>");
                                //sb.AppendLine("<tr><td></td><td>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</td><td> " + kyc_data.SECOND_HOLDER_KYC_STATUS + "</td><td>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</td><td>" + kyc_data.DONOR_KYC_STATUS + "</td><td>" + kyc_data.INVESTED_SCHEMES + "</td><td></td><td>" + kyc_data.ARNCODE + "</td></tr>");
                                //sb.AppendLine("</table>");
                                running_count++;
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




        //public static string GetHtmlStringAbridged_Report(List<KYC_DataModel> model_list)
        //{
        //    //List<KYC_DataModel> data_list = new List<KYC_DataModel>();
        //    StringBuilder sb = new StringBuilder();

        //    string Html_Template_Dir = ConfigurationManager.AppSettings.Get("html_template_location");
        //    string htmlstr = string.Empty;


        //    if (model_list.Count > 0)
        //    {
        //        string line = string.Empty;

        //        using (StreamReader sr = new StreamReader(Html_Template_Dir + "\\" + "KYC_Processed_data.html"))
        //        {



        //            while ((line = sr.ReadLine()) != null && model_list.Count > 0)
        //            {
        //                line = line.Replace("#{emp_code}", UserManager.User.Code);
        //                line = line.Replace("#{emp_name}", UserManager.User.Name);
        //                line = line.Replace("#{location}", string.IsNullOrWhiteSpace(model_list[0].LOCATION) ? "" : model_list[0].LOCATION);


        //                CommonHelper.WriteLog("line :" + line);
        //                if (line.Trim() == "<br />")
        //                {
        //                    int running_count = 0;
        //                    sb.AppendLine(line);
        //                    foreach (KYC_DataModel kyc_data in model_list)
        //                    {

        //                        //sb.AppendLine("<div style='background: url('~/images/logo.jpg') center center no-repeat; opacity:0.1; position:absolute; width:100%; height:100%; '>");
        //                        sb.AppendLine("<table style='border:1px solid black; border-collapse: collapse;' width='100%'>");
        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SLNO :</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;' colspan='7'>" + kyc_data.SRNO + "</td>");
        //                        sb.AppendLine("</tr>");
        //                        sb.AppendLine("<tr>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ACNO</b> </td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER</b></td>");
        //                        //sb.AppendLine("<span style='margin: 3rem; transform-origin: 0 0; position: absolute; font-size: 25px; transform: rotate(-45deg); color: #dd9191; opacity: 0.2'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR NAME</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ADDRESS</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMPLOYEE CODE</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ARN CODE</b></td>");
        //                        sb.AppendLine("</tr>");
        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ACNO + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_NAME + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_NAME + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_NAME + "</td>");
        //                        //sb.AppendLine("<span style='margin: 200px 10px 149px 200px; position: absolute; font-size: 25px; -webkit-transform: rotate(-45deg); - moz - transform: rotate(-45deg); -o-transform: rotate(-45deg); color: #dd9191; opacity: 0.5;'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DONOR_NAME + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SELECTED_EMPID + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ARN_CODE + "</td>");
        //                        sb.AppendLine("</tr>");

        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER PAN</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER PAN</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER PAN</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR PAN</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>GAURDIAN PAN</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMPLOYEE NAME</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>EMAIL</b></td>");
        //                        sb.AppendLine("</tr>");
        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_PAN + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_PAN + "</td>");
        //                        sb.AppendLine("<span style='margin: 200px 10px 149px 200px; position: absolute; font-size: 25px; -webkit-transform: rotate(-45deg); - moz - transform: rotate(-45deg); -o-transform: rotate(-45deg); color: #dd9191; opacity: 0.5;'>" + UserManager.User.Code + " " + UserManager.User.Name + "</span>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_PAN + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DONOR_PAN + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.GAURDIAN_PAN + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.EMPLOYEENAME + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ARN_EMAIL + "</td>");
        //                        sb.AppendLine("</tr>");

        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>FIRST HOLDER KYC STATUS</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SECOND HOLDER KYC STATUS</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>THIRD HOLDER KYC STATUS</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>DONOR KYC STATUS</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>SCHEMES INVESTED IN</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>ARN NAME</b></td>");
        //                        sb.AppendLine("</tr>");
        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.SECOND_HOLDER_KYC_STATUS + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.DNR_PAN_KYCSTATUS + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.INVESTED_SCHEMES + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ARN_NAME + "</td>");
        //                        sb.AppendLine("</tr>");

        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>AUM BRACKET</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>AADHARSEEDING_FLAG</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>KYC_FLAG</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>BANK_FLAG</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>NOMINEE_FLAG</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b>MOBILE</b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
        //                        //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'><b></b></td>");
        //                        sb.AppendLine("</tr>");
        //                        sb.AppendLine("<tr style='border:1px solid black; border-collapse: collapse;'>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AUM_BRACKET + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.AADHARSEEDINGFLAG + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.KYCFLAG + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.BANK_FLAG + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.NOMINEEFLAG + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'>" + kyc_data.ARN_MOBILE + "</td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        //sb.AppendLine("<td style='border:1px solid black; border-collapse: collapse;'></td>");
        //                        sb.AppendLine("</tr>");



        //                        sb.AppendLine(" </table>");
        //                        //sb.AppendLine("</div>");
        //                        sb.AppendLine("</br>");






        //                        //sb.AppendLine("<table border='1' style='border - spacing: 0px 0px;'>");
        //                        //sb.AppendLine("<tr><td></td><td>EMPLOYEE CODE</td>" + data_list[0].SELECTED_EMPID + "<td></td><td></td><td>EMPLOYEE NAME</td><td>" + UserManager.User.Name + "</td><td>LOCATION </td><td>Corporate Office</td></tr>");
        //                        //sb.AppendLine("<br>");
        //                        //sb.AppendLine("<tr style='border: 0; '><td>SLNO :</td><td>SRNO (" + kyc_data.SRNO + ")</td></tr>");
        //                        //sb.AppendLine("<tr style='border:0;'><td>SLNO:</td><td>SRNO(1)</td></tr><tr><td>ACNO</td><td>FIRSTHOLDER</td><td>SECONDHOLDER</td><td>THIRDHOLDER</td><td>DONORNAME</td><td>ADDRESSOFFIRSTHOLDER</td><td>AUM(inRs)</td><td>ARNCODE</td></tr>");
        //                        //sb.AppendLine("<tr><td>" + kyc_data.ACNO + "</td><td>" + kyc_data.FIRST_HOLDER_NAME + "</td><td>" + kyc_data.SECOND_HOLDER_NAME + "</td><td>" + kyc_data.THIRD_HOLDER_NAME + "</td><td></td><td>" + kyc_data.ADDRESS_OF_FIRST_HOLDER + "</td><td>" + kyc_data.AUM_OF_THE_FOLIO + "/td><td></td></tr>");
        //                        //sb.AppendLine("<tr><td></td><td>FIRST_HOLDER_KYC_STATUS</td><td>SECOND_HOLDER_KYC_STATUS</td><td>THIRD_HOLDER_KYC_STATUS</td><td>DONOR_KYC_STATUS</td><td>SCHEMES_INVESTED_IN</td><td></td><td>ARN_NAME</td></tr>");
        //                        //sb.AppendLine("<tr><td></td><td>" + kyc_data.FIRST_HOLDER_KYC_STATUS + "</td><td> " + kyc_data.SECOND_HOLDER_KYC_STATUS + "</td><td>" + kyc_data.THIRD_HOLDER_KYC_STATUS + "</td><td>" + kyc_data.DONOR_KYC_STATUS + "</td><td>" + kyc_data.INVESTED_SCHEMES + "</td><td></td><td>" + kyc_data.ARNCODE + "</td></tr>");
        //                        //sb.AppendLine("</table>");
        //                        running_count++;
        //                    }
        //                }
        //                else
        //                {
        //                    sb.AppendLine(line);
        //                }
        //            }
        //        }
        //    }
        //    // CommonHelper.WriteLog(sb.ToString());
        //    return sb.ToString();
        //}




    }


    public class UpdateStatusFolio
    {
        public List<KYC_DataModel> GetGridDetailStatusFolio(DateTime P_Selected_dt, string P_ACNO, string P_INVNAME)
        {
            List<KYC_DataModel> Status_folio_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    string cmd_query = string.Empty;
                    DynamicParameters parameters = new DynamicParameters();


                    parameters.Add("p_employee_code", UserManager.User.Code);
                    parameters.Add("P_DATE", P_Selected_dt.ToString("dd-MMM-yyyy"));
                    parameters.Add("P_ACNO", P_ACNO);
                    if (!string.IsNullOrWhiteSpace(P_INVNAME))
                    {
                        parameters.Add("P_INVNAME", P_INVNAME.ToUpper());
                    }
                    else
                    {
                        parameters.Add("P_INVNAME", P_INVNAME);

                    }

                    //if (P_ACNO != null || P_INVNAME != null)
                    //{
                    //    if (P_ACNO != null && P_INVNAME != null)
                    //    {
                    //        parameters.Add("P_ACNO", string.IsNullOrWhiteSpace(P_ACNO) ? "" : P_ACNO.ToUpper());
                    //        parameters.Add("P_INVNAME", string.IsNullOrWhiteSpace(P_INVNAME) ? "" : P_INVNAME.ToUpper());
                    //        CommonHelper.WriteLog("THIS IS ACNO AND INVNAME CONDITION: " + P_ACNO + " | INVNAME: " + P_INVNAME);
                    //        cmd_query = QueryMaster.UpdateStatusFolio_Grid;
                    //    }
                    //    else
                    //    if (P_ACNO != null)
                    //    {
                    //        parameters.Add("P_ACNO", string.IsNullOrWhiteSpace(P_ACNO) ? "" : P_ACNO.ToUpper());
                    //        cmd_query = QueryMaster.UpdateStatusFolio_Grid__ACNO;

                    //        CommonHelper.WriteLog("THIS IS ACNO CONDITION: " + P_ACNO);

                    //    }
                    //    else
                    //    {
                    //        parameters.Add("P_INVNAME", string.IsNullOrWhiteSpace(P_INVNAME) ? "" : P_INVNAME.ToUpper());
                    //        CommonHelper.WriteLog("THIS IS INVNAME ELSE CONDITION: " + P_INVNAME);
                    //        cmd_query = QueryMaster.UpdateStatusFolio_Grid__INVNAME;
                    //    }
                    //}
                    //else
                    //{
                    //    cmd_query = QueryMaster.UpdateStatusFolio_Grid__date_and_Emp_code;
                    //    CommonHelper.WriteLog("THIS IS MAIN ELSE CONDITION : SELECTED DATE: " + P_Selected_dt);

                    //}
                    cmd_query = QueryMaster.UpdateStatusFolio_Grid;

                    Status_folio_list = conn.Query<KYC_DataModel>(cmd_query, parameters).ToList();


                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendLine(QueryMaster.UpdateStatusFolio_Grid);

                    //sb.AppendLine("nvl(a.selected_rec, 'N') = 'S'");
                    //sb.AppendLine("and a.selected_empid ='" + UserManager.User.Code + "'");
                    //sb.AppendLine("and a.selected_dt ='" + P_Selected_dt.ToString("dd-MMM-yyyy") + "'");
                    //if (!string.IsNullOrWhiteSpace(P_ACNO))
                    //{
                    //    sb.AppendLine("and upper(Trim(a.ACNO))='" + P_ACNO.ToUpper() + "'");
                    //}
                    //if (!string.IsNullOrWhiteSpace(P_INVNAME))
                    //{
                    //    sb.AppendLine("and upper(Trim(a.INVNAME))='" + P_INVNAME.ToUpper() + "'");
                    //}


                    //CommonHelper.WriteLog("query of Status Folio Page: \n" + sb.ToString());
                    //Status_folio_list = conn.Query<KYC_DataModel>(sb.ToString()).ToList();




                    CommonHelper.WriteLog("Count of UpdateStatusFolio> GetGridDetailStatusFolio () :" + Status_folio_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in UpdateStatusFolio> GetGridDetailStatusFolio ()" + ex.Message);
                }
            }
            return Status_folio_list;
        }



        public ResponseModel SaveUpdateStatusFolio(KYC_DataModel model)
        {
            //P_SLNO = REMARK


            ResponseModel res = new ResponseModel();

            List<KYC_DataModel> kyc_data = new List<KYC_DataModel>();
            string Response = string.Empty;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                conn.Open();
                try
                {


                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("P_REMARK_CODE", model.REMARK_CODE);
                    parameters.Add("P_REMARK_DATE", model.REMARK_DATE);
                    parameters.Add("P_REMARK_COMMENT", model.REMARK_COMMENT);
                    parameters.Add("P_SRNO", model.SRNO);
                    conn.Execute(QueryMaster.Update_QUERY_StatusFolio, parameters); // uncommented
                    CommonHelper.WriteLog("data updated in SaveUpdateStatusFolio() : | selected Remark_date : " + model.REMARK_DATE + " Selected SRNO : " + model.SRNO + " Selected Remark_CODE : " + model.REMARK_CODE + "P_REMARK_COMMENT: " + model.REMARK_COMMENT);

                    res.msg = "Data updated successfully!";
                    res.is_success = true;
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in UpdateStatusFolio> SaveUpdateStatusFolio()" + ex.Message);
                    res.msg = "Something went wrong ,Please try after sometime !";
                }
            }
            return res;
        }

        public List<DDLModel> GetRemarkList()
        {
            List<DDLModel> Remark_data_list = new List<DDLModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    Remark_data_list = conn.Query<DDLModel>(QueryMaster.GetRemarkListQuery, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in UpdateStatusFolio> GetRemarkList ()" + Remark_data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in UpdateStatusFolio> GetRemarkList ()" + ex.Message);
                }
            }
            return Remark_data_list;
        }


    }
}