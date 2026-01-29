using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using Dapper;
using System.Text;
using KYCAPP.Web.CustomHelper;

namespace KYCAPP.Web.Models
{
    public class ReallocationUFCReport
    {
        public List<KYC_DataModel> GetGridDetailReallocation(string Search_Text, string p_aum_bracket)
        {
            List<KYC_DataModel> Reallocation_data = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("P_EMPCODE", UserManager.User.Code);
                    parameters.Add("P_SEARCH_TEXT", string.IsNullOrWhiteSpace(Search_Text) ? "" : Search_Text.ToUpper());
                    // parameters.Add("P_AUM_BRACKET", p_aum_bracket);
                    Reallocation_data = conn.Query<KYC_DataModel>(QueryMaster.GetRealloactionGrid_Query, parameters).ToList();
                    CommonHelper.WriteLog("Count of ReallocationUFCReport> GetGridDetailReallocation () : " + Reallocation_data.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReallocationUFCReport> GetGridDetailReallocation ()" + ex.Message);
                }
            }
            return Reallocation_data;
        }


        public List<DDLModel> GetZone_List()
        {
            List<DDLModel> zone_list = new List<DDLModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    string query = "SELECT distinct ZONE_UTI FROM mistest.KYC_REMED_UFC_NAME";

                    zone_list = conn.Query<DDLModel>(query, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in ReallocationUFCReport> GetZone_List() " + zone_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReallocationUFCReport> GetZone_List() " + ex.Message);
                }
            }
            return zone_list;
        }

        public List<DDLModel> GetRegion_List(string p_zone)
        {
            List<DDLModel> region_list = new List<DDLModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    string query = "SELECT DISTINCT REGION_NAME_UTI FROM mistest.KYC_REMED_UFC_NAME WHERE ZONE_UTI = '" + p_zone + "'";

                    region_list = conn.Query<DDLModel>(query, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in ReallocationUFCReport> GetRegion_List() " + region_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReallocationUFCReport> GetRegion_List() " + ex.Message);
                }
            }
            return region_list;
        }
        public List<UpdateReallocationUFC_MODEL> GetUFC_List_reg_Zonewise(string p_zone, string p_region)
        {
            List<UpdateReallocationUFC_MODEL> ufc_list = new List<UpdateReallocationUFC_MODEL>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    string query = "SELECT * FROM mistest.KYC_REMED_UFC_NAME WHERE ZONE_UTI = '" + p_zone + "' AND REGION_NAME_UTI = '" + p_region + "'";

                    ufc_list = conn.Query<UpdateReallocationUFC_MODEL>(query, parameters).ToList();
                    CommonHelper.WriteLog("Data Count in ReallocationUFCReport> GetUFC_List() " + ufc_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReallocationUFCReport> GetUFC_List() " + ex.Message);
                }
            }
            return ufc_list;
        }


        public ResponseModel Save(UpdateReallocationUFC_MODEL model, string selected_employee_code, string selected_email_id)
        {
            MAIL_DATA obj1 = new MAIL_DATA();
            ResponseModel res = new ResponseModel();

            List<UpdateReallocationUFC_MODEL> kyc_data = new List<UpdateReallocationUFC_MODEL>();
            string Response = string.Empty;
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                conn.Open();


                try
                {
                    StringBuilder sb = new StringBuilder();
                    EmailHelper obj = new EmailHelper();
                    if (string.IsNullOrWhiteSpace(selected_employee_code) && string.IsNullOrWhiteSpace(selected_email_id))
                    {

                        var strSRNO = String.Join(",", model.SRNO.Select(s => $"'{s.Replace("'", "''")}'"));

                        sb.Append(QueryMaster.Update_Reallocation_diff_UFC);
                        sb.AppendLine(" where b.zone_uti='" + model.ZONE_UTI + "' and b.region_name_uti='" + model.REGION_NAME_UTI + "' and b.ufc_name_dnq='" + model.UFC_NAME + "') where a.srno in (" + strSRNO + ")");
                        CommonHelper.WriteLogUpdate("Update Query of Different UFC: " + sb.ToString());
                        conn.Execute(sb.ToString());
                        obj.SendMail(model.MAIL_DATA, model.EMAIL_ID, model.FromEmailId);

                    }
                    else
                    {
                        var strSRNO = String.Join(",", model.SRNO.Select(s => $"'{s.Replace("'", "''")}'"));
                        sb.Append(QueryMaster.Update_reallocation_same_ufc);
                        sb.AppendLine("selected_empid='" + selected_employee_code + "', SUPVERVISOR_ID='" + UserManager.User.Code + "' where SRNO in (" + strSRNO + ")");
                        CommonHelper.WriteLogUpdate("Update Query of same UFC: " + sb.ToString());
                        conn.Execute(sb.ToString());
                        obj.SendMail_SameUFC(model.MAIL_DATA, selected_email_id, model.FromEmailId);
                    }


                    res.msg = "Data updated successfully!";
                    res.is_success = true;
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReallocationUFCReport> Save() " + ex.Message);
                    res.msg = "Something went wrong, Please try after sometime!";
                }
            }
            return res;
        }

    }
}