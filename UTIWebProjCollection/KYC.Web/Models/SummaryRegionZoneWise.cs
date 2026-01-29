using Dapper;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KYC.Web.Models
{
    public class SummaryRegionZoneWise
    {
        public List<SummaryRegionZoneWiseModel> GetGridDetail(string zone, string region, string ufc, DateTime from_date, DateTime to_date)
        {
            List<SummaryRegionZoneWiseModel> data_list = new List<SummaryRegionZoneWiseModel>();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings.Get("DatabaseConnection")))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PIN2", zone);
                    data_list = conn.Query<SummaryRegionZoneWiseModel>(QueryMaster.KYC_REMEDIATION_GRID, parameters).ToList();
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