using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace KYCAPP.Web.Models
{
    public class DataAcceptanceRpt_Staff
    {
        public List<DataAcceptanceReportStaffModel> GetGridDetail(DateTime from_date, DateTime to_date, string status)
        {
            List<DataAcceptanceReportStaffModel> data_list = new List<DataAcceptanceReportStaffModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_status", status);
                    parameters.Add("p_from_date", from_date.ToString("dd-MMM-yyyy"));
                    parameters.Add("p_to_date", to_date.ToString("dd-MMM-yyyy"));
                    data_list = conn.Query<DataAcceptanceReportStaffModel>(QueryMaster.KYC_DATAACCEPTANCE_REPORT_STAFF, parameters).ToList();
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in DataAcceptanceRpt_Staff> GetGridDetail ()" + ex.Message);
                }
            }
            return data_list;
        }
    }
}