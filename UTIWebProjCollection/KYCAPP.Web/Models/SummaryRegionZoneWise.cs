using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;

namespace KYCAPP.Web.Models
{
    public class SummaryRegionZoneWise
    {
        public List<SummaryRegionZoneWiseModel> GetGridDetail(DateTime? report_date)
        {
            List<SummaryRegionZoneWiseModel> data_list = new List<SummaryRegionZoneWiseModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("P_REPORTDT", report_date?.ToString("dd-MMM-yyyy"));
                    //parameters.Add("p_employee_code", UserManager.User.Code);
                    data_list = conn.Query<SummaryRegionZoneWiseModel>(QueryMaster.KYC_SUMMARYWISE_ZONEWISE_REPORT, parameters).ToList();

                    //conn.Open();
                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendLine(QueryMaster.KYC_SUMMARYWISE_ZONEWISE_REPORT);

                    //DynamicParameters parameters = new DynamicParameters();
                    //sb.AppendLine("trunc(selected_dt) between '" + from_date.ToString("dd-MMM-yyyy") + "' and '" + to_date.ToString("dd-MMM-yyyy") + "'");
                    //if (!string.IsNullOrWhiteSpace(zone))
                    //{
                    //    sb.AppendLine("and upper(Trim(zonedesc))='" + zone.ToUpper() + "'");
                    //}
                    //if (!string.IsNullOrWhiteSpace(region))
                    //{
                    //    sb.AppendLine("and upper(Trim(regiondesc))='" + region.ToUpper() + "'");
                    //}
                    //if (!string.IsNullOrWhiteSpace(ufc))
                    //{
                    //    sb.AppendLine("and upper(Trim(ufc_name))='" + ufc.ToUpper() + "'");
                    //}

                    //sb.AppendLine("group by upper(Trim(zonedesc)),upper(Trim(regiondesc)),upper(Trim(ufc_name))");
                    //CommonHelper.WriteLog("query :\n" + sb.ToString());
                    //data_list = conn.Query<SummaryRegionZoneWiseModel>(sb.ToString()).ToList();

                    CommonHelper.WriteLog("SummaryRegionwise Count: " + data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in SummaryRegionZoneWise> GetGridDetail ()" + ex.Message);
                }
            }
            return data_list;
        }

        public List<SummaryRegionZoneWiseModel> GetGridDetailKYC_Record_Status(DateTime report_date)
        {
            List<SummaryRegionZoneWiseModel> data_list = new List<SummaryRegionZoneWiseModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {

                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("P_REPORTDT", report_date.ToString("dd-MMM-yyyy"));
                    //parameters.Add("p_employee_code", UserManager.User.Code);
                    data_list = conn.Query<SummaryRegionZoneWiseModel>(QueryMaster.KYC_SUMMARYWISE_ZONEWISE_REPORT, parameters).ToList();

                    CommonHelper.WriteLog("SummaryRegionwise Count: " + data_list.Count);
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in SummaryRegionZoneWise> GetGridDetail ()" + ex.Message);
                }
            }
            return data_list;
        }

        public List<DDLModel> GetRegion_list(string p_zone)
        {
            List<DDLModel> list = new List<DDLModel>();
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                OracleCommand objCmd = new OracleCommand("SP_GET_REGION_NAME", objConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add(new OracleParameter("P_ZONE", p_zone));

                objCmd.Parameters.Add("GET_REGION", OracleType.Cursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    OracleDataReader objReader = objCmd.ExecuteReader();


                    if (objReader.HasRows)
                    {

                        while (objReader.Read())
                        {
                            DDLModel ObjModel = new DDLModel();
                            ObjModel.REGION_NAME_UTI = objReader["REGION_NAME_UTI"] != DBNull.Value ? Convert.ToString(objReader["REGION_NAME_UTI"]) : "";

                            list.Add(ObjModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error In GetRegion_list() " + ex.ToString());

                }
                objConn.Close();
            }

            return list;
        }

        public List<UpdateReallocationUFC_MODEL> GetUFC_list(string p_zone, string p_region)
        {
            List<UpdateReallocationUFC_MODEL> list = new List<UpdateReallocationUFC_MODEL>();
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                OracleCommand objCmd = new OracleCommand("SP_GET_UFC_NAME", objConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add(new OracleParameter("P_ZONE", p_zone));
                objCmd.Parameters.Add(new OracleParameter("P_REGION", p_region));
                objCmd.Parameters.Add("GET_UFC", OracleType.Cursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    OracleDataReader objReader = objCmd.ExecuteReader();


                    if (objReader.HasRows)
                    {

                        while (objReader.Read())
                        {
                            UpdateReallocationUFC_MODEL ObjModel = new UpdateReallocationUFC_MODEL();
                            ObjModel.REGION_NAME_UTI = objReader["REGION_NAME_UTI"] != DBNull.Value ? Convert.ToString(objReader["REGION_NAME_UTI"]) : "";
                            ObjModel.UFC_NAME_DNQ = objReader["UFC_NAME_DNQ"] != DBNull.Value ? Convert.ToString(objReader["UFC_NAME_DNQ"]) : "";
                            ObjModel.JURISDICTION = objReader["JURISDICTION"] != DBNull.Value ? Convert.ToString(objReader["JURISDICTION"]) : "";
                            ObjModel.EMAIL_ID = objReader["EMAIL_ID"] != DBNull.Value ? Convert.ToString(objReader["EMAIL_ID"]) : "";

                            list.Add(ObjModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error In GetUFC_list() " + ex.ToString());

                }
                objConn.Close();
            }

            return list;
        }

    }
}