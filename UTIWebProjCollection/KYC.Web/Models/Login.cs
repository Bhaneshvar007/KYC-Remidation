using MicroORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Cylsys.Common;
using System.DirectoryServices;
using System.Configuration;

namespace KYC.Web.Models
{
    public class Login : EntityBusinessLogicBase<Login>
    {
        public UserDetailsModel GetUserDetail(string UserID, string Password)
        {
            UserDetailsModel umodel = new UserDetailsModel();
            DataTable DT = DataAccess.ExecuteQuery("SELECT * from EMPLOYEE_LIST_FOR_KYC where employeecode='" + UserID + "'");

            bool IsAdLogin = Convert.ToBoolean(ConfigurationManager.AppSettings["IsADLogin"].ToString());
            if (DT.Rows.Count > 0)
            {
                umodel.Code = string.IsNullOrWhiteSpace(DT.Rows[0]["employeecode"].ToString()) ? "" : DT.Rows[0]["employeecode"].ToString();
                umodel.Name = string.IsNullOrWhiteSpace(DT.Rows[0]["Name"].ToString()) ? "" : DT.Rows[0]["Name"].ToString();
            }
            if (IsAdLogin)
            {
                if (!CheckUser(UserID, Password))
                {
                    umodel = null;
                }
            }
            else
            {
                if (DT.Rows.Count == 0)
                {
                    umodel = null;
                }
            }
            return umodel;
        }
        public bool CheckUser(string userid, string password)
        {
            bool status = false;

            DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["ADConnectionString"].ToString(), userid.Trim(), password);
            try
            {
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + userid + ")";

                search.PropertiesToLoad.Add("cn");

                SearchResult result = search.FindOne();

                if (null == result)
                {
                    status = false;
                }
                else
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}