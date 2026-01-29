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
    public class ContestLeadership
    {
             

        //public List<ContestLeadershipModel> Get_ContestLeadershipBoardRpt()
        //{
        //    List<ContestLeadershipModel> data_list = new List<ContestLeadershipModel>();

        //    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
        //    {
        //        string query = QueryMaster.GetGridContestLeadershipRpt_Query;

        //        OracleCommand cmd = new OracleCommand(query, conn);

        //        try
        //        {
        //            conn.Open();
        //            CommonHelper.WriteLog("Connection Opened");

        //            OracleDataReader reader = cmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                CommonHelper.WriteLog("DataReader Count of Contest Leaderboard:-");

        //                while (reader.Read())
        //                {
        //                    ContestLeadershipModel model = new ContestLeadershipModel
        //                    {
        //                        RANK = reader["RANK"] != DBNull.Value ? Convert.ToInt32(reader["RANK"]) : 0,
        //                        EMPLOYEEID = reader["EMPLOYEEID"] != DBNull.Value ? Convert.ToInt32(reader["EMPLOYEEID"]) : 0,
        //                        //EMPLOYEEID = reader["EMPLOYEEID"] != DBNull.Value ? Convert.ToString(reader["EMPLOYEEID"]) : "",
        //                        NAME = reader["NAME"] != DBNull.Value ? Convert.ToString(reader["NAME"]) : "",
        //                        LOCATIONCODE = reader["LOCATIONCODE"] != DBNull.Value ? Convert.ToString(reader["LOCATIONCODE"]) : "",
        //                        UFC_LOC = reader["UFC_LOC"] != DBNull.Value ? Convert.ToString(reader["UFC_LOC"]) : "",
        //                        REGION_NAME = reader["REGION_NAME"] != DBNull.Value ? Convert.ToString(reader["REGION_NAME"]) : "",
        //                        ZONE = reader["ZONE"] != DBNull.Value ? Convert.ToString(reader["ZONE"]) : "",
        //                        REMEDIATED_CNT = reader["REMEDIATED_CNT"] != DBNull.Value ? Convert.ToString(reader["REMEDIATED_CNT"]) : ""
        //                    };

        //                    data_list.Add(model);

        //                    var recordDetails = string.Join(", ", model.GetType().GetProperties().Select(p => p.GetValue(model, null)?.ToString() ?? "null"));
        //                    CommonHelper.WriteLog("Retrieved Record: " + recordDetails);
        //                }

        //                CommonHelper.WriteLog("Total Count of ContestLeadership- Get_ContestLeadershipBoardRpt(): " + data_list.Count);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonHelper.WriteLog("Error in ContestLeadership Get_ContestLeadershipBoardRpt(): " + ex.Message);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }

        //    return data_list;
        //}


        public List<ContestLeadershipModel> Get_ContestLeadershipBoardRpt()
        {
            List<ContestLeadershipModel> data_list = new List<ContestLeadershipModel>();

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                string query = QueryMaster.GetGridContestLeadershipRpt_Query;

                OracleCommand cmd = new OracleCommand(query, conn);
                try
                {
                    conn.Open();
                    CommonHelper.WriteLog("Connection Opened");

                    // Execute the query and load the result into a DataTable
                    DataTable dt = new DataTable();
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }


                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    var rowDetails = string.Join(", ", dt.Columns.Cast<DataColumn>().Select(col => $"{col.ColumnName}: {row[col]}"));
                    //    CommonHelper.WriteLog("Query Result of Contest Leadership: " + rowDetails);
                    //}

                    // Loop through the rows and populate the list
                    foreach (DataRow row in dt.Rows)
                    {
                        ContestLeadershipModel model = new ContestLeadershipModel
                        {
                            RANK = row["RANK"] != DBNull.Value ? Convert.ToInt32(row["RANK"]) : 0,
                            //EMPLOYEEID = row["EMPLOYEEID"] != DBNull.Value ? Convert.ToInt32(row["EMPLOYEEID"]) : 0,
                            EMPLOYEEID = row["Employee Code"] != DBNull.Value ? Convert.ToString(row["Employee Code"]) : "",
                            NAME = row["Employee Name"] != DBNull.Value ? Convert.ToString(row["Employee Name"]) : "",
                            LOCATIONCODE = row["UFC/LOCATION"] != DBNull.Value ? Convert.ToString(row["UFC/LOCATION"]) : "",
                            UFC_LOC = row["UFC / Location Name"] != DBNull.Value ? Convert.ToString(row["UFC / Location Name"]) : "",
                            REGION_NAME = row["Region"] != DBNull.Value ? Convert.ToString(row["Region"]) : "",
                            ZONE = row["Zone"] != DBNull.Value ? Convert.ToString(row["Zone"]) : "",
                            KYC_Complied_Count = row["KYC Complied Count"] != DBNull.Value ? Convert.ToString(row["KYC Complied Count"]) : "",
                            REMEDIATED_CNT = row["Remediated Count"] != DBNull.Value ? Convert.ToString(row["Remediated Count"]) : "",
                            CONTEST_QUALIFY_STATUS = row["CONTEST_QUALIFY_STATUS"] != DBNull.Value ? Convert.ToString(row["CONTEST_QUALIFY_STATUS"]) : ""
                        };

                        data_list.Add(model);
                    }

                    CommonHelper.WriteLog("Total Count of ContestLeadership- Get_ContestLeadershipBoardRpt(): " + data_list.Count);
                }

                
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error in ContestLeadership Get_ContestLeadershipBoardRpt(): " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return data_list;
        }


    }
}