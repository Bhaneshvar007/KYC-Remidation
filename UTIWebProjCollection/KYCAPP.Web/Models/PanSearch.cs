using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using Dapper;

namespace KYCAPP.Web.Models
{
    public class PanSearchModel
    {
        public int Id { get; set; }
        public string PANNO { get; set; }
        public string NAME_AS_PER_PAN { get; set; }
    }

    public class PanSearchModule
    {
        public List<PanSearchModel> GetGridDetail(string PanNo)
        {
            List<PanSearchModel> pan_list = new List<PanSearchModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    SaveLogPanNo(PanNo, conn);
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("P_PanNo", string.IsNullOrWhiteSpace(PanNo) ? "" : PanNo);

                    pan_list = conn.Query<PanSearchModel>(QueryMaster.PanSearchQuery, parameters).ToList();
                    CommonHelper.WriteLog("Count of PanSearchModule> GetGridDetail () :" + pan_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in PanSearchModule> GetGridDetail (): " + ex.Message);
                }
            }
            return pan_list;
        }

        public static void SaveLogPanNo(string panno, OracleConnection conn)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_employee_code", UserManager.User.Code);
                parameters.Add("p_panno", panno);
                parameters.Add("p_DateOfSearch", DateTime.Now);
                conn.Query(QueryMaster.PanSearchAddLog, parameters);
                CommonHelper.WriteLog($"Data added successfully : PanNo.:{panno} , EmployeeCode : {UserManager.User.Code} , DateOfSearch : {DateTime.Now}");


            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("Error in SaveLogPanNo(): " + ex.Message);
            }
        }
    }
}