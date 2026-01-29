using Dapper;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;

namespace KYCAPP.Web.Models
{
    public class ReasonWiseReport
    {
        public List<ReasonWiseReportModel> GetGridDetailReasonwiseRept(DateTime report_date)
        {
            List<ReasonWiseReportModel> data_list = new List<ReasonWiseReportModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", UserManager.User.Code);
                    //parameters.Add("p_report_date", report_date);

                    data_list = conn.Query<ReasonWiseReportModel>(QueryMaster.GetGridReasonwiseRpt_Query, parameters).ToList();
                    CommonHelper.WriteLog("Count of ReasonWiseReport> GetGridDetailReasonwiseRept() : " + data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReasonWiseReport> GetGridDetailReasonwiseRept() " + ex.Message);
                }
            }
            return data_list;
        }

        public List<ReasonWiseReportModel> GetGridDetailReasonwiseRept_Region(DateTime report_date, string p_zone)
        {
            List<ReasonWiseReportModel> data_list = new List<ReasonWiseReportModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", UserManager.User.Code);
                    parameters.Add("p_zone", p_zone);
                    //parameters.Add("p_report_date", report_date);

                    data_list = conn.Query<ReasonWiseReportModel>(QueryMaster.GetGridReasonwiseRpt_Query_Region, parameters).ToList();
                    CommonHelper.WriteLog("Count of ReasonWiseReport> GetGridDetailReasonwiseRept_Region() : " + data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReasonWiseReport> GetGridDetailReasonwiseRept_Region() " + ex.Message);
                }
            }
            return data_list;
        }
        public List<ReasonWiseReportModel> GetGridDetailReasonwiseRept_UFC(DateTime report_date, string p_zone, string p_region)
        {
            List<ReasonWiseReportModel> data_list = new List<ReasonWiseReportModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", UserManager.User.Code);
                    parameters.Add("p_zone", p_zone);
                    parameters.Add("p_region", p_region);
                    //parameters.Add("p_report_date", report_date);

                    data_list = conn.Query<ReasonWiseReportModel>(QueryMaster.GetGridReasonwiseRpt_Query_UFC, parameters).ToList();
                    CommonHelper.WriteLog("Count of ReasonWiseReport> GetGridDetailReasonwiseRept_UFC() : " + data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReasonWiseReport> GetGridDetailReasonwiseRept_UFC() " + ex.Message);
                }
            }
            return data_list;
        }
        public List<ReasonWiseReportModel> GetGridDetailReasonwiseRept_Emp(DateTime report_date, string p_zone, string p_region, string p_ufc)
        {
            List<ReasonWiseReportModel> data_list = new List<ReasonWiseReportModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", UserManager.User.Code);
                    parameters.Add("p_zone", p_zone);
                    parameters.Add("p_region", p_region);
                    parameters.Add("p_ufcname", p_ufc);
                    //parameters.Add("p_report_date", report_date);

                    data_list = conn.Query<ReasonWiseReportModel>(QueryMaster.GetGridReasonwiseRpt_Query_EMP, parameters).ToList();
                    CommonHelper.WriteLog("Count of ReasonWiseReport> GetGridDetailReasonwiseRept_Emp() : " + data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReasonWiseReport> GetGridDetailReasonwiseRept_Emp() " + ex.Message);
                }
            }
            return data_list;
        }

        public List<KYC_DataModel> GetGridPopupReasonwiseRept(DateTime report_date, string p_zone, string p_region, string p_ufc, string p_empId, string p_remark_code)
        {
            CommonHelper.WriteLog("query of ReasonWiseReport> GetGridPopupReasonwiseRept() : p_zone " + p_zone + "p_region: " + p_region + "p_ufc: " + p_ufc + "p_remark_code: " + p_remark_code);
            List<KYC_DataModel> data_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(QueryMaster.GetPopupReasonWiseRpt_Query);
                    sb.AppendLine("inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = EK.EMPLOYEECODE left outer join(select ufc_code, ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode = um.ufc_code");
                    sb.AppendLine("left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.mis_cuser_logs where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id left outer join(select ufc_code, ufc_name, region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location = um.ufc_code left outer join(select region_code, region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code = rmz.region_code where");
                    sb.AppendLine("case When ek.employeecode in ('2824', '1287', '3010', '3195', '8500', '4426', '4517', '2372', '4471', '4488', '4594', '8000', '4237', '4228', '2179') then 'VALID' when((upper(trim(ek.emp_role)) = 'ZH' and a.zone_uti = cu.zone) or (upper(trim(ek.emp_role)) = 'RH' and  a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone)");
                    sb.AppendLine("or (upper(trim(ek.emp_role))='CM' and a.region_name_uti=rmz.region_name and a.zone_uti=cu.zone  and a.ufc_NAME=um.UFC_NAME) or nvl(upper(trim(a.selected_empid)), 'ZZZ') ='" + UserManager.User.Code + "') then 'VALID' else 'ALL' end = 'VALID'");
                    sb.AppendLine("and a.remark_code is not null and a.remark_code='" + p_remark_code + "'");
                    //sb.AppendLine("and a.report_dt ='" + report_date.ToString("dd-MMM-yyyy") + "'");

                    if (!string.IsNullOrWhiteSpace(p_zone))
                    {
                        sb.AppendLine("and a.ZONE_UTI = '" + p_zone + "'");
                    }
                    if (!string.IsNullOrWhiteSpace(p_region))
                    {
                        sb.AppendLine("and a.REGION_NAME_UTI = '" + p_region + "'");
                    }
                    if (!string.IsNullOrWhiteSpace(p_ufc))
                    {
                        sb.AppendLine("and a.UFC_NAME = '" + p_ufc + "'");
                    }
                    if (!string.IsNullOrWhiteSpace(p_empId))
                    {
                        sb.AppendLine("and a.SELECTED_EMPID = '" + p_empId + "'");
                    }
                    CommonHelper.WriteLog("query of ReasonWiseReport> GetGridPopupReasonwiseRept() : " + sb.ToString());


                    data_list = conn.Query<KYC_DataModel>(sb.ToString()).ToList();
                    CommonHelper.WriteLog("Count of ReasonWiseReport> GetGridPopupReasonwiseRept() : " + data_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in ReasonWiseReport> GetGridPopupReasonwiseRept() " + ex.Message);
                }
            }
            return data_list;
        }
    }
}