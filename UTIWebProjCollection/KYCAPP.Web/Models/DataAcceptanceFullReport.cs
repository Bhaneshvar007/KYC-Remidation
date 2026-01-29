using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;


namespace KYCAPP.Web.Models
{
    public class DataAcceptanceFullReport
    {
        //public List<KYC_DataModel> GetGridDetail(DateTime from_date, DateTime to_date)
        //{
        //    List<KYC_DataModel> data_list = new List<KYC_DataModel>();
        //    using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            DynamicParameters parameters = new DynamicParameters();
        //            parameters.Add("p_from_date", from_date.ToString("dd-MMM-yyyy"));
        //            parameters.Add("p_to_date", to_date.ToString("dd-MMM-yyyy"));
        //            parameters.Add("p_employee_code", UserManager.User.Code);
        //            data_list = conn.Query<KYC_DataModel>(QueryMaster.KYC_DATAACCEPTANCE_FULL_REPORT, parameters).ToList();

        //            CommonHelper.WriteLog("Count of Full Report: " + data_list.Count);
        //        }


        //        catch (Exception ex)
        //        {
        //            CommonHelper.WriteLog("error in DataAcceptanceFullReport> GetGridDetail ()" + ex.Message);
        //        }
        //    }
        //    return data_list;
        //}

        public List<KYC_DataModel> GetGridDetail(KYC_DETAILS_SEARCH objSearch)
        {


            List<KYC_DataModel> record_Status_list = new List<KYC_DataModel>();
            using (OracleConnection conn = new OracleConnection(DataAccess.DBConnectionString))
            {
                try
                {
                    conn.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(QueryMaster.KYC_DATAACCEPTANCE_FULL_REPORT);
                    sb.AppendLine(" left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = EK.EMPLOYEECODE left outer join EMPLOYEE_LIST_FOR_KYC ELK1 on a.selected_empid = ELK1.EMPLOYEECODE");
                    sb.AppendLine(" left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id left outer join(select ufc_code, ufc_name, region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location = um.ufc_code left outer join(select region_code, region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code = rmz.region_code left outer join mistest.KYC_AUM_BRACKET AB on a.AUM_BRACKET = ab.aum_bracket");
                    sb.AppendLine(" where case When ek.employeecode in ('2372','2824', '1287', '3010', '3195', '8500', '4426', '4517', '4228') then 'VALID' WHEN ek.employeecode in ('8000', '4237') and a.ZONE_UTI in ('SOUTH', 'CORP') then 'VALID' WHEN ek.employeecode in ('4488', '4594', '2179')  and a.ZONE_UTI IN('WEST', 'NORTH', 'GULF', 'CORP')  then 'VALID' WHEN ek.employeecode in ('4471', '1073')  and a.ZONE_UTI IN('EAST', 'NORTH', 'WEST', 'GULF', 'CORP') then 'VALID' when((upper(trim(ek.emp_role)) = 'ZH' and a.zone_uti = cu.zone) or");
                    sb.AppendLine(" (upper(trim(ek.emp_role))='RH' and  a.region_name_uti=rmz.region_name and a.zone_uti=cu.zone) or(upper(trim(ek.emp_role)) = 'CM' and a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone  and a.ufc_NAME = um.UFC_NAME) or nvl(upper(trim(ek.employeecode)), 'ZZZ') = a.selected_empid) then 'VALID' else 'ALL' end = 'VALID'");

                    //sb.AppendLine(" left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = ek.EMPLOYEECODE");
                    //sb.AppendLine(" left outer join EMPLOYEE_LIST_FOR_KYC ELK1 on a.selected_empid = ELK1.EMPLOYEECODE left outer join(select emp_id, emp_role, location, region_code, zone from dynamic.mis_cuser_logs where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode = cu.emp_id left outer join(select ufc_code, ufc_name, region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location = um.ufc_code ");
                    //sb.AppendLine(" left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code left outer join mistest.KYC_AUM_BRACKET AB on a.AUM_BRACKET = ab.aum_bracket where case ");
                    //sb.AppendLine("When ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228') then 'VALID' WHEN ek.employeecode in ('8000', '4237') and a.ZONE_UTI in ('SOUTH', 'CORP') then 'VALID' WHEN ek.employeecode in ('4488', '4594', '2179')  and a.ZONE_UTI IN('WEST', 'NORTH', 'GULF', 'CORP')  then 'VALID' WHEN ek.employeecode in ('2372', '4471', '1073')  and a.ZONE_UTI IN('EAST', 'NORTH', 'CORP') then 'VALID' when((upper(trim(ek.emp_role)) = 'ZH' and a.zone_uti = cu.zone) or ");
                    //sb.AppendLine("(upper(trim(ek.emp_role))='RH' and  a.region_name_uti=rmz.region_name and a.zone_uti=cu.zone) or(upper(trim(ek.emp_role)) = 'CM' and a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone  and a.ufc_NAME = um.UFC_NAME) or nvl(upper(trim(ek.employeecode)), 'ZZZ') = a.selected_empid) then 'VALID' else 'ALL' end = 'VALID' ");

                    //sb.AppendLine(" inner join EMPLOYEE_LIST_FOR_KYC EK on '" + UserManager.User.Code + "' = EK.EMPLOYEECODE   left outer join EMPLOYEE_LIST_FOR_KYC ELK1 on a.selected_empid = ELK1.EMPLOYEECODE");
                    //sb.AppendLine("left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.mis_cuser_logs where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id");
                    //sb.AppendLine("left outer join (select ufc_code,ufc_name,region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location=um.ufc_code  left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code");
                    //sb.AppendLine(" left outer join mistest.KYC_AUM_BRACKET AB on a.AUM_BRACKET=ab.aum_bracket where");
                    //sb.AppendLine(" case When ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228') then 'VALID' WHEN ek.employeecode in ('8000', '4237') and a.ZONE_UTI = 'SOUTH' then 'VALID' WHEN ek.employeecode in ('4488', '4594', '2179')  and a.ZONE_UTI IN('WEST', 'NORTH')  then 'VALID' WHEN ek.employeecode in ('2372','1073','4471')  and ZONE_UTI = 'EAST' then 'VALID'");
                    //sb.AppendLine(" when((upper(trim(ek.emp_role)) = 'ZH' and a.zone_uti = cu.zone) or (upper(trim(ek.emp_role)) = 'RH' and  a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone) or(upper(trim(ek.emp_role)) = 'CM' and a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone  and a.ufc_NAME = um.UFC_NAME) or nvl(upper(trim(ek.employeecode)), 'ZZZ') = a.selected_empid) then 'VALID' else 'ALL' end = 'VALID'");
                    if (!string.IsNullOrWhiteSpace(objSearch.P_SEARCH_TEXT))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.ACNO||a.INVNAME||a.AGENT||a.CONCAT_ADD)),upper(trim('" + objSearch.P_SEARCH_TEXT + "')))");
                    }
                    if (!string.IsNullOrWhiteSpace(objSearch.EMP_NAME_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.SELECTED_EMPID||ELK1.NAME)),upper(trim('" + objSearch.EMP_NAME_SEARCH + "')))");
                    }
                    if (!string.IsNullOrWhiteSpace(objSearch.UFC_NAME_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(replace(replace(a.UFC_NAME,'(',''),')',''))),upper(trim(replace(replace('" + objSearch.UFC_NAME_SEARCH + "','(',''),')','')))) ");
                        //sb.AppendLine("and REGEXP_LIKE(upper(trim(a.UFC_NAME)),upper(trim('" + objSearch.UFC_NAME_SEARCH + "')))");
                    }
                    if (objSearch.AUM_BRACKET_SEARCH != null)
                    {
                        if (objSearch.AUM_BRACKET_SEARCH.Count > 0)
                        {
                            var strAum_Br = String.Join(",", objSearch.AUM_BRACKET_SEARCH.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and a.AUM_BRACKET IN (" + strAum_Br + ")");
                        }
                    }
                    if (!string.IsNullOrEmpty(objSearch.KYCFLAG_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.KYCFLAG)),upper(trim('" + objSearch.KYCFLAG_SEARCH + "')))");
                    }
                    if (!string.IsNullOrWhiteSpace(objSearch.NOMINEEFLAG_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(upper(trim(a.NOMINEEFLAG)),upper(trim('" + objSearch.NOMINEEFLAG_SEARCH + "')))");
                    }
                    if (objSearch.AADHARSEEDINGFLAG_SEARCH != null)
                    {
                        if (objSearch.AADHARSEEDINGFLAG_SEARCH != null && objSearch.AADHARSEEDINGFLAG_SEARCH.Count > 0)
                        {
                            var strAadhar_flag = String.Join(",", objSearch.AADHARSEEDINGFLAG_SEARCH.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and a.AADHARSEEDINGFLAG IN (" + strAadhar_flag + ")");
                        }
                    }
                    if (objSearch.BANK_FLAG_SEARCH != null)
                    {
                        if (objSearch.BANK_FLAG_SEARCH.Count > 0)
                        {
                            var strBank_flag = String.Join(",", objSearch.BANK_FLAG_SEARCH.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and a.BANK_FLAG IN (" + strBank_flag + ")");
                        }
                    }
                    if (objSearch.FOLIO_STATUS != null)
                    {
                        if (objSearch.FOLIO_STATUS.Count > 0)
                        {
                            var strFolioStatus = String.Join(",", objSearch.FOLIO_STATUS.Select(s => $"'{s.Replace("'", "''")}'"));
                            sb.AppendLine("and upper(trim(a.selected_rec)) IN (" + strFolioStatus + ")");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(objSearch.FOLIONO_SEARCH))
                    {
                        sb.AppendLine("and REGEXP_LIKE(a.acno, '" + objSearch.FOLIONO_SEARCH + "')");
                    }
                    if (!string.IsNullOrWhiteSpace(objSearch.PRE_POST_2008))
                    {
                        sb.AppendLine("and REGEXP_LIKE(a.PRE_POST_2008, '" + objSearch.PRE_POST_2008 + "')");
                    }


                    CommonHelper.WriteLog("Query of KYC Records Details: " + sb.ToString());
                    record_Status_list = conn.Query<KYC_DataModel>(sb.ToString()).ToList();
                    CommonHelper.WriteLog("Count of DataAcceptanceFullReport> GetGridDetail() :" + record_Status_list.Count);

                }
                catch (Exception ex)
                {
                    CommonHelper.WriteLog("error in DataAcceptanceFullReport> GetGridDetail() " + ex.Message);
                }
            }
            return record_Status_list;
        }

    }
}