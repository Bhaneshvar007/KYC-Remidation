using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
//using Dapper;


namespace KYCAPP.Web.Models
{
    public class DataAccess
    {
        public DataSet ExecuteQuery2(string cQuery)
        {
            DataSet ds = new DataSet();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                try
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        {
                            conn.Open();
                            using (OracleDataAdapter SqDA = new OracleDataAdapter(cmd))
                            {
                                SqDA.Fill(ds);
                            }
                            return ds;
                        }
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        public static DataTable ExecuteQuery(string cQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                //CommonHelper.WriteLog("connection string :\n" + ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString());
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
                {

                    conn.Open();
                    using (OracleDataAdapter SqDA = new OracleDataAdapter(cQuery, conn))
                    {
                        SqDA.Fill(dt);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("error in DataAccess>ExecuteQuery() :" + ex.Message);
                CommonHelper.WriteLog("inner exception DataAccess>ExecuteQuery \n" + ex.InnerException);
                return null;
            }

        }

        public static string DBConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();
            }
        }

    }
}