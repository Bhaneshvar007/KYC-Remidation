using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace KYC.Web.Models
{
    public class KYCRemediation
    {
        string con_string = string.Empty;
        public List<KYC_DataModel> GetGridDetail(string Search_Text)
        {
            List<KYC_DataModel> kyc_data_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(con_string))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PIN2", Search_Text);
                    kyc_data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_REMEDIATION_GRID, parameters).ToList();
                }
                catch (Exception ex)
                {
                    // write exeception
                }
            }
            return kyc_data_list;
        }
        public string Save(List<KYC_DataModel> model_list)
        {
            List<KYC_DataModel> kyc_data = new List<KYC_DataModel>();
            string Response = string.Empty;
            using (OracleConnection conn = new OracleConnection(con_string))
            {
                conn.Open();
                OracleTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (KYC_DataModel model in model_list)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@employee_code", UserManager.User.Code);
                        kyc_data = conn.Query<KYC_DataModel>(QueryMaster.KYC_REMEDIATION_UPDATE_CMD, parameters, trans).ToList();
                    }
                    trans.Commit();
                    Response = "Data updated successfully!";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Response = "Something went wrong ,Please try after sometime !\n" + ex.Message;
                }
            }
            return Response;
        }
    }
}