using MicroORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Cylsys.Common;
using System.Data.SqlClient;

namespace KYC.Web.Models
{
    public class Common2 : EntityBusinessLogicBase<Common2>
    {
        DbCommonHelper bll = new DbCommonHelper();
        List<DDLModel> ListModel = new List<DDLModel>();
        public List<DDLModel> GetSecurityTypes()
        {
            ListModel = new List<DDLModel>();
            DDLModel ddlobj = new DDLModel();
            ListModel = bll.LOAD_DROPDOWNLIST(GetQuery.DDL_QUERY_SECURITY_TYPES, null);
            return ListModel;
        }
        public List<DDLModel> GetEmailfiles(DateTime from_date,DateTime to_date)
        {
            SqlParameter[] param = {
                new SqlParameter("@from_date",from_date.ToString("yyyy-MM-dd")),
                new SqlParameter("@to_date",to_date.ToString("yyyy-MM-dd"))

            };

            ListModel = new List<DDLModel>();
            DDLModel ddlobj = new DDLModel();
            ListModel = bll.LOAD_FILE_DROPDOWNLIST("pps_ddl_bind_excel_files", param);
            return ListModel;
        }
        public List<DDLModel> GetCommentTypes()
        {
            ListModel = new List<DDLModel>();
            DDLModel ddlobj = new DDLModel();
            ListModel = bll.LOAD_DROPDOWNLIST(GetQuery.DDL_QUERY_COMMENT_TYPES, null);
            return ListModel;
        }

        public List<MenusModel> GetMenus()// approved/reject
        {
            List<MenusModel> menulist = new List<MenusModel>();


            SqlParameter[] param = {
                new SqlParameter("@userid",Helper.GetUserID())

            };
            DDLModel ddlobj = new DDLModel();
            DataTable dt = bll.ExecuteParaQuery(GetQuery.DDL_GET_MENUS, param);

            Helper.WriteLog("menu Count : " + dt.Rows.Count);

            foreach (DataRow drow in dt.Rows)
            {
                MenusModel mmobj = new MenusModel();

                mmobj.SeqID = string.IsNullOrEmpty(drow["id"].ToString()) ? 0 : Convert.ToInt32(drow["id"].ToString());
                mmobj.Link = string.IsNullOrEmpty(drow["link"].ToString()) ? "" : drow["link"].ToString();
                mmobj.Title = string.IsNullOrEmpty(drow["title"].ToString()) ? "" : drow["title"].ToString();
                mmobj.css_class = string.IsNullOrEmpty(drow["css_class"].ToString()) ? "" : drow["css_class"].ToString();
                // mmobj.is_active = string.IsNullOrEmpty(drow["isactive"].ToString()) ? 0 : Convert.ToInt32(drow["is_active"].ToString());
                menulist.Add(mmobj);

            }
            return menulist;
        }
        public List<ReportAccessModel> GetReportAccessByEmployeeCode()// approved/reject
        {
            List<ReportAccessModel> report_access_list = new List<ReportAccessModel>();


            SqlParameter[] param = {
                new SqlParameter("@employee_code",Helper.GetUserID())

            };
            DDLModel ddlobj = new DDLModel();
            DataTable dt = bll.ExecuteParaQuery(GetQuery.GET_REPORT_ACCESS_BY_EMPLOYEE_CODE, param);
            
            foreach (DataRow drow in dt.Rows)
            {
                ReportAccessModel mmobj = new ReportAccessModel();

                mmobj.ID = string.IsNullOrEmpty(drow["id"].ToString()) ? 0 : Convert.ToInt32(drow["id"].ToString());
                mmobj.Name = string.IsNullOrEmpty(drow["Name"].ToString()) ? "" : drow["Name"].ToString();
                mmobj.is_send_mail_allowed = string.IsNullOrEmpty(drow["is_send_mail_allowed"].ToString()) ? false : Convert.ToBoolean(drow["is_send_mail_allowed"]);
                mmobj.is_download_excel_allowed = string.IsNullOrEmpty(drow["is_download_excel_allowed"].ToString()) ? false : Convert.ToBoolean(drow["is_download_excel_allowed"]);
                report_access_list.Add(mmobj);

            }
            return report_access_list;
        }

        public List<DDLModel> GetRoles()
        {
            ListModel = new List<DDLModel>();
            DDLModel ddlobj = new DDLModel();
            ListModel = bll.LOAD_DROPDOWNLIST(GetQuery.DDL_GET_ROLES, null);
            return ListModel;
        }
        public List<DDLModel2> GetEmpList(string q)
        {
            SqlParameter[] param = {
                new SqlParameter("@emp_name",q)
            };
            List<DDLModel2> ListModel2 = new List<DDLModel2>();
            DDLModel2 ddlobj = new DDLModel2();
            ListModel2 = bll.FEEL_DROPDOWNLIST_FOR_EMP("select distinct emp_code,emp_name from ems_tbl_record_waiting_for_payment where lower(emp_name) like lower('%" + q + "%')", "emp_name", null);
            return ListModel2;
        }

    }
}