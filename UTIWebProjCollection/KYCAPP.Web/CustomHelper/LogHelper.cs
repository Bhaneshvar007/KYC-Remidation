using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYCAPP.Web.CustomHelper
{
    public class LogHelper
    {
        static string strcon = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public static void InsertInLogger(string sessionId, string pageName, string eventName)
        {
            //Response res = new Response();
            using (OracleConnection con = new OracleConnection(strcon))
            {
                OracleCommand cmd = new OracleCommand("INSERT_INTO_TBL_LOGGER", con);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("P_PAGE_NAME", pageName);
                    cmd.Parameters.AddWithValue("P_SESSION_ID", sessionId);
                    cmd.Parameters.AddWithValue("P_CREATED_BY", UserManager.User.Code);
                    cmd.Parameters.AddWithValue("P_CREATED_ON", DateTime.Now);
                    cmd.Parameters.AddWithValue("P_EVENT_NAME", string.IsNullOrWhiteSpace(eventName) ? "" : eventName);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("Error in LogHelper > InsertInLogger()" + ex.Message);
                }

                finally
                {

                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    if (con != null)
                    {
                        con.Dispose();
                        con.Close();
                    }

                }
            }

        }
    }
}
