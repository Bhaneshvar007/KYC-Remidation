using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KYCAPP.Web.Models
{
    public class KYC_REMEDIATED_LEADERBOARD
    {
        public List<KYC_REMEDIATED_LEADERBOARDModel> GetLeaderBoard()
        {
            List<KYC_REMEDIATED_LEADERBOARDModel> list = new List<KYC_REMEDIATED_LEADERBOARDModel>();
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                OracleCommand objCmd = new OracleCommand("SP_Get_LeaderBoard_DATA", objConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_cursor", OracleType.Cursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    CommonHelper.WriteLog("Connection Opened");
                    OracleDataReader objReader = objCmd.ExecuteReader();


                    if (objReader.HasRows)
                    {
                        CommonHelper.WriteLog("DataReader Count");
                        int count1 = 0;

                        while (objReader.Read())
                        {
                            KYC_REMEDIATED_LEADERBOARDModel LeaderBoardObj = new KYC_REMEDIATED_LEADERBOARDModel();
                            LeaderBoardObj.RANK = objReader["RANK"] != DBNull.Value ? Convert.ToInt32(objReader["RANK"]) : 0;
                            LeaderBoardObj.EMPLOYEEID = objReader["Employee Code"] != DBNull.Value ? Convert.ToString(objReader["Employee Code"]) : "";
                            LeaderBoardObj.NAME = objReader["Employee Name"] != DBNull.Value ? Convert.ToString(objReader["Employee Name"]) : "";
                            LeaderBoardObj.LOCATIONCODE = objReader["UFC/LOCATION"] != DBNull.Value ? Convert.ToString(objReader["UFC/LOCATION"]) : "";
                            LeaderBoardObj.UFC_LOC = objReader["UFC / Location Name"] != DBNull.Value ? Convert.ToString(objReader["UFC / Location Name"]) : "";
                            LeaderBoardObj.REGION_NAME = objReader["Region"] != DBNull.Value ? Convert.ToString(objReader["Region"]) : "";
                            LeaderBoardObj.ZONE = objReader["Zone"] != DBNull.Value ? Convert.ToString(objReader["Zone"]) : "";
                            LeaderBoardObj.REMEDIATED_CNT = objReader["Remediated Count"] != DBNull.Value ? Convert.ToString(objReader["Remediated Count"]) : "";
                            count1++;
                            if (count1 <= 10)
                            {
                                list.Add(LeaderBoardObj);
                            }
                        }


                        CommonHelper.WriteLog("Count of GetLeaderBoard : " + list.Count());
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error In GetLeaderBoard() " + ex.ToString());

                }
                objConn.Close();
            }

            return list;
        }
        public List<KYC_REMEDIATED_LEADERBOARDModel> GetLeaderBoard_all()
        {
            List<KYC_REMEDIATED_LEADERBOARDModel> list = new List<KYC_REMEDIATED_LEADERBOARDModel>();
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                OracleCommand objCmd = new OracleCommand("SP_Get_LeaderBoard_DATA", objConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_cursor", OracleType.Cursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    CommonHelper.WriteLog("Connection Opened");
                    OracleDataReader objReader = objCmd.ExecuteReader();


                    if (objReader.HasRows)
                    {
                        CommonHelper.WriteLog("DataReader Count");
                        int count1 = 0;

                        while (objReader.Read())
                        {
                            KYC_REMEDIATED_LEADERBOARDModel LeaderBoardObj = new KYC_REMEDIATED_LEADERBOARDModel();
                            LeaderBoardObj.RANK = objReader["RANK"] != DBNull.Value ? Convert.ToInt32(objReader["RANK"]) : 0;
                            LeaderBoardObj.EMPLOYEEID = objReader["Employee Code"] != DBNull.Value ? Convert.ToString(objReader["Employee Code"]) : "";
                            LeaderBoardObj.NAME = objReader["Employee Name"] != DBNull.Value ? Convert.ToString(objReader["Employee Name"]) : "";
                            LeaderBoardObj.LOCATIONCODE = objReader["UFC/LOCATION"] != DBNull.Value ? Convert.ToString(objReader["UFC/LOCATION"]) : "";
                            LeaderBoardObj.UFC_LOC = objReader["UFC / Location Name"] != DBNull.Value ? Convert.ToString(objReader["UFC / Location Name"]) : "";
                            LeaderBoardObj.REGION_NAME = objReader["Region"] != DBNull.Value ? Convert.ToString(objReader["Region"]) : "";
                            LeaderBoardObj.ZONE = objReader["Zone"] != DBNull.Value ? Convert.ToString(objReader["Zone"]) : "";
                            LeaderBoardObj.REMEDIATED_CNT = objReader["Remediated Count"] != DBNull.Value ? Convert.ToString(objReader["Remediated Count"]) : "";
                            list.Add(LeaderBoardObj);
                            count1++;
                            
                        }


                        CommonHelper.WriteLog("Count of GetLeaderBoard : " + list.Count());
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error In GetLeaderBoard() " + ex.ToString());

                }
                objConn.Close();
            }

            return list;
        }
    }
}
