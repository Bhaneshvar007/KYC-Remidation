using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace KYCAPP.Web.Models
{
    public class GoogleMap
    {
        public List<GoogleMapModel> GetGogleMapDetails(GoogleMapSearchActivities objSea)
        {
            List<GoogleMapModel> list = new List<GoogleMapModel>();
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                OracleCommand objCmd = new OracleCommand("SP_GET_GOOGLE_MAP", objConn);
                objCmd.CommandType = CommandType.StoredProcedure;

                // objCmd.Parameters.Add(new OracleParameter("p_employee_code", UserManager.User.Code));
                //objCmd.Parameters.Add(new OracleParameter("P_SEARCH_TEXT", "54545"));
                //objCmd.Parameters.Add(new OracleParameter("EMP_NAME_SEARCH", null));
                //objCmd.Parameters.Add(new OracleParameter("UFC_NAME_SEARCH", null));
                //objCmd.Parameters.Add(new OracleParameter("FOLIONO_SEARCH", null));

                objCmd.Parameters.Add("p_cursor", OracleType.Cursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    OracleDataReader objReader = objCmd.ExecuteReader();


                    if (objReader.HasRows)
                    {
                        CommonHelper.WriteLog("DataReader Count");
                        int count1 = 0;

                        while (objReader.Read())
                        {
                            GoogleMapModel modelObj = new GoogleMapModel();
                            modelObj.ZONEDESC = objReader["ZONEDESC"] != DBNull.Value ? Convert.ToString(objReader["ZONEDESC"]) : "";
                            modelObj.REGIONDESC = objReader["REGIONDESC"] != DBNull.Value ? Convert.ToString(objReader["REGIONDESC"]) : "";
                            modelObj.UFC_NAME = objReader["UFC_NAME"] != DBNull.Value ? Convert.ToString(objReader["UFC_NAME"]) : "";
                            modelObj.SELECTED_REC = objReader["SELECTED_REC"] != DBNull.Value ? Convert.ToString(objReader["SELECTED_REC"]) : "";
                            modelObj.ACNO = objReader["ACNO"] != DBNull.Value ? Convert.ToString(objReader["ACNO"]) : "";
                            modelObj.INVNAME = objReader["INVNAME"] != DBNull.Value ? Convert.ToString(objReader["INVNAME"]) : "";
                            modelObj.MOBILE = objReader["MOBILE"] != DBNull.Value ? Convert.ToString(objReader["MOBILE"]) : "";
                            modelObj.CONCAT_ADD = objReader["CONCAT_ADD"] != DBNull.Value ? Convert.ToString(objReader["CONCAT_ADD"]) : "";
                            count1++;

                            list.Add(modelObj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error In GetGogleMapDetails() " + ex.ToString());

                }
                finally
                {
                    objCmd.Dispose();
                    objConn.Close();
                }
            }

            return list;
        }

        public List<GoogleMapModel> GetGogleMapDetails_1()
        {
            List<GoogleMapModel> list = new List<GoogleMapModel>();
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                OracleCommand objCmd = new OracleCommand("SP_GET_GOOGLE_MAP_1", objConn);
                objCmd.CommandType = CommandType.StoredProcedure;

                //objCmd.Parameters.Add(new OracleParameter("p_employee_code", UserManager.User.Code));
                //objCmd.Parameters.Add(new OracleParameter("P_SEARCH_TEXT", "54545"));
                //objCmd.Parameters.Add(new OracleParameter("EMP_NAME_SEARCH", null));
                //objCmd.Parameters.Add(new OracleParameter("UFC_NAME_SEARCH", null));
                //objCmd.Parameters.Add(new OracleParameter("FOLIONO_SEARCH", null));

                objCmd.Parameters.Add("p_cursor", OracleType.Cursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    OracleDataReader objReader = objCmd.ExecuteReader();


                    if (objReader.HasRows)
                    {
                        CommonHelper.WriteLog("DataReader Count");

                        while (objReader.Read())
                        {
                            GoogleMapModel modelObj = new GoogleMapModel();
                            modelObj.SRNO = Convert.ToInt32(objReader["SRNO"]);
                            modelObj.ZONEDESC = objReader["ZONEDESC"] != DBNull.Value ? Convert.ToString(objReader["ZONEDESC"]) : "";
                            modelObj.REGIONDESC = objReader["REGIONDESC"] != DBNull.Value ? Convert.ToString(objReader["REGIONDESC"]) : "";
                            modelObj.UFC_NAME = objReader["UFC_NAME"] != DBNull.Value ? Convert.ToString(objReader["UFC_NAME"]) : "";
                            modelObj.SELECTED_REC = objReader["SELECTED_REC"] != DBNull.Value ? Convert.ToString(objReader["SELECTED_REC"]) : "";
                            modelObj.ACNO = objReader["ACNO"] != DBNull.Value ? Convert.ToString(objReader["ACNO"]) : "";
                            modelObj.INVNAME = objReader["INVNAME"] != DBNull.Value ? Convert.ToString(objReader["INVNAME"]) : "";
                            modelObj.MOBILE = objReader["MOBILE"] != DBNull.Value ? Convert.ToString(objReader["MOBILE"]) : "";
                            modelObj.CONCAT_ADD = objReader["CONCAT_ADD"] != DBNull.Value ? Convert.ToString(objReader["CONCAT_ADD"]) : "";

                            list.Add(modelObj);
                        }
                    }
                    var count = list.Count;
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error In GetGogleMapDetails() " + ex.ToString());

                }
                finally
                {
                    objCmd.Dispose();
                    objConn.Close();
                }
            }

            return list;
        }


        public ResponseModel UpdateLongLat(int P_SRNO, string P_CONCAT_ADD, double P_LONGITUDE, double P_LATITUDE)
        {
            string query = string.Empty;
            ResponseModel res = new ResponseModel();


            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                conn.Open();
                try
                {


                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("P_SRNO", P_SRNO);
                    parameters.Add("P_CONCAT_ADD", P_CONCAT_ADD);
                    parameters.Add("P_LONGITUDE", P_LONGITUDE);
                    parameters.Add("P_LATITUDE", P_LATITUDE);
                    query = "UPDATE mistest.KYC_REMED_BASE_DAT SET LONGITUDE=:P_LONGITUDE, LATITUDE=:P_LATITUDE WHERE SRNO =:P_SRNO AND CONCAT_ADD = :P_CONCAT_ADD";
                    conn.Execute(query, parameters);

                    res.msg = "Data updated successfully!";
                    res.is_success = true;
                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in GoogleMap> UpdateLongLat()" + ex.Message);
                    res.msg = "Something went wrong ,Please try after sometime !";
                }
            }
            return res;
        }


    }
}