
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.DirectoryServices;
using System.Configuration;


namespace KYCAPP.Web.Models
{
    public class Login : EntityBusinessLogicBase<Login>
    {
        public UserDetailsModel GetUserDetail(string UserID, string Password, bool isSSO)
        {

            CommonHelper.WriteLog("trying to login by : " + UserID);
            UserDetailsModel umodel = new UserDetailsModel();
            umodel.is_cm_login = false;
            DataTable DT = new DataTable();

            //Added By Bhaneshvar
            if (!isSSO)
            {

                CommonHelper.WriteLog(" Via id :");
                DT = DataAccess.ExecuteQuery("SELECT * from EMPLOYEE_LIST_FOR_KYC where employeecode='" + UserID + "'");
            }
            else
            {
                DT = DataAccess.ExecuteQuery("SELECT * FROM EMPLOYEE_LIST_FOR_KYC WHERE LOWER(email_id) = LOWER('" + UserID + "')"); // This is for sso email // added by bahenshvar 22012026
            }

            // DataTable CMDT = DataAccess.ExecuteQuery("select EMP_ID,EMP_ROLE from DYNAMIC.mis_cuser_logs where valid_upto='30-DEC-9999' and emp_role in ('CM', 'RH', 'ZH') and employeecode ='" + UserID + "'");

            CommonHelper.WriteLog("DT row count :" + DT.Rows.Count);
            bool IsAdLogin = Convert.ToBoolean(ConfigurationManager.AppSettings["IsADLogin"].ToString());
            if (DT.Rows.Count > 0)
            {
                umodel.Code = string.IsNullOrWhiteSpace(DT.Rows[0]["employeecode"].ToString()) ? "" : DT.Rows[0]["employeecode"].ToString();
                umodel.Name = string.IsNullOrWhiteSpace(DT.Rows[0]["Name"].ToString()) ? "" : DT.Rows[0]["Name"].ToString();
                umodel.emp_role = string.IsNullOrWhiteSpace(DT.Rows[0]["emp_role"].ToString()) ? "" : DT.Rows[0]["emp_role"].ToString();
                umodel.Email = string.IsNullOrWhiteSpace(DT.Rows[0]["EMAIL_ID"].ToString()) ? "" : DT.Rows[0]["EMAIL_ID"].ToString();

                if (umodel.emp_role.ToUpper() == "CM")
                {
                    umodel.is_cm_login = true;
                }
                CommonHelper.WriteLog("is_cm_login :" + umodel.is_cm_login + "| role :" + umodel.emp_role);
            }
            //if (CMDT.Rows.Count > 0)
            //{
            //    umodel.is_cm_login = true;

            //}
            if (IsAdLogin)
            {
                if (!isSSO)
                {
                    if (!CheckUser(UserID, Password))
                    {
                        umodel = null;
                    }
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
                CommonHelper.WriteLog("error in Login > CheckUser() :" + ex.ToString());
                status = false;
            }
            return status;
        }
    }
}