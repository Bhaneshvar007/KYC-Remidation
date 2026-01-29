using Dapper;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KYC.Web.Models
{
    public class DataAcceptanceFullReport
    {
        public List<KYC_DataModel> GetGridDetail(DateTime from_date, DateTime to_date)
        {
            List<KYC_DataModel> data_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings.Get("DatabaseConnection")))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PIN2", "");
                    data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_REMEDIATION_GRID, parameters).ToList();
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