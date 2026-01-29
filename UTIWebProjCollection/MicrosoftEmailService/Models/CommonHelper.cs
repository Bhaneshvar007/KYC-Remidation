using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MicrosoftEmailService.Models
{
    public class CommonHelper
    {
        public static ApplicationSettings GetApplicationSettings(string application_code)
        {
            return JsonConvert.DeserializeObject<List<ApplicationSettings>>(File.ReadAllText(HostingEnvironment.MapPath("~/ApplicationSettings.json"))).Where<ApplicationSettings>((Func<ApplicationSettings, bool>)(item => item.application_code == application_code)).FirstOrDefault<ApplicationSettings>();
        }

        public static void WriteLog(string message)
        {
            string appSetting = ConfigurationManager.AppSettings["ErrorLogFile"];
            if (!Directory.Exists(appSetting))
                Directory.CreateDirectory(appSetting);
            using (StreamWriter streamWriter = new StreamWriter(appSetting + "\\MS_EmailService_ErrorLog_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", true))
                streamWriter.WriteLine(DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "\t" + message);
        }
        public static DataTable getDataTable(string procedureName, SqlParameter sqlparameter)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName))
                {
                    if (sqlparameter != null)
                        cmd.Parameters.Add(sqlparameter);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    try
                    {

                        conn.Open();
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        WriteLog("Error " + ex.ToString());
                    }
                }
            }
            return dt;
        }
    }
}