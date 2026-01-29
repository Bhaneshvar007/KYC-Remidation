using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;


namespace KYC.Web.Models
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
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString()))
            {
                try
                {
                    conn.Open();
                    using (OracleDataAdapter SqDA = new OracleDataAdapter(cQuery, conn))
                    {
                        SqDA.Fill(dt);
                    }
                    return dt;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }
      
    }
}