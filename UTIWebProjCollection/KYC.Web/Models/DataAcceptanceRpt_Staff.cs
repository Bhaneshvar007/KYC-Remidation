using Dapper;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KYC.Web.Models
{
    public class DataAcceptanceRpt_Staff
    {
        public List<DataAcceptanceReportStaffModel> GetGridDetail(DateTime from_date, DateTime to_date)
        {
            List<DataAcceptanceReportStaffModel> data_list = new List<DataAcceptanceReportStaffModel>();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings.Get("DatabaseConnection")))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@from_date", from_date);
                    parameters.Add("@to_date", to_date);
                    data_list = conn.Query<DataAcceptanceReportStaffModel>(QueryMaster.KYC_REMEDIATION_GRID, parameters).ToList();
                }
                catch (Exception ex)
                {
                    // write exeception
                }
            }
            return data_list;
        }
    }
}