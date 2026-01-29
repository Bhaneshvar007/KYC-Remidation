using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYCAPP.Web.Models
{
    public class QueryMaster
    {
        public static string KYC_REMEDIATION_GRID
        {
            get
            {
                //return @"SELECT * FROM mistest.KYC_REMED_BASE_DAT KR  WHERE KR.PIN1=:PIN2";
                //return @"SELECT* FROM mistest.KYC_REMED_BASE_DAT krbd WHERE SELECTED_REC='N' and REGEXP_LIKE(upper(trim(CONCAT_ADD)),:PIN2)";


                //return @" SELECT 
                //          a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM AUM_OF_THE_FOLIO
                //                a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent  ARN_Code
                //                FROM mistest.KYC_REMED_BASE_DAT a 
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b
                //                ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),' ','')))
                //                WHERE nvl(a.selected_rec,'N')='N'
                //                      and b.employeecode=:P_EMPCODE
                //                      and REGEXP_LIKE(upper(trim(CONCAT_ADD)),:P_SEARCH_TEXT)";

                return @" SELECT  a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER,a.agent ARN_CODE, a.AUM_BRACKET AUM_BRACKET,
                        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG
                        FROM mistest.KYC_REMED_BASE_DAT a
                        LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                        WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                        and REGEXP_LIKE(upper(trim(INVNAME||AGENT||CONCAT_ADD)),:P_SEARCH_TEXT)";

                //return @" SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))";

            }
        }
        public static string KYC_REMEDIATION_GRID_CM__AUMBRCKT
        {
            get
            {
                //working
                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //            and (a.AUM_BRACKET =:P_AUM_BRACKET)";

                //new query
                return @"SELECT 
                            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_CODE
                            FROM mistest.KYC_REMED_BASE_DAT a
                             LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON replace(upper(trim(a.UFC_NAME_DNQ)),' ','')=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),' ','')))
                                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                            and (a.AUM_BRACKET =:P_AUM_BRACKET)";

            }
        }

        public static string KYC_REMEDIATION_GRID_CM__AUMBRCKT_ALL_And_Search_Text
        {
            get
            {
                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //            and (a.AUM_BRACKET =:P_AUM_BRACKET)";

                //wORKING
                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //                and REGEXP_LIKE(upper(trim(CONCAT_ADD||agent)),:P_SEARCH_TEXT)";

                //NEW QUERY

                return @"SELECT 
                            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_CODE
                            FROM mistest.KYC_REMED_BASE_DAT a
                            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON replace(upper(trim(a.UFC_NAME_DNQ)),' ','')=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),' ','')))
                                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                            and REGEXP_LIKE(upper(trim(INVNAME||AGENT||CONCAT_ADD)),:P_SEARCH_TEXT)";
            }
        }

        public static string KYC_REMEDIATION_GRID_CM__AUMBRCKT_ALL
        {
            get
            {
                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //            and (a.AUM_BRACKET =:P_AUM_BRACKET)";

                //Working
                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE";

                //New query
                return @"SELECT 
                            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_CODE
                            FROM mistest.KYC_REMED_BASE_DAT a
                            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON replace(upper(trim(a.UFC_NAME_DNQ)),' ','')=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),' ','')))
                                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE";
            }
        }
        public static string KYC_REMEDIATION_GRID_CM
        {
            get
            {
                //return @"SELECT * FROM mistest.KYC_REMED_BASE_DAT KR  WHERE KR.PIN1=:PIN2";
                //return @"SELECT* FROM mistest.KYC_REMED_BASE_DAT krbd WHERE SELECTED_REC='N' and REGEXP_LIKE(upper(trim(CONCAT_ADD)),:PIN2)";


                //return @" SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //            and REGEXP_LIKE(upper(trim(CONCAT_ADD)),:P_SEARCH_TEXT
                //            or a.AUM_BRACKET =:P_AUM_BRACKET";


                //return @"SELECT  a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG
                //        FROM mistest.KYC_REMED_BASE_DAT a
                //        LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))
                //        WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //        and case when (nvl(upper(trim(CONCAT_ADD)),'ZZZ')='ZZZ' or REGEXP_LIKE(upper(trim(CONCAT_ADD)),:P_SEARCH_TEXT))
                //           then 'VALID' ELSE 'NOT VALID' END ='VALID'";

                //return @" SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.JURISDICTION))=upper(trim(replace(replace(b.LOCATIONNAME,'UFC',''),'','')))";


                //new working query 
                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.UFC_NAME_DNQ))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),'','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //            and REGEXP_LIKE(upper(trim(INVNAME||CONCAT_ADD||agent||)),:P_SEARCH_TEXT)
                //            and (a.AUM_BRACKET =:P_AUM_BRACKET)";

                //return @"SELECT 
                //            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_Code
                //            FROM mistest.KYC_REMED_BASE_DAT a
                //            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(replace(a.UFC_NAME_DNQ,' ','')))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),' ','')))
                //                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                //            and REGEXP_LIKE(upper(trim(INVNAME||AGENT||CONCAT_ADD)),:P_SEARCH_TEXT)
                //            and (a.AUM_BRACKET =:P_AUM_BRACKET)";

                return @"SELECT

                            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_CODE
                            FROM mistest.KYC_REMED_BASE_DAT a
                             LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON replace(upper(trim(a.UFC_NAME_DNQ)),' ','')=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),' ','')))
                                WHERE nvl(a.selected_rec,'N')='N'  and b.employeecode=:P_EMPCODE
                          and REGEXP_LIKE(upper(trim(CONCAT_ADD||agent||INVNAME)),upper(trim(:P_SEARCH_TEXT)))
                            and (a.AUM_BRACKET =:P_AUM_BRACKET)";
            }


        }
        public static string KYC_REMEDIATION_UPDATE_CMD
        {
            get
            {
                return @"UPDATE mistest.KYC_REMED_BASE_DAT SET selected_rec='S',selected_dt=SYSDATE ,selected_empid=:p_employee_code where SRNO=:p_SRNO";
            }
        }

        public static string KYC_REMEDIATION_CM_UPDATE_CMD
        {
            get
            {
                return @"UPDATE mistest.KYC_REMED_BASE_DAT SET selected_rec='S',selected_dt=SYSDATE ,selected_empid=:p_employee_code, SUPVERVISOR_ID=:P_LOGIN_EMPCODE where SRNO =:p_SRNO";
                //return @"UPDATE mistest.KYC_REMED_BASE_DAT SET selected_rec='S',selected_dt=SYSDATE ,selected_empid=:p_employee_code where SRNO=:p_SRNO";
            }
        }
        public static string KYC_DATAACCEPTANCE_FULL_REPORT
        {
            get
            {
                //return @"select * from mistest.kyc_remed_base_dat where selected_empid=:p_employee_code and trunc(selected_dt) between :p_from_date and :p_to_date";

                //return @" SELECT
                //                      a.SRNO
                //        --                                      a.AUM AUM_OF_THE_FOLIO
                //                        ,a.ufccode
                //                        ,a.AUM_Bracket AUM_BRACKET
                //                      ,a.zone_uti ZONEDESC
                //                      ,a.region_name_uti REGIONDESC
                //                      ,a.ufc_name UFC_NAME
                //                      ,a.selected_empid SELECTED_EMPID
                //                      ,trunc(selected_dt)  AS SELECTION_DATE
                //                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) EMPLOYEENAME
                //                      ,case when nvl(a.selected_rec,'N')='N' then 'To Be Selected'
                //                      when nvl(a.selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                //                      when nvl(a.selected_rec,'N')='K' then 'KFIND - KYC Validation Confirmation in Progress'
                //                      when nvl(a.selected_rec,'N')='S' then 'Selected'
                //                      when nvl(a.selected_rec,'N')='R' then 'Remediated'
                //                      when nvl(a.selected_rec,'N')='V' then 'CVL Records shared with Zones'
                //                      when nvl(a.selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                //                      when nvl(a.selected_rec,'N')='T' then 'Folios with Transmission DOne' end SELECTED_REC
                //                      ,a.ACNO ACNO
                //                      ,a.INVNAME INVNAME
                //                      ,a.JT1 JT1 
                //                      ,a.Jt2 JT2
                //                      ,a.DNR_NAME DNR_NAME
                //                      ,nvl(a.PAN,'NO PAN') PAN
                //                      ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') KRA_PAN_KYCSTATUS
                //                      ,nvl(a.JT1PAN,'NO PAN') JT1PAN
                //                      ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') KRA_PAN1_KYCSTATUS
                //                      ,nvl(a.JT2PAN,'NO PAN') JT2PAN
                //                      ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') KRA_PAN2_KYCSTATUS
                //                     ,nvl(a.DNR_PAN,'NO PAN') DNR_PAN
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DNR_PAN_KYCSTATUS
                //                      ,nvl(a.KRA_GPAN,'NO PAN') KRA_GPAN
                //                      ,a.MOHDESC MOHDESC
                //                      ,bm.BRANCH_NAME SOURCE_BRANCH, a.FOLIO_MIN_DATE,a.pin1 PIN1
                //                      ,a.MOBILE MOBILE, a.EMAIL EMAIL
                //                      ,a.CONCAT_ADD CONCAT_ADD
                //                      ,a.NOMINEEFLAG
                //                      ,a.KYCFLAG
                //                      ,a.AADHARSEEDINGFLAG
                //                      ,a.BANK_FLAG
                //                      ,a.AGENT ARN_CODE
                //                      ,a.AGENTNAME     ARN_NAME
                //                      ,a.AGNETMOBILE ARN_MOBILE
                //                      ,a.AGENTEMAIL    ARN_EMAIL
                //                      ,a.INVESTED_SCHEMES INVESTED_SCHEMES
                //                     FROM mistest.KYC_REMED_BASE_DAT a
                //                      left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE
                //                      inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //                      left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                //                      where --case when ek.emp_role='CM' then a.SUPVERVISOR_ID else a.selected_empid end=:p_employee_code
                //                      case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                //                      when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID'
                //                      else 'XXXX'
                //                      end='VALID'
                //                      and trunc(a.selected_dt) between :p_from_date and :p_to_date";

                return @"SELECT
                                      a.SRNO
                                        ,a.ufccode
                                        ,a.AUM_Bracket AUM_BRACKET
                                      ,a.zone_uti ZONEDESC
                                      ,a.region_name_uti REGIONDESC
                                      ,a.ufc_name UFC_NAME
                                      ,a.selected_empid SELECTED_EMPID
                                      ,trunc(selected_dt)  AS SELECTION_DATE
                                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) EMPLOYEENAME
                                      ,case when nvl(a.selected_rec,'N')='N' then 'To Be Selected'
                                      when nvl(a.selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                                      when nvl(a.selected_rec,'N')='K' then 'KFIND - KYC Validation Confirmation in Progress'
                                      when nvl(a.selected_rec,'N')='S' then 'Selected'
                                     when nvl(a.selected_rec,'N')='R' then 'Remediated'
                                      when nvl(a.selected_rec,'N')='V' then 'CVL Records shared with Zones'
                                      when nvl(a.selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                                      when nvl(a.selected_rec,'N')='T' then 'Folios with Transmission DOne' end SELECTED_REC
                                      ,a.ACNO ACNO
                                      ,a.INVNAME INVNAME
                                      ,a.JT1 JT1
                                      ,a.Jt2 JT2
                                      ,a.DNR_NAME DNR_NAME
                                      ,nvl(a.PAN,'NO PAN') PAN
                                      ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') KRA_PAN_KYCSTATUS
                                      ,nvl(a.JT1PAN,'NO PAN') JT1PAN
                                      ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') KRA_PAN1_KYCSTATUS
                                      ,nvl(a.JT2PAN,'NO PAN') JT2PAN
                                      ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') KRA_PAN2_KYCSTATUS
                                     ,nvl(a.DNR_PAN,'NO PAN') DNR_PAN
                                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DNR_PAN_KYCSTATUS
                                      ,nvl(a.KRA_GPAN,'NO PAN') KRA_GPAN
                                      ,a.MOHDESC MOHDESC
                                      ,bm.BRANCH_NAME SOURCE_BRANCH, a.FOLIO_MIN_DATE,a.pin1 PIN1
                                      ,a.MOBILE MOBILE, a.EMAIL EMAIL
                                      ,a.CONCAT_ADD CONCAT_ADD
                                      ,a.NOMINEEFLAG
                                      ,a.KYCFLAG
                                      ,a.AADHARSEEDINGFLAG
                                      ,a.BANK_FLAG
                                      ,a.AGENT ARN_CODE
                                      ,a.AGENTNAME     ARN_NAME
                                      ,a.AGNETMOBILE ARN_MOBILE
                                      ,a.AGENTEMAIL    ARN_EMAIL
                                      ,a.INVESTED_SCHEMES INVESTED_SCHEMES,
                                      a.PRE_POST_2008 AS PRE_POST_2008  
                                      FROM mistest.KYC_REMED_BASE_DAT a
                                     ";
            }
        }
        public static string KYC_DATAACCEPTANCE_REPORT_STAFF
        {
            get
            {
                return @"SELECT 
                                  a.SRNO
                            	  ,a.acno FOLIO
                            	  ,a.SELECTED_EMPID
                            	  ,b.NAME
                            	  ,b.LOCATIONNAME
                            	  ,a.selected_dt DATE_ACCEPT_DT
                            	  ,(a.selected_dt+30) DATE_ACCEPT_END_DT
                                  ,a.AUM
                            FROM mistest.KYC_REMED_BASE_DAT a
                            INNER JOIN mistest.EMPLOYEE_LIST_FOR_KYC b on a.selected_empid=b.employeecode
                            WHERE a.SELECTED_REC=:p_status and trunc(a.SELECTED_DT) between :p_from_date and :p_to_date
                            ORDER BY a.SELECTED_DT DESC";
            }
        }
        public static string KYC_SUMMARYWISE_ZONEWISE_REPORT
        {
            get
            {
                //return @"SELECT 
                //                upper(Trim(zonedesc)) ZONE
                //          	   ,upper(Trim(regiondesc)) REGION
                //          	   ,upper(Trim(ufc_name)) BRANCH,
                //          sum(case when selected_rec in ('S','R') and selected_dt is not null then 1 else 0 end) TOTAL_CASE_ACCEPTED,
                //          sum(case when selected_rec in ('S') and selected_dt is not null then 1 else 0 end) PENDING_FOR_ACTION,
                //          sum(case when selected_rec in ('P') and selected_dt is not null then 1 else 0 end) MOVED_TO_POOL
                //          from mistest.KYC_REMED_BASE_DAT where ";

                //return @"select
                //    a.ufc_name AS UFC_NAME, a.region_name_uti REGION,a.zone_uti AS ZONE, nvl(a.selected_empid,'NOT SELECTED') AS EMPLOYEE_CODE,
                //    --(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) AS EMPLOYEE_NAME,
                //        a.aum_bracket AS AUM_BRACKET, count(a.acno) AS FOLIO_COUNT,
                //        sum(case when nvl(a.selected_rec, 'N') = 'S' and selected_dt <=:P_REPORTDT and nvl(a.selected_dt, '31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) AS SELECTED,
                //        sum(case when upper(Trim(a.KYCFLAG)) = 'KYC COMPLIED' and a.KYCFLAG_DT <=:P_REPORTDT and nvl(a.KYCFLAG_DT, '31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) AS KYC_REMEDIATED,
                //        sum(case when a.NOMINEEFLAG = 'YES'  and a.NOMINEE_DT <=:P_REPORTDT and nvl(a.NOMINEE_DT, '31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) AS NOMINEE_UPDATED,
                //        sum(case when a.AADHARSEEDINGFLAG = 'LINKED'  and a.ADHAR_SEED_DT <=:P_REPORTDT and nvl(a.ADHAR_SEED_DT, '31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) AADHAR_SEEDING_DONE,
                //        sum(case when a.BANK_FLAG in ('VALID')  and a.BANK_VALID_DT <=:P_REPORTDT and nvl(a.BANK_VALID_DT, '31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) BANK_STATUS_VALIDATED
                //        from mistest.KYC_REMED_BASE_DAT A
                //        inner join EMPLOYEE_LIST_FOR_KYC EK on: p_employee_code = EK.EMPLOYEECODE
                //        left outer join(select emp_id, emp_role, location, region_code, zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode = cu.emp_id
                //        left outer join(select ufc_code, ufc_name, region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location = um.ufc_code
                //        left outer join(select region_code, region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code = rmz.region_code
                //        where
                //        case
                //        When ek.employeecode in ('2824', '1287', '3010', 'ACEO', '8500', '4426', '4517', '2372', '4471', '4488', '4594', '8000', '4237', '4228', '2179','1073' ) then 'VALID'
                //        when((upper(trim(ek.emp_role)) = 'ZH' and a.zone_uti = cu.zone) or
                //        (upper(trim(ek.emp_role)) = 'RH' and  a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone) or
                //        (upper(trim(ek.emp_role)) = 'CM' and a.region_name_uti = rmz.region_name and a.zone_uti = cu.zone  and a.ufccode = cu.location)
                //        or a.selected_empid = ek.employeecode) then 'VALID' else 'ALL' end = 'VALID'
                //        group by a.ufc_name,a.region_name_uti,a.zone_uti,nvl(a.selected_empid, 'NOT SELECTED'),a.aum_bracket
                //        order by a.zone_uti,a.region_name_uti,a.ufc_name,nvl(a.selected_empid, 'NOT SELECTED') ";

                return @"select
                        zone_uti ZONE,region_name_uti REGION,ufc_name  UFC_NAME,nvl(selected_empid,'NOT SELECTED') EMPLOYEE_CODE,
                        (select name from EMPLOYEE_LIST_FOR_KYC where employeecode=nvl(selected_empid,'NOT SELECTED')) EMPLOYEE_NAME,
                        aum_bracket AUM_BRACKET,
                        case when nvl(selected_rec,'N')='N' then 'To Be Selected'
                        when nvl(selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                        when nvl(selected_rec,'N')='K' then 'KFIN - KYC Validation Confirmation in Progress'
                        when nvl(selected_rec,'N')='S' then 'Selected'
                        when nvl(selected_rec,'N')='R' then 'Remediated'
                        when nvl(selected_rec,'N')='V' then 'CVL Records shared with Zones'
                        when nvl(selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                        when nvl(selected_rec,'N')='T' then 'Folios with Transmission DOne' end FOLIO_STATUS,
                        inv_type INV_TYPE,
                        pre_post_2008 PRE_POST_2008,
                        count(acno) FOLIO_COUNT,
                        sum(case when nvl(selected_rec,'N')='S' and selected_dt <=:P_REPORTDT and nvl(selected_dt,'31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) SELECTED_BY_UFC_EMP,
                        sum(case when upper(Trim(KYCFLAG)) = 'KYC COMPLIED' and KYCFLAG_DT <=:P_REPORTDT and nvl(KYCFLAG_DT,'31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) KYC_STATUS_YES,
                        sum(case when NOMINEEFLAG ='YES'  and NOMINEE_DT <=:P_REPORTDT and nvl(NOMINEE_DT,'31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) NOMINEE_STATUS_YES,
                        sum(case when AADHARSEEDINGFLAG='LINKED'  and ADHAR_SEED_DT <=:P_REPORTDT and nvl(ADHAR_SEED_DT,'31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) AADHARSEEDING_STATUS_YES,
                        sum(case when BANK_FLAG in ('VALID')  and BANK_VALID_DT <=:P_REPORTDT and nvl(BANK_VALID_DT,'31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) BANK_STATUS_YES
                        from mistest.KYC_REMED_BASE_DAT
                        group by ufc_name,region_name_uti,zone_uti,nvl(selected_empid,'NOT SELECTED')
                        ,case when nvl(selected_rec,'N')='N' then 'To Be Selected'
                        when nvl(selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                        when nvl(selected_rec,'N')='K' then 'KFIN - KYC Validation Confirmation in Progress'
                        when nvl(selected_rec,'N')='S' then 'Selected'
                        when nvl(selected_rec,'N')='R' then 'Remediated'
                        when nvl(selected_rec,'N')='V' then 'CVL Records shared with Zones'
                        when nvl(selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                        when nvl(selected_rec,'N')='T' then 'Folios with Transmission DOne' end
                        ,pre_post_2008,aum_bracket,inv_type
                        order by zone_uti,region_name_uti,ufc_name
                        ,aum_bracket";
            }
        }

        public static string KYC_DATA_DOWNLOAD_QUERY
        {
            get
            {
                //return @"SELECT 
                //          a.SRNO
                //          ,a.ACNO
                //          ,INVNAME FIRST_HOLDER_NAME
                //          ,a.JT1 SECOND_HOLDER_NAME
                //          ,a.Jt2 THIRD_HOLDER_NAME
                //          ,a.DNR_NAME DONOR_NAME
                //          ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //          ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //          ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //          ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //          ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //          ,a.AUM AUM_OF_THE_FOLIO
                //          ,A.AGENT
                //          ,A.AGENTNAME
                //    FROM mistest.KYC_REMED_BASE_DAT a 
                //    WHERE SELECTED_EMPID=:P_EMPID 
                // AND nvl(selected_rec,'N')='S'";


                //return @"SELECT 
                //                      a.SRNO
                //                       ,a.AUM_Bracket AUM_BRACKET 
                //                      ,a.ACNO
                //                      ,INVNAME FIRST_HOLDER_NAME
                //                      ,a.JT1 SECOND_HOLDER_NAME
                //                      ,a.Jt2 THIRD_HOLDER_NAME
                //                      ,a.DNR_NAME DONOR_NAME
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                //                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                //                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                //                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //                      ,a.AUM AUM_OF_THE_FOLIO
                //                      ,A.AGENT ARNCODE
                //                      ,A.AGENTNAME
                //                      ,a.MOBILENO AS MOBILE
                //                      ,a.EMAILID AS EMAIL
                //                      ,a.INVESTED_SCHEMES
                //                      ,b.LOCATIONNAME_ORIG AS LOCATION 
                //                      ,a.NOMINEEFLAG                                                
                //                      ,a.KYCFLAG                                                            
                //                      ,a.AADHARSEEDINGFLAG                                                 
                //                      ,a.BANK_FLAG      
                //                      FROM mistest.KYC_REMED_BASE_DAT a 
                //                      LEFT OUTER JOIN EMPLOYEE_LIST_FOR_KYC B ON A.SELECTED_EMPID=B.EMPLOYEECODE
                //                    WHERE SELECTED_EMPID=:P_EMPID
                //                    AND nvl(selected_rec,'N')='S' and trunc(selected_dt) between :p_from_date and :p_to_date
                //                    or REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                //                    ORDER BY selected_dt DESC";

                //OLD
                //return @"SELECT
                //                      a.SRNO
                //                       ,a.AUM_Bracket AUM_BRACKET
                //                      ,a.ACNO
                //                      ,INVNAME FIRST_HOLDER_NAME
                //                      ,a.JT1 SECOND_HOLDER_NAME
                //                      ,a.Jt2 THIRD_HOLDER_NAME
                //                      ,a.DNR_NAME DONOR_NAME
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                //                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                //                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                //                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //                      ,a.AUM AUM_OF_THE_FOLIO
                //                      ,A.AGENT ARNCODE
                //                      ,A.AGENTNAME
                //                      ,a.MOBILENO AS MOBILE
                //                      ,a.EMAILID AS EMAIL
                //                      ,a.INVESTED_SCHEMES
                //                      ,b.LOCATIONNAME_ORIG AS LOCATION
                //                      ,a.NOMINEEFLAG                                               
                //                      ,a.KYCFLAG                                                           
                //                      ,a.AADHARSEEDINGFLAG                                                
                //                      ,a.BANK_FLAG     
                //                      FROM mistest.KYC_REMED_BASE_DAT a
                //                      LEFT OUTER JOIN EMPLOYEE_LIST_FOR_KYC B ON A.SELECTED_EMPID=B.EMPLOYEECODE
                //                    WHERE SELECTED_EMPID=:P_EMPID
                //                    AND nvl(selected_rec,'N')='S' and (trunc(selected_dt) between :p_from_date and :p_to_date
                //                    and
                //                    case when :P_SEARCH_TEXT is null then 'VALID'
                //                    when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                //                    then 'VALID' else null end ='VALID')
                //                    ORDER BY selected_dt DESC";

                //NEW ONE
                //return @"SELECT
                //                      a.SRNO
                //                       ,a.AUM_Bracket AUM_BRACKET
                //                      ,a.ACNO
                //                      ,INVNAME FIRST_HOLDER_NAME
                //                      ,a.JT1 SECOND_HOLDER_NAME
                //                      ,a.Jt2 THIRD_HOLDER_NAME
                //                      ,a.DNR_NAME DONOR_NAME
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                //                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                //                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                //                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //                      ,a.AUM AUM_OF_THE_FOLIO
                //                      ,A.AGENT ARNCODE
                //                      ,A.AGENTNAME
                //                      ,a.MOBILE AS MOBILE
                //                      ,a.EMAIL AS EMAIL
                //                      ,a.INVESTED_SCHEMES
                //                      ,b.LOCATIONNAME_ORIG AS LOCATION
                //                      ,a.NOMINEEFLAG                                               
                //                      ,a.KYCFLAG                                                           
                //                      ,a.AADHARSEEDINGFLAG                                                
                //                      ,a.BANK_FLAG     
                //                    ,a.selected_empid EMPLOYEECODE
                //                    ,c.NAME EMPLOYEENAME
                //                      FROM mistest.KYC_REMED_BASE_DAT a
                //                      LEFT OUTER JOIN EMPLOYEE_LIST_FOR_KYC B ON A.SELECTED_EMPID=B.EMPLOYEECODE
                //                    WHERE SELECTED_EMPID=:P_EMPID
                //                    AND nvl(selected_rec,'N')='S' and (trunc(selected_dt) between :p_from_date and :p_to_date
                //                    and
                //                    case when :P_SEARCH_TEXT is null then 'VALID'
                //                    when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                //                    then 'VALID')
                //                    ORDER BY selected_dt DESC";

                //new
                return @"   SELECT
                                      a.SRNO
                                       ,a.AUM_Bracket AUM_BRACKET
                                      ,a.ACNO
                                      ,INVNAME FIRST_HOLDER_NAME
                                      ,a.JT1 SECOND_HOLDER_NAME
                                      ,a.Jt2 THIRD_HOLDER_NAME
                                      ,a.DNR_NAME DONOR_NAME
                                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                                      ,a.AUM AUM_OF_THE_FOLIO
                                      ,A.AGENT ARNCODE
                                      ,A.AGENTNAME
                                      ,a.MOBILE AS MOBILE
                                      ,a.EMAIL AS EMAIL
                                      ,a.INVESTED_SCHEMES
                                      ,b.LOCATIONNAME_ORIG AS LOCATION
                                      ,a.NOMINEEFLAG                                               
                                      ,a.KYCFLAG                                                           
                                      ,a.AADHARSEEDINGFLAG                                                
                                      ,a.BANK_FLAG     
                                    ,a.selected_empid EMPLOYEECODE
                                    ,b.NAME EMPLOYEENAME
                                      FROM mistest.KYC_REMED_BASE_DAT a
                                      LEFT OUTER JOIN EMPLOYEE_LIST_FOR_KYC B ON A.SELECTED_EMPID=B.EMPLOYEECODE
                                    WHERE SELECTED_EMPID=:P_EMPID
                                    AND nvl(selected_rec,'N')='S' and (trunc(selected_dt) between :p_from_date and :p_to_date
                                    and
                                    case when :P_SEARCH_TEXT is null then 'VALID'
                                    when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                                    then 'VALID' else null end ='VALID')
                                    ORDER BY selected_dt DESC";
            }
        }

        public static string KYC_DATA_DOWNLOAD_QUERY_CM
        {
            get
            {
                //return @"SELECT 
                //          a.SRNO
                //          ,a.ACNO
                //          ,INVNAME FIRST_HOLDER_NAME
                //          ,a.JT1 SECOND_HOLDER_NAME
                //          ,a.Jt2 THIRD_HOLDER_NAME
                //          ,a.DNR_NAME DONOR_NAME
                //          ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //          ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //          ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //          ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //          ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //          ,a.AUM AUM_OF_THE_FOLIO
                //          ,A.AGENT
                //          ,A.AGENTNAME
                //    FROM mistest.KYC_REMED_BASE_DAT a 
                //    WHERE SELECTED_EMPID=:P_EMPID 
                // AND nvl(selected_rec,'N')='S'";


                //return @" SELECT                                                 

                //                      a.SRNO                                                    
                //                      ,a.AUM_Bracket                                                  
                //                      ,a.zone_uti zone                                                  
                //                      ,region_name_uti region                                                  
                //                      ,a.ufc_name                                                          
                //                      ,a.ACNO                                                   
                //                      ,INVNAME FIRST_HOLDER_NAME                                                
                //                      ,a.JT1 SECOND_HOLDER_NAME                                                    
                //                      ,a.Jt2 THIRD_HOLDER_NAME                                                         
                //                      ,a.DNR_NAME DONOR_NAME                                                       
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN                                                
                //                      ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS                                                             
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN                                                   
                //                      ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS                                                     
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN                                                        
                //                      ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS                                                        
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN                                                   
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS                                                          
                //                      ,nvl(a.KRA_GPAN,'NO PAN') GAURDIAN_PAN                                                          
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER                                                       
                //                      ,a.NOMINEEFLAG                                                
                //                     ,a.KYCFLAG                                                            
                //                      ,a.AADHARSEEDINGFLAG                                                 
                //                    ,a.BANK_FLAG                                                       
                //                     ,A.AGENT ARN_CODE                                                   
                //                    ,A.AGENTNAME     ARN_NAME
                //                    ,a.selected_empid EMPLOYEECODE
                //                    ,c.NAME EMPLOYEECODE
                //                ,upper(trim(replace(replace(c.LOCATIONNAME_ORIG,'UFC',''),'',''))) LOCATION
                //                FROM mistest.KYC_REMED_BASE_DAT a
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.UFC_NAME_DNQ))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),'','')))
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  c  ON upper(Trim(a.selected_empid)) = upper(Trim(c.employeecode))
                //                WHERE b.employeecode='3655'
                //                AND nvl(selected_rec,'N')='S' and trunc(selected_dt) between '01-OCT-2022' and '31-MAR-2023' ORDER BY selected_dt DESC";


                //return @" SELECT                                                 

                //                      a.SRNO                                                    
                //                      ,a.AUM_Bracket AUM_BRACKET                                                  
                //                      ,a.zone_uti zone                                                  
                //                      ,region_name_uti region                                                  
                //                      ,a.ufc_name                                                          
                //                      ,a.ACNO                                                   
                //                      ,INVNAME FIRST_HOLDER_NAME                                                
                //                      ,a.JT1 SECOND_HOLDER_NAME                                                    
                //                      ,a.Jt2 THIRD_HOLDER_NAME                                                         
                //                      ,a.DNR_NAME DONOR_NAME                                                       
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN                                                
                //                      ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS                                                             
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN                                                   
                //                      ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS                                                     
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN                                                        
                //                      ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS                                                        
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN                                                   
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS                                                          
                //                      ,nvl(a.KRA_GPAN,'NO PAN') GAURDIAN_PAN                                                          
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER                                                       
                //                      ,a.NOMINEEFLAG                                                
                //                     ,a.KYCFLAG                                                            
                //                      ,a.AADHARSEEDINGFLAG                                                 
                //                    ,a.BANK_FLAG                                                       
                //                     ,A.AGENT ARN_CODE                                                   
                //                    ,A.AGENTNAME ARN_NAME
                //                    ,a.selected_empid EMPLOYEECODE
                //                    ,c.NAME EMPLOYEENAME
                //                ,upper(trim(replace(replace(c.LOCATIONNAME_ORIG,'UFC',''),'',''))) LOCATION
                //                FROM mistest.KYC_REMED_BASE_DAT a
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.UFC_NAME_DNQ))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),'','')))
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  c  ON upper(Trim(a.selected_empid)) = upper(Trim(c.employeecode))
                //                WHERE b.employeecode=:P_EMPID
                //                AND nvl(selected_rec,'N')='S' and trunc(selected_dt) between :p_from_date and :p_to_date
                //            or REGEXP_LIKE(upper(trim(CONCAT_ADD||agent||INVNAME)),upper(trim(:P_SEARCH_TEXT)))
                //                ORDER BY selected_dt DESC";

                //return @"SELECT                                                
                //                      a.SRNO                                                   
                //                      ,a.AUM_Bracket AUM_BRACKET                                                 
                //                      ,a.zone_uti zone                                                 
                //                      ,region_name_uti region                                                  
                //                      ,a.ufc_name                                                         
                //                      ,a.ACNO                                                  
                //                      ,INVNAME FIRST_HOLDER_NAME                                               
                //                      ,a.JT1 SECOND_HOLDER_NAME                                                   
                //                      ,a.Jt2 THIRD_HOLDER_NAME                                                        
                //                      ,a.DNR_NAME DONOR_NAME                                                      
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN                                               
                //                      ,nvl(a.KRA_PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS                                                            
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN                                                  
                //                      ,nvl(a.KRA_PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS                                                    
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN                                                       
                //                      ,nvl(a.KRA_PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS                                                        
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN                                                  
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS                                                          
                //                      ,nvl(a.KRA_GPAN,'NO PAN') GAURDIAN_PAN                                                         
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER                                                      
                //                      ,a.NOMINEEFLAG                                               
                //                     ,a.KYCFLAG                                                           
                //                      ,a.AADHARSEEDINGFLAG                                                
                //                    ,a.BANK_FLAG                                                      
                //                     ,A.AGENT ARN_CODE                                                  
                //                    ,A.AGENTNAME ARN_NAME
                //                    ,a.selected_empid EMPLOYEECODE
                //                    ,c.NAME EMPLOYEENAME
                //                ,upper(trim(replace(replace(c.LOCATIONNAME_ORIG,'UFC',''),'',''))) LOCATION
                //                FROM mistest.KYC_REMED_BASE_DAT a
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.UFC_NAME_DNQ))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),'','')))
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  c  ON upper(Trim(a.selected_empid)) = upper(Trim(c.employeecode))
                //                WHERE b.employeecode=:P_EMPID
                //                AND nvl(selected_rec,'N')='S' and (trunc(selected_dt) between :p_from_date and :p_to_date
                //                    and
                //                    case when :P_SEARCH_TEXT is null then 'VALID'
                //                   when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                //                    then 'VALID' else null end ='VALID')
                //                ORDER BY selected_dt DESC";

                // New Query 
                //return @"SELECT                                                
                //                      a.SRNO
                //                       ,a.AUM_Bracket AUM_BRACKET
                //                      ,a.ACNO
                //                      ,INVNAME FIRST_HOLDER_NAME
                //                      ,a.JT1 SECOND_HOLDER_NAME
                //                      ,a.Jt2 THIRD_HOLDER_NAME
                //                      ,a.DNR_NAME DONOR_NAME
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                //                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                //                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                //                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //                      ,a.AUM AUM_OF_THE_FOLIO
                //                      ,A.AGENT ARN_CODE
                //                      ,A.AGENTNAME ARN_NAME
                //                      ,a.MOBILE AS MOBILE
                //                      ,a.EMAIL AS EMAIL
                //                      ,a.INVESTED_SCHEMES
                //                      ,b.LOCATIONNAME_ORIG AS LOCATION
                //                      ,a.NOMINEEFLAG                                               
                //                      ,a.KYCFLAG                                                           
                //                      ,a.AADHARSEEDINGFLAG                                                
                //                      ,a.BANK_FLAG     
                //                    ,a.selected_empid EMPLOYEECODE
                //                    ,c.NAME EMPLOYEENAME
                //                ,upper(trim(replace(replace(c.LOCATIONNAME_ORIG,'UFC',''),'',''))) LOCATION
                //                FROM mistest.KYC_REMED_BASE_DAT a
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.UFC_NAME_DNQ))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),'','')))
                //                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  c  ON upper(Trim(a.selected_empid)) = upper(Trim(c.employeecode))
                //                WHERE b.employeecode=:P_EMPID
                //                AND nvl(selected_rec,'N')='S' and (trunc(selected_dt) between :p_from_date and :p_to_date
                //                    and
                //                    case when :P_SEARCH_TEXT is null then 'VALID'
                //                    when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                //                    then 'VALID' and  
                //                    case when : P_AUM_BRACKET is null then 'VALID'
                //                   when : P_AUM_BRACKET is not null and  (a.AUM_BRACKET =:P_AUM_BRACKET)
                //                    then 'VALID'    else null end ='VALID') ORDER BY selected_dt DESC";

                // New Query1
                return @"  SELECT                                                
                                      a.SRNO
                                       ,a.AUM_Bracket AUM_BRACKET
                                      ,a.ACNO
                                      ,INVNAME FIRST_HOLDER_NAME
                                      ,a.JT1 SECOND_HOLDER_NAME
                                      ,a.Jt2 THIRD_HOLDER_NAME
                                      ,a.DNR_NAME DONOR_NAME
                                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                                      ,a.AUM AUM_OF_THE_FOLIO
                                      ,A.AGENT ARNCODE
                                      ,A.AGENTNAME
                                      ,a.MOBILE AS MOBILE
                                      ,a.EMAIL AS EMAIL
                                      ,a.INVESTED_SCHEMES
                                      ,b.LOCATIONNAME_ORIG AS LOCATION
                                      ,a.NOMINEEFLAG                                               
                                      ,a.KYCFLAG                                                           
                                      ,a.AADHARSEEDINGFLAG                                                
                                      ,a.BANK_FLAG     
                                    ,a.selected_empid EMPLOYEECODE
                                    ,b.NAME EMPLOYEENAME
                                ,upper(trim(replace(replace(c.LOCATIONNAME_ORIG,'UFC',''),'',''))) LOCATION
                                FROM mistest.KYC_REMED_BASE_DAT a
                                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON upper(trim(a.UFC_NAME_DNQ))=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),'','')))
                                LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  c  ON upper(Trim(a.selected_empid)) = upper(Trim(c.employeecode))
                                WHERE b.employeecode=:P_EMPID
                                AND nvl(selected_rec,'N')='S' and (trunc(selected_dt) between :p_from_date and :p_to_date
                                    and
                                    case when :P_SEARCH_TEXT is null then 'VALID'
                                    when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD)),:P_SEARCH_TEXT)
                                    then 'VALID' else null end ='VALID') ORDER BY selected_dt DESC";
            }
        }


        public static string KYC_DATA_LIMIT_CHECK_QUERY_FOR_UPDATE
        {
            get
            {
                return @"select count(*) from mistest.kyc_remed_base_dat where SELECTED_REC='S' and selected_empid=:p_employee_code and trunc(selected_dt) between :p_from_date and :p_to_date";
            }
        }

        public static string GetEmpcodeName
        {
            get
            {
                //return @"select EMPLOYEECODE Code, NAME Name FROM
                //        mistest.EMPLOYEE_LIST_FOR_KYC where locationname_orig in
                //        (select locationname_orig from EMPLOYEE_LIST_FOR_KYC  where employeecode=:p_EMPCODE )";

                return @"select EMPLOYEECODE Code, NAME Name, EMAIL_ID EMAIL_ID FROM
                        mistest.EMPLOYEE_LIST_FOR_KYC where locationname_orig in
                        (select locationname_orig from EMPLOYEE_LIST_FOR_KYC  where employeecode=:p_EMPCODE)";
            }
        }


        public static string UpdateStatusFolio_Grid
        {
            get
            {
                //return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                //                FROM mistest.KYC_REMED_BASE_DAT a 
                //                WHERE ";


                /* return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                         a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                                 FROM mistest.KYC_REMED_BASE_DAT a
                                       inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                                       left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                                     where
                                     case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                                     when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) 
                                     and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID'
                                     else 'XXXX'
                                     end='VALID'
                                     and nvl(a.selected_rec, 'N') =  'S'
                                     and (trunc(a.selected_dt) =:P_DATE
                                     or upper(Trim(a.ACNO)) = :P_ACNO
                                     or REGEXP_LIKE(upper(Trim(a.INVNAME)),:P_INVNAME))";
                 */

                //Added By Bhaneshvar
                    return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                                FROM mistest.KYC_REMED_BASE_DAT a
                                      inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                                      left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                                    where
                                    case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                                    when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID' 
                                    when nvl(a.selected_empid,'XXXX')=:p_employee_code then 'VALID'
                                    else 'XXXX'
                                    end='VALID'
                                    and (trunc(a.selected_dt) =:P_DATE
                                    or upper(Trim(a.ACNO)) = :P_ACNO
                                    or REGEXP_LIKE(UPPER(TRIM(a.INVNAME) || TRIM(a.AGENT)),UPPER(:P_INVNAME)))";


            }
        }

        public static string UpdateStatusFolio_Grid__date_and_Emp_code
        {
            get
            {
                //return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                //                FROM mistest.KYC_REMED_BASE_DAT a 
                //                WHERE ";


                return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                                FROM mistest.KYC_REMED_BASE_DAT a
                                      inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                                      left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                    where
                    case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                    when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID'
                    else 'XXXX'
                    end='VALID'
                    and nvl(a.selected_rec, 'N') =  'S'
                    and (trunc(a.selected_dt) =:P_DATE)";
            }
        }

        public static string UpdateStatusFolio_Grid__INVNAME
        {
            get
            {
                //return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                //                FROM mistest.KYC_REMED_BASE_DAT a 
                //                WHERE ";


                return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                                FROM mistest.KYC_REMED_BASE_DAT a
                                      inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                                      left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                    where
                    case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                    when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID'
                    else 'XXXX'
                    end='VALID'
                    and nvl(a.selected_rec, 'N') =  'S'
                    and (trunc(a.selected_dt) =:P_DATE
                    or REGEXP_LIKE(upper(Trim(a.INVNAME)),:P_INVNAME))";
            }
        }

        public static string UpdateStatusFolio_Grid__ACNO
        {
            get
            {
                //return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                //        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                //                FROM mistest.KYC_REMED_BASE_DAT a 
                //                WHERE ";


                return @"SELECT a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                        a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.remark_code REMARK_CODE, a.remark_dt REMARK_DATE, a.ACNO
                                FROM mistest.KYC_REMED_BASE_DAT a
                                      inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                                      left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                    where
                    case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                    when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID'
                    else 'XXXX'
                    end='VALID'
                    and nvl(a.selected_rec, 'N') =  'S'
                    and (trunc(a.selected_dt) =:P_DATE
                    or upper(Trim(a.ACNO)) = :P_ACNO)";
            }
        }
        public static string GetRemarkListQuery
        {
            get
            {
                return @"select slno SLNO, r_desc Description from mistest.KYC_REMARKS_MASTER ORDER BY slno";
            }
        }

        public static string Update_QUERY_StatusFolio
        {
            get
            {
                //return @"Update mistest.KYC_REMED_BASE_DAT set remark_code=:P_REMARK_CODE, remark_dt = :P_REMARK_DATE where SRNO = :P_SRNO";
                return @"Update mistest.KYC_REMED_BASE_DAT set remark_code=:P_REMARK_CODE, remark_dt = :P_REMARK_DATE, REMARK_COMMENT = :P_REMARK_COMMENT where SRNO = :P_SRNO";


            }
        }
        public static string Get_grid_KYC_Record_Status_Abridged
        {
            get
            {
                //return @"SELECT
                //                        a.AUM_Bracket AS AUM_BRACKET
                //                      ,a.KYCFLAG AS KYCFLAG
                //                      ,a.NOMINEEFLAG AS NOMINEEFLAG
                //                      ,a.AADHARSEEDINGFLAG AS AADHARSEEDINGFLAG
                //                     ,a.BANK_FLAG AS BANK_FLAG
                //                      ,a.selected_empid AS SELECTED_EMPID
                //                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) AS EMPLOYEENAME
                //                        ,trunc(selected_dt)  AS SELECTION_DATE
                //                      ,a.ufc_name AS UFC_NAME
                //                      ,a.region_name_uti AS REGIONDESC
                //                      ,a.zone_uti AS ZONEDESC
                //                    ,case when nvl(a.selected_rec,'N')='N' then 'To Be Selected'
                //                    when nvl(a.selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                //                    when nvl(a.selected_rec,'N')='K' then 'KFIND - KYC Validation Confirmation in Progress'
                //                    when nvl(a.selected_rec,'N')='S' then 'Selected'
                //                    when nvl(a.selected_rec,'N')='R' then 'Remediated'
                //                    when nvl(a.selected_rec,'N')='V' then 'CVL Records shared with Zones'
                //                    when nvl(a.selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                //                    when nvl(a.selected_rec,'N')='T' then 'Folios with Transmission Done' end AS FOLIO_STATUS
                //                    ,trunc(sysdate)-trunc(selected_dt) AS DAYS_OF_SELECTION
                //                      ,a.ACNO AS ACNO
                //                      ,a.INVNAME AS FIRST_HOLDER_NAME
                //                      ,a.CONCAT_ADD AS ADDRESS_OF_FIRST_HOLDER
                //                    ,a.AGENT  AS ARN_CODE
                //                    ,a.AGENTNAME  AS    ARN_NAME
                //                    ,a.AGNETMOBILE AS  ARN_MOBILE
                //                    ,a.AGENTEMAIL   AS ARN_EMAIL
                //                    ,a.SRNO AS SERIAL_NO
                //                      FROM mistest.KYC_REMED_BASE_DAT a
                //                      left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE
                //                      inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //                      left outer join (select ufc_code,ufc_name from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on a.ufccode=um.ufc_code
                //    where
                //   case when (ek.emp_role='CM' and upper(trim(replace(replace(EK.LOCATIONNAME_ORIG,'UFC',''),' ','')))=upper(trim(replace(a.UFC_NAME_DNQ,' ','')))) then 'VALID'
                //    when (upper(trim(a.JURISDICTION))=upper(trim(replace(replace(EK.LOCATIONNAME,'UFC',''),'',''))) and nvl(a.selected_empid,'XXXX')=:p_employee_code) then 'VALID'
                //    else 'XXXX'
                //    end='VALID'
                //    and trunc(a.selected_dt) between :p_from_date and :p_to_date";


                //return @" SELECT          a.AUM_Bracket AS AUM_BRACKET
                //                      ,a.KYCFLAG AS KYCFLAG
                //                      ,a.NOMINEEFLAG AS NOMINEEFLAG
                //                      ,a.AADHARSEEDINGFLAG AS AADHARSEEDINGFLAG
                //                     ,a.BANK_FLAG AS BANK_FLAG
                //                      ,a.selected_empid AS SELECTED_EMPID
                //                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) AS EMPLOYEENAME
                //                        ,trunc(selected_dt)  AS SELECTION_DATE
                //                      ,a.ufc_name AS UFC_NAME
                //                      ,a.region_name_uti AS REGIONDESC
                //                      ,a.zone_uti AS ZONEDESC
                //                    ,case when nvl(a.selected_rec,'N')='N' then 'To Be Selected'
                //                    when nvl(a.selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                //                    when nvl(a.selected_rec,'N')='K' then 'KFIND - KYC Validation Confirmation in Progress'
                //                    when nvl(a.selected_rec,'N')='S' then 'Selected'
                //                    when nvl(a.selected_rec,'N')='R' then 'Remediated'
                //                    when nvl(a.selected_rec,'N')='V' then 'CVL Records shared with Zones'
                //                    when nvl(a.selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                //                    when nvl(a.selected_rec,'N')='T' then 'Folios with Transmission Done' end AS FOLIO_STATUS
                //                    ,trunc(sysdate)-trunc(selected_dt) AS DAYS_OF_SELECTION
                //                      ,a.ACNO AS ACNO
                //                      ,a.INVNAME AS FIRST_HOLDER_NAME
                //                      ,a.CONCAT_ADD AS ADDRESS_OF_FIRST_HOLDER
                //                    ,a.AGENT  AS ARN_CODE
                //                    ,a.AGENTNAME  AS    ARN_NAME
                //                    ,a.AGNETMOBILE AS  ARN_MOBILE
                //                    ,a.AGENTEMAIL   AS ARN_EMAIL
                //                    ,a.SRNO AS SERIAL_NO
                //                      FROM mistest.KYC_REMED_BASE_DAT a
                //                      left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE";

                return @"SELECT          a.AUM_Bracket AS AUM_BRACKET
                                      ,a.KYCFLAG AS KYCFLAG
                                      ,a.NOMINEEFLAG AS NOMINEEFLAG
                                      ,a.AADHARSEEDINGFLAG AS AADHARSEEDINGFLAG
                                     ,a.BANK_FLAG AS BANK_FLAG
                                      ,a.selected_empid AS SELECTED_EMPID
                                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) AS EMPLOYEENAME
                                        ,trunc(selected_dt)  AS SELECTION_DATE
                                      ,a.ufc_name AS UFC_NAME
                                      ,a.region_name_uti AS REGIONDESC
                                      ,a.zone_uti AS ZONEDESC
                                   ,case when nvl(a.selected_rec,'N')='N' then 'To Be Selected'
                                      when nvl(a.selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                                      when nvl(a.selected_rec,'N')='K' then 'KFIND - KYC Validation Confirmation in Progress'
                                      when nvl(a.selected_rec,'N')='S' then 'Selected'
                                      when nvl(a.selected_rec,'N')='R' then 'Remediated'
                                      when nvl(a.selected_rec,'N')='V' then 'CVL Records shared with Zones'
                                      when nvl(a.selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                                      when nvl(a.selected_rec,'N')='D' then 'Shared with Department of Posts'
                                      when nvl(a.selected_rec,'N')='T' then 'Folios with Transmission Done' end AS FOLIO_STATUS
                                    ,trunc(sysdate)-trunc(selected_dt) AS DAYS_OF_SELECTION
                                      ,a.ACNO AS ACNO
                                      ,a.INVNAME AS FIRST_HOLDER_NAME
                                      ,a.CONCAT_ADD AS ADDRESS_OF_FIRST_HOLDER
                                    ,a.AGENT  AS ARN_CODE
                                    ,a.AGENTNAME  AS    ARN_NAME
                                    ,a.AGNETMOBILE AS  ARN_MOBILE
                                    ,a.AGENTEMAIL   AS ARN_EMAIL
                                    ,a.SRNO AS SERIAL_NO
                                    ,a.PRE_POST_2008 AS PRE_POST_2008
                                      FROM mistest.KYC_REMED_BASE_DAT a";
            }
        }


        public static string Aum_Bracket_list_Query
        {
            get
            {
                return @"select * from mistest.KYC_AUM_BRACKET";
            }
        }
        public static string Get_CountWise_details_Zonal
        {
            get
            {
                return @"SELECT                          
                                KRM.R_DESC AS ACTION_UPDATE
                             ,a.REMARK_DT As DATE_OF_LATEST_ACTION
                             ,a.REMARK_COMMENT As LATEST_ACTION_NOTE
                                        ,a.AUM_Bracket AS AUM_BRACKET
                                      ,a.KYCFLAG AS KYCFLAG
                                      ,a.NOMINEEFLAG AS NOMINEEFLAG
                                      ,a.AADHARSEEDINGFLAG AS AADHARSEEDINGFLAG
                                     ,a.BANK_FLAG AS BANK_FLAG
                                      ,a.selected_empid AS SELECTED_EMPID
                                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode=a.selected_empid) AS EMPLOYEENAME
                                        ,trunc(selected_dt)  AS SELECTION_DATE
                                      ,a.ufc_name AS UFC_NAME
                                      ,a.region_name_uti AS REGIONDESC
                                      ,a.zone_uti AS ZONEDESC
                                    ,case when nvl(a.selected_rec,'N')='N' then 'To Be Selected'
                                    when nvl(a.selected_rec,'N')='C' then 'CAMS KYC List - To be Confirmed from OPS'
                                   when nvl(a.selected_rec,'N')='K' then 'KFIND - KYC Validation Confirmation in Progress'
                                    when nvl(a.selected_rec,'N')='S' then 'Selected'
                                    when nvl(a.selected_rec,'N')='R' then 'Remediated'
                                    when nvl(a.selected_rec,'N')='V' then 'CVL Records shared with Zones'
                                    when nvl(a.selected_rec,'N')='W' then 'CVL Records shared with VRMs'
                                    when nvl(a.selected_rec,'N')='T' then 'Folios with Transmission Done' end AS FOLIO_STATUS
                                    ,trunc(sysdate)-trunc(selected_dt) AS DAYS_OF_SELECTION
                                      ,a.ACNO AS ACNO
                                      ,a.INVNAME AS FIRST_HOLDER_NAME
                                      ,a.CONCAT_ADD AS ADDRESS_OF_FIRST_HOLDER
                                    ,a.AGENT  AS ARN_CODE
                                    ,a.AGENTNAME  AS    ARN_NAME
                                    ,a.AGNETMOBILE AS  ARN_MOBILE
                                    ,a.AGENTEMAIL   AS ARN_EMAIL
                                    ,a.SRNO AS SERIAL_NO
                                      FROM mistest.KYC_REMED_BASE_DAT a
                                      LEFT OUTER JOIN MISTEST.KYC_REMARKS_MASTER KRM on a.remark_code=KRM.SLNO
                                      left outer join (select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH=bm.BRANCH_CODE";
            }
        }


        public static string GetHistoryPopup_Query
        {
            get
            {
                return @"select distinct remark_code REMARK_CODE,remark_dt REMARK_DT,remark_comment REMARK_COMMENT from mistest.kyc_remed_base_tab_dtl where acno=:p_acno";
            }
        }
        public static string KYC_DATA_DOWNLOAD_QUERY_FolioWise
        {
            get
            {
                //return @" SELECT                             a.SRNO
                //                       ,a.AUM_Bracket AUM_BRACKET
                //                      ,a.ACNO
                //                      ,INVNAME FIRST_HOLDER_NAME
                //                      ,a.JT1 SECOND_HOLDER_NAME
                //                      ,a.Jt2 THIRD_HOLDER_NAME
                //                      ,a.DNR_NAME DONOR_NAME
                //                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                //                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                //                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                //                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                //                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                //                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                //                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                //                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                //                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                //                      ,a.AUM AUM_OF_THE_FOLIO
                //                      ,A.AGENT ARNCODE
                //                      ,A.AGENTNAME
                //                      ,a.MOBILE AS MOBILE
                //                      ,a.EMAIL AS EMAIL
                //                      ,a.INVESTED_SCHEMES
                //                      ,b.LOCATIONNAME_ORIG AS LOCATION
                //                      ,a.NOMINEEFLAG                                               
                //                      ,a.KYCFLAG                                                           
                //                      ,a.AADHARSEEDINGFLAG                                                
                //                      ,a.BANK_FLAG     
                //                    ,a.selected_empid EMPLOYEECODE
                //                    ,b.NAME EMPLOYEENAME
                //                      FROM mistest.KYC_REMED_BASE_DAT a
                //                      LEFT OUTER JOIN EMPLOYEE_LIST_FOR_KYC B ON A.SELECTED_EMPID=B.EMPLOYEECODE
                //                    WHERE SELECTED_EMPID=:P_EMPID
                //                    AND nvl(selected_rec,'N')='S' 
                //                    and a.ACNO in :p_folio_no
                //                    ORDER BY selected_dt DESC";

                return @"SELECT       a.SRNO
                                      ,a.AUM_Bracket AUM_BRACKET
                                      ,a.ACNO
                                      ,INVNAME FIRST_HOLDER_NAME
                                      ,a.JT1 SECOND_HOLDER_NAME
                                      ,a.Jt2 THIRD_HOLDER_NAME
                                      ,a.DNR_NAME DONOR_NAME
                                      ,nvl(a.PAN,'NO PAN') FIRST_HOLDER_PAN
                                      ,nvl(a.PAN_KYCSTATUS,'NOT COMPLIED') FIRST_HOLDER_KYC_STATUS
                                      ,nvl(a.JT1PAN,'NO PAN') SECOND_HOLDER_PAN
                                      ,nvl(a.PAN1_KYCSTATUS,'NOT COMPLIED') SECOND_HOLDER_KYC_STATUS
                                      ,nvl(a.JT2PAN,'NO PAN') THIRD_HOLDER_PAN
                                      ,nvl(a.PAN2_KYCSTATUS,'NOT COMPLIED') THIRD_HOLDER_KYC_STATUS
                                      ,nvl(a.DNR_PAN,'NO PAN') DONOR_PAN
                                      ,nvl(a.DNR_PAN_KYCSTATUS,'NOT COMPLIED') DONOR_KYC_STATUS
                                      ,nvl(a.GPAN,'NO PAN') GAURDIAN_PAN
                                      ,a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER
                                      ,a.AUM AUM_OF_THE_FOLIO
                                      ,A.AGENT ARNCODE
                                      ,A.AGENTNAME
                                      ,a.MOBILE AS MOBILE
                                      ,a.EMAIL AS EMAIL
                                      ,a.INVESTED_SCHEMES
                                      ,b.LOCATIONNAME_ORIG AS LOCATION
                                      ,a.NOMINEEFLAG                                              
                                      ,a.KYCFLAG                                                          
                                      ,a.AADHARSEEDINGFLAG                                                
                                      ,a.BANK_FLAG    
                                    ,a.selected_empid EMPLOYEECODE
                                    ,b.NAME EMPLOYEENAME
                                      FROM mistest.KYC_REMED_BASE_DAT a
                                      LEFT OUTER JOIN EMPLOYEE_LIST_FOR_KYC B ON A.SELECTED_EMPID=B.EMPLOYEECODE
                                   WHERE nvl(a.selected_rec,'N')  in (select SELECTED_REC from mistest.KYC_RECORD_STATUS)
                                    and a.ACNO in :p_folio_no
                                    ORDER BY a.agent||a.invname||a.concat_add DESC";
            }
        }


        //Reason Wise Report Query
        public static string GetPopupReasonWiseRpt_Query
        {
            get
            {
                return @"SELECT                           
                                        KRM.R_DESC AS ACTION_UPDATE,
                                         KRM.SLNO as REMARK_CODE   
                                        ,a.remark_dt AS REMARK_DT
                                        ,a.REMARK_COMMENT AS REMARK_COMMENT
                                        ,a.AUM_Bracket AS AUM_BRACKET
                                      ,a.KYCFLAG AS KYCFLAG
                                      ,a.NOMINEEFLAG AS NOMINEEFLAG
                                      ,a.AADHARSEEDINGFLAG AS AADHARSEEDINGFLAG
                                     ,a.BANK_FLAG AS BANK_FLAG
                                      ,a.selected_empid AS SELECTED_EMPID
                                      ,(select name from EMPLOYEE_LIST_FOR_KYC where employeecode = a.selected_empid) AS EMPLOYEENAME
                                          , trunc(selected_dt)  AS SELECTION_DATE
                                         , a.ufc_name AS UFC_NAME
                                      ,a.region_name_uti AS REGIONDESC
                                      ,a.zone_uti AS ZONEDESC
                                    ,case when nvl(a.selected_rec,'N')= 'N' then 'To Be Selected'
                                    when nvl(a.selected_rec,'N')= 'C' then 'CAMS KYC List - To be Confirmed from OPS'
                                    when nvl(a.selected_rec,'N')= 'K' then 'KFIND - KYC Validation Confirmation in Progress'
                                    when nvl(a.selected_rec,'N')= 'S' then 'Selected'
                                    when nvl(a.selected_rec,'N')= 'R' then 'Remediated'
                                    when nvl(a.selected_rec,'N')= 'V' then 'CVL Records shared with Zones'
                                    when nvl(a.selected_rec,'N')= 'W' then 'CVL Records shared with VRMs'
                                    when nvl(a.selected_rec,'N')= 'T' then 'Folios with Transmission Done' end AS FOLIO_STATUS
                                    ,trunc(sysdate) - trunc(selected_dt) AS DAYS_OF_SELECTION
                                        , a.ACNO AS ACNO
                                      ,a.INVNAME AS FIRST_HOLDER_NAME
                                      ,a.CONCAT_ADD AS ADDRESS_OF_FIRST_HOLDER
                                    ,a.AGENT AS ARN_CODE
                                    ,a.AGENTNAME AS    ARN_NAME
                                    ,a.AGNETMOBILE AS  ARN_MOBILE
                                    ,a.AGENTEMAIL AS ARN_EMAIL
                                    ,a.SRNO AS SERIAL_NO
                                      FROM mistest.KYC_REMED_BASE_DAT a
                                      LEFT OUTER JOIN MISTEST.KYC_REMARKS_MASTER KRM on a.remark_code = KRM.SLNO
                                      left outer join(select * from icc.branch_master) bm on a.FOLIO_MIN_BRANCH = bm.BRANCH_CODE";
            }
        }


        public static string GetGridReasonwiseRpt_Query
        {
            get
            {
                //return @"select
                //a.zone_uti as ZONE,a.region_name_uti as REGION,a.ufc_name as UFC,nvl(a.selected_empid,'NOT SELECTED') as EMPLOYEEID,
                //(select nvl(name,'NOT SELECTED') from EMPLOYEE_LIST_FOR_KYC where employeecode=nvl(a.selected_empid,'NOT SELECTED')) AS EMPNAME,
                //a.remark_code AS REMARK_CODE,
                //(select r_desc from mistest.kyc_remarks_master where slno=a.remark_code) AS REMARK_DESC,
                //count(a.acno) AS FOLIO_COUNT,
                //sum(case when nvl(a.remark_code,99) <> 99 and trunc(a.remark_dt) <=:p_report_date and nvl(trunc(a.remark_dt),'31-DEC-9999') <> '31-DEC-9999' then 1 else 0 end) AS REMARK_COUNT
                //from mistest.KYC_REMED_BASE_DAT a
                //inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id
                //left outer join (select ufc_code,ufc_name,region_code from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um on cu.location=um.ufc_code
                //left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code
                //where
                //case
                //When ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','2372','4471','4488','4594','8000','4237','4228','2179') then 'VALID'
                // when ((upper(trim(ek.emp_role))='ZH' and a.zone_uti=cu.zone) or
                // (upper(trim(ek.emp_role))='RH' and  a.region_name_uti=rmz.region_name and a.zone_uti=cu.zone)
                // or (upper(trim(ek.emp_role))='CM' and a.region_name_uti=rmz.region_name and a.zone_uti=cu.zone  and a.ufc_NAME=um.UFC_NAME)
                //or nvl(upper(trim(a.selected_empid)),'ZZZ')=:p_employee_code) then 'VALID' else 'ALL' end='VALID'
                //and a.remark_code is not null
                //group by a.ufc_name,a.region_name_uti,a.zone_uti,nvl(a.selected_empid,'NOT SELECTED'),
                //a.remark_code
                //order by a.zone_uti,a.region_name_uti,a.ufc_name,nvl(a.selected_empid,'NOT SELECTED')";

                //NEW ONE BY NADEEM SIR

                return @"SELECT *
                        FROM (
                          SELECT
                            a.zone_uti AS ZONE,
                            --a.region_name_uti AS REGION,
                            rmk.r_desc AS REMARK_DESC,
                            COUNT(a.acno) AS FOLIO_COUNT
                          FROM
                            mistest.KYC_REMED_BASE_DAT a
                            INNER JOIN mistest.EMPLOYEE_LIST_FOR_KYC EK ON :p_employee_code = EK.EMPLOYEECODE
                            LEFT OUTER JOIN (
                              SELECT emp_id, emp_role, location, region_code, zone
                              FROM dynamic.MIS_CUSER_LOGS_KYC
                              WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto) cu ON ek.employeecode = cu.emp_id
                            LEFT OUTER JOIN (SELECT ufc_code, ufc_name, region_code
                              FROM mis0910.ufc_mast
                              WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto) um ON cu.location = um.ufc_code
                            LEFT OUTER JOIN (SELECT region_code, region_name
                              FROM mis0910.region_mast
                              WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto) rmz ON cu.region_code = rmz.region_code
                            INNER JOIN mistest.kyc_remarks_master rmk ON rmk.slno = a.remark_code
                          WHERE
                            CASE
                              WHEN ek.employeecode IN ('2824','1287','3010','3195','8500','4426','4517','2372','4471','4488','4594','8000','4237','4228','2179') THEN 'VALID'
                              WHEN (
                                (UPPER(TRIM(ek.emp_role)) = 'ZH' AND a.zone_uti = cu.zone) OR
                                (UPPER(TRIM(ek.emp_role)) = 'RH' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone) OR
                                (UPPER(TRIM(ek.emp_role)) = 'CM' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone AND a.ufc_NAME = um.UFC_NAME) OR
                                NVL(UPPER(TRIM(a.selected_empid)), 'ZZZ') = :p_employee_code) THEN 'VALID'
                              ELSE 'ALL'
                            END = 'VALID'
                            AND a.remark_code IS NOT NULL
                          GROUP BY
                            a.zone_uti,
                            --a.region_name_uti,
                            rmk.r_desc)
                        PIVOT (SUM(FOLIO_COUNT)
                          FOR REMARK_DESC IN (    
	                        'Customer shifted from given address & no further contact established' as REASON1
                        ,'Death reported for primary applicant' AS REASON2
                        ,'Death reported for joint applicants' AS REASON3
                        ,'Transmission reported by customer'  AS REASON4
                        ,'No address found' AS REASON5
                        ,'Account Frozen for legal case or law enforcement agencies'  AS REASON6
                        ,'Account Frozen as per SEBI Order or Other Security maket agencies'  AS REASON7
                        ,'Customer shifted from given address & contact established'  AS REASON8
                        ,'Visited address - customer not available' AS REASON9
                        ,'Customer will visit Branch/ MFD for updating the details' AS REASON10
                        ,'Visited/ Contacted customer - KYC & Documents (Bank Details, Nominee) submitted for remediation'  AS REASON11
                        ,'Death reported for any applicant (Transmission procedure completed in the folio)' AS REASON12
                          )
                        )
                        ORDER BY ZONE";

            }
        }
        public static string GetGridReasonwiseRpt_Query_Region
        {
            get
            {
                return @"SELECT *
                                FROM (
                                  SELECT
                                    a.zone_uti AS ZONE,
                                    a.region_name_uti AS REGION,
                                    rmk.r_desc AS REMARK_DESC,
                                    COUNT(a.acno) AS FOLIO_COUNT
                                  FROM
                                    mistest.KYC_REMED_BASE_DAT a
                                    INNER JOIN mistest.EMPLOYEE_LIST_FOR_KYC EK ON :p_employee_code = EK.EMPLOYEECODE
                                    LEFT OUTER JOIN (
                                      SELECT emp_id, emp_role, location, region_code, zone
                                      FROM dynamic.MIS_CUSER_LOGS_KYC
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto
                                    ) cu ON ek.employeecode = cu.emp_id
                                    LEFT OUTER JOIN (
                                      SELECT ufc_code, ufc_name, region_code
                                      FROM mis0910.ufc_mast
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto
                                    ) um ON cu.location = um.ufc_code
                                    LEFT OUTER JOIN (
                                      SELECT region_code, region_name
                                      FROM mis0910.region_mast
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto
                                    ) rmz ON cu.region_code = rmz.region_code
                                    INNER JOIN mistest.kyc_remarks_master rmk ON rmk.slno = a.remark_code
                                  WHERE
                                    CASE
                                      WHEN ek.employeecode IN ('2824','1287','3010','3195','8500','4426','4517','2372','4471','4488','4594','8000','4237','4228','2179') THEN 'VALID'
                                      WHEN (
                                        (UPPER(TRIM(ek.emp_role)) = 'ZH' AND a.zone_uti = cu.zone) OR
                                        (UPPER(TRIM(ek.emp_role)) = 'RH' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone) OR
                                        (UPPER(TRIM(ek.emp_role)) = 'CM' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone AND a.ufc_NAME = um.UFC_NAME) OR
                                        NVL(UPPER(TRIM(a.selected_empid)), 'ZZZ') = :p_employee_code
                                      ) THEN 'VALID'
                                      ELSE 'ALL'
                                    END = 'VALID'
                                    AND a.remark_code IS NOT NULL
                                    AND A.ZONE_UTI =:p_zone
                                  GROUP BY
                                    a.zone_uti,
                                    A.REGION_NAME_UTI,
                                    rmk.r_desc
                                )
                                PIVOT (
                                  SUM(FOLIO_COUNT)
                                  FOR REMARK_DESC IN (
    
                                'Customer shifted from given address & no further contact established' as REASON1
                                ,'Death reported for primary applicant' AS REASON2
                                ,'Death reported for joint applicants' AS REASON3
                                ,'Transmission reported by customer'  AS REASON4
                                ,'No address found' AS REASON5
                                ,'Account Frozen for legal case or law enforcement agencies'  AS REASON6
                                ,'Account Frozen as per SEBI Order or Other Security maket agencies'  AS REASON7
                                ,'Customer shifted from given address & contact established'  AS REASON8
                                ,'Visited address - customer not available' AS REASON9
                                ,'Customer will visit Branch/ MFD for updating the details' AS REASON10
                                ,'Visited/ Contacted customer - KYC & Documents (Bank Details, Nominee) submitted for remediation'  AS REASON11
                                ,'Death reported for any applicant (Transmission procedure completed in the folio)' AS REASON12
                                  )
                                )
                                ORDER BY ZONE";

            }
        }
        public static string GetGridReasonwiseRpt_Query_UFC
        {
            get
            {
                return @"SELECT *
                                FROM (
                                  SELECT
                                    a.zone_uti AS ZONE,
                                    a.region_name_uti AS REGION,
                                    a.ufc_name as UFC_NAME,
                                    rmk.r_desc AS REMARK_DESC,
                                    COUNT(a.acno) AS FOLIO_COUNT
                                  FROM
                                    mistest.KYC_REMED_BASE_DAT a
                                    INNER JOIN mistest.EMPLOYEE_LIST_FOR_KYC EK ON :p_employee_code = EK.EMPLOYEECODE
                                    LEFT OUTER JOIN (
                                      SELECT emp_id, emp_role, location, region_code, zone
                                      FROM dynamic.MIS_CUSER_LOGS_KYC
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto
                                    ) cu ON ek.employeecode = cu.emp_id
                                    LEFT OUTER JOIN (
                                      SELECT ufc_code, ufc_name, region_code
                                      FROM mis0910.ufc_mast
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto
                                    ) um ON cu.location = um.ufc_code
                                    LEFT OUTER JOIN (
                                      SELECT region_code, region_name
                                      FROM mis0910.region_mast
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto
                                    ) rmz ON cu.region_code = rmz.region_code
                                    INNER JOIN mistest.kyc_remarks_master rmk ON rmk.slno = a.remark_code
                                  WHERE
                                    CASE
                                      WHEN ek.employeecode IN ('2824','1287','3010','3195','8500','4426','4517','2372','4471','4488','4594','8000','4237','4228','2179') THEN 'VALID'
                                      WHEN (
                                        (UPPER(TRIM(ek.emp_role)) = 'ZH' AND a.zone_uti = cu.zone) OR
                                        (UPPER(TRIM(ek.emp_role)) = 'RH' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone) OR
                                        (UPPER(TRIM(ek.emp_role)) = 'CM' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone AND a.ufc_NAME = um.UFC_NAME) OR
                                        NVL(UPPER(TRIM(a.selected_empid)), 'ZZZ') = :p_employee_code
                                      ) THEN 'VALID'
                                      ELSE 'ALL'
                                    END = 'VALID'
                                    AND a.remark_code IS NOT NULL
                                    AND A.ZONE_UTI = :p_zone
                                    AND a.region_name_uti = :p_region
                                  GROUP BY
                                    a.zone_uti,
                                    A.REGION_NAME_UTI,
                                    a.ufc_name,
                                    rmk.r_desc
                                )
                                PIVOT (
                                  SUM(FOLIO_COUNT)
                                  FOR REMARK_DESC IN (
    
                                'Customer shifted from given address & no further contact established' as REASON1
                                ,'Death reported for primary applicant' AS REASON2
                                ,'Death reported for joint applicants' AS REASON3
                                ,'Transmission reported by customer'  AS REASON4
                                ,'No address found' AS REASON5
                                ,'Account Frozen for legal case or law enforcement agencies'  AS REASON6
                                ,'Account Frozen as per SEBI Order or Other Security maket agencies'  AS REASON7
                                ,'Customer shifted from given address & contact established'  AS REASON8
                                ,'Visited address - customer not available' AS REASON9
                                ,'Customer will visit Branch/ MFD for updating the details' AS REASON10
                                ,'Visited/ Contacted customer - KYC & Documents (Bank Details, Nominee) submitted for remediation'  AS REASON11
                                ,'Death reported for any applicant (Transmission procedure completed in the folio)' AS REASON12
                                  )
                                )
                                ORDER BY ZONE";

            }
        }
        public static string GetGridReasonwiseRpt_Query_EMP
        {
            get
            {
                return @"SELECT *
                                FROM (
                                  SELECT
                                    a.zone_uti AS ZONE,
                                    a.region_name_uti AS REGION,
                                    a.ufc_name as UFC_NAME,
                                    rmk.r_desc AS REMARK_DESC,
                                    a.selected_empid AS EMPLOYEEID,
                                    ek1.name AS EMPNAME,
                                    COUNT(a.acno) AS FOLIO_COUNT
                                  FROM
                                    mistest.KYC_REMED_BASE_DAT a
                                    INNER JOIN mistest.EMPLOYEE_LIST_FOR_KYC EK ON :p_employee_code = EK.EMPLOYEECODE
                                   INNER JOIN mistest.EMPLOYEE_LIST_FOR_KYC EK1 ON a.selected_empid = EK1.EMPLOYEECODE
                                    LEFT OUTER JOIN (
                                      SELECT emp_id, emp_role, location, region_code, zone
                                      FROM dynamic.MIS_CUSER_LOGS_KYC
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto) cu ON ek.employeecode = cu.emp_id
                                    LEFT OUTER JOIN (SELECT ufc_code, ufc_name, region_code FROM mis0910.ufc_mast
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto) um ON cu.location = um.ufc_code
                                    LEFT OUTER JOIN (SELECT region_code, region_name FROM mis0910.region_mast
                                      WHERE '30-DEC-9999' BETWEEN valid_from AND valid_upto) rmz ON cu.region_code = rmz.region_code
                                    INNER JOIN mistest.kyc_remarks_master rmk ON rmk.slno = a.remark_code
                                  WHERE
                                    CASE
                                      WHEN ek.employeecode IN ('2824','1287','3010','3195','8500','4426','4517','2372','4471','4488','4594','8000','4237','4228','2179') THEN 'VALID'
                                      WHEN ((UPPER(TRIM(ek.emp_role)) = 'ZH' AND a.zone_uti = cu.zone) OR
                                        (UPPER(TRIM(ek.emp_role)) = 'RH' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone) OR
                                        (UPPER(TRIM(ek.emp_role)) = 'CM' AND a.region_name_uti = rmz.region_name AND a.zone_uti = cu.zone AND a.ufc_NAME = um.UFC_NAME) OR 
                                        NVL(UPPER(TRIM(a.selected_empid)), 'ZZZ') = :p_employee_code) THEN 'VALID'
                                      ELSE 'ALL' END = 'VALID'
                                    AND a.remark_code IS NOT NULL
                                    AND A.ZONE_UTI = :p_zone
                                    AND a.region_name_uti = :p_region
                                   and a.ufc_name= :p_ufcname
                                  GROUP BY
                                    a.zone_uti,
                                    A.REGION_NAME_UTI,
                                    a.ufc_name,
                                    rmk.r_desc,
                                    a.selected_empid,
                                    ek1.name)
                                PIVOT (SUM(FOLIO_COUNT) FOR REMARK_DESC IN (
                                    'Customer shifted from given address & no further contact established' as REASON1
                                ,'Death reported for primary applicant' AS REASON2
                                ,'Death reported for joint applicants' AS REASON3
                                ,'Transmission reported by customer'  AS REASON4
                                ,'No address found' AS REASON5
                                ,'Account Frozen for legal case or law enforcement agencies'  AS REASON6
                                ,'Account Frozen as per SEBI Order or Other Security maket agencies'  AS REASON7
                                ,'Customer shifted from given address & contact established'  AS REASON8
                                ,'Visited address - customer not available' AS REASON9
                                ,'Customer will visit Branch/ MFD for updating the details' AS REASON10
                                ,'Visited/ Contacted customer - KYC & Documents (Bank Details, Nominee) submitted for remediation'  AS REASON11
                                ,'Death reported for any applicant (Transmission procedure completed in the folio)' AS REASON12))
                                ORDER BY ZONE";

            }
        }


        // Reallocation Report Query
        public static string GetRealloactionGrid_Query
        {
            get
            {
                return @" SELECT
                            a.SRNO,a.ACNO,INVNAME, a.CONCAT_ADD ADDRESS_OF_FIRST_HOLDER, a.AUM_BRACKET AUM_BRACKET,
                            a.BANK_FLAG, a.KYCFLAG, a.AADHARSEEDINGFLAG, a.NOMINEEFLAG, a.agent ARN_CODE
                            FROM mistest.KYC_REMED_BASE_DAT a
                            LEFT OUTER JOIN mistest.EMPLOYEE_LIST_FOR_KYC  b  ON replace(upper(trim(a.UFC_NAME_DNQ)),' ','')=upper(trim(replace(replace(b.LOCATIONNAME_ORIG,'UFC',''),' ','')))
                            WHERE nvl(a.selected_rec,'N') in (select SELECTED_REC from mistest.KYC_REC_REALLOC_UFCCM)  and b.employeecode=:P_EMPCODE
                            and case when :P_SEARCH_TEXT is null then 'VALID'
                            when :P_SEARCH_TEXT is not null and REGEXP_LIKE(upper(trim(a.INVNAME||a.AGENT||a.CONCAT_ADD||a.ACNO)),:P_SEARCH_TEXT)
                            then 'VALID'  END='VALID'";
            }
        }
        public static string Update_Reallocation_diff_UFC
        {
            get
            {//Update to be add
                return @"Update mistest.KYC_REMED_BASE_DAT a set (a.selected_empid,a.selected_dt,a.selected_rec,a.SUPVERVISOR_ID,a.ufc_name,a.ufc_name_dnq,a.jurisdiction,a.region_name_uti,a.zone_uti) = (select null,null, 'N', null,b.ufc_name,b.ufc_name_dnq,b.jurisdiction,b.region_name_uti,b.zone_uti from  mistest.KYC_REMED_UFC_NAME b ";
            }
        }

        public static string Update_reallocation_same_ufc
        {
            get
            {
                return @"UPDATE mistest.KYC_REMED_BASE_DAT SET selected_rec='S',selected_dt=SYSDATE, ";
                //return @"UPDATE mistest.KYC_REMED_BASE_DAT SET selected_rec='S',selected_dt=SYSDATE ,selected_empid=:p_employee_code where SRNO=:p_SRNO";
            }
        }

        //Abridged Report
        public static string Get_UFC_list_empwise
        {
            get
            {
                //return @"SELECT um.UFC_NAME  FROM --EMPLOYEE_LIST_FOR_KYC EK
                //                (select ufc_code,ufc_name,region_code,zone from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um
                //                inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //                left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id
                //                left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code
                //                WHERE
                //                CASE
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488') then 'TRUE'
                //                --WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228') and cu.zone='ADMIN' then 'TRUE' --regexp_like(cu.zone,'WEST|NORTH|EAST|SOUTH|GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('4488','4594','2179') and cu.zone in ('WEST','NORTH','GULF') then 'TRUE' -- regexp_like(cu.zone,'WEST|NORTH') then 'TRUE'
                //                WHEN ek.employeecode in ('8000','4237') then 'SOUTH'
                //                WHEN ek.employeecode in ('2372','4471') then 'EAST'
                //                WHEN cu.emp_role='ZH' then cu.zone
                //                WHEN cu.emp_role='RH' then cu.zone||cu.region_code
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                else null end =
                //                CASE
                //                --WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228') and um.zone in ('WEST','NORTH','EAST','SOUTH','GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488') then 'TRUE'
                //                ---WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228')  and cu.zone='ADMIN' then 'TRUE'  ---and regexp_like(um.zone,'WEST|NORTH|EAST|SOUTH|GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('4488','4594','2179') and  um.zone in ('WEST','NORTH','GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('8000','4237') then um.zone
                //                WHEN ek.employeecode in ('2372','4471') then um.zone
                //                WHEN cu.emp_role='ZH' then um.zone
                //                WHEN cu.emp_role='RH' then um.zone||um.region_code
                //                WHEN cu.emp_role='CM' then um.zone||um.region_code||um.ufc_code
                //                else null end order by um.UFC_NAME";

                //return @"SELECT um.UFC_NAME  FROM --EMPLOYEE_LIST_FOR_KYC EK
                //                (select ufc_code,ufc_name,region_code,zone from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um
                //                inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //                left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id
                //                left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code
                //                WHERE
                //                CASE
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488') then 'TRUE'
                //                --WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228') and cu.zone='ADMIN' then 'TRUE' --regexp_like(cu.zone,'WEST|NORTH|EAST|SOUTH|GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('4488','4594','2179') and cu.zone in ('WEST','NORTH','GULF') then 'TRUE' -- regexp_like(cu.zone,'WEST|NORTH') then 'TRUE'
                //                WHEN ek.employeecode in ('8000','4237') then 'SOUTH'
                //                WHEN ek.employeecode in ('2372','4471','1073') then 'EAST'
                //                WHEN cu.emp_role='ZH' then cu.zone
                //                WHEN cu.emp_role='RH' then cu.zone||cu.region_code
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                else null end =
                //                CASE
                //                --WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228') and um.zone in ('WEST','NORTH','EAST','SOUTH','GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488') then 'TRUE'
                //                ---WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228')  and cu.zone='ADMIN' then 'TRUE'  ---and regexp_like(um.zone,'WEST|NORTH|EAST|SOUTH|GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('4488','4594','2179') and  um.zone in ('WEST','NORTH','GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('8000','4237') then um.zone
                //                WHEN ek.employeecode in ('2372','4471','1073') then um.zone
                //                WHEN cu.emp_role='ZH' then um.zone
                //                WHEN cu.emp_role='RH' then um.zone||um.region_code
                //                WHEN cu.emp_role='CM' then um.zone||um.region_code||um.ufc_code
                //                else null end order by um.UFC_NAME";

                //return @"SELECT um.UFC_NAME,um.zone,cu.zone zzz FROM --EMPLOYEE_LIST_FOR_KYC EK
                //                (select ufc_code,ufc_name,region_code,zone from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um
                //                inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //                left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id
                //                left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code
                //                WHERE CASE
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','2372','4471','1073','4488','4594','2179','8000','4237') then 'TRUE'
                //                WHEN cu.emp_role='ZH' then cu.zone
                //                WHEN cu.emp_role='RH' then cu.zone||cu.region_code
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                else null end = CASE
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488') then 'TRUE'
                //                --WHEN ek.employeecode in ('4488','4594','2179') and  um.zone in ('WEST','GULF') then 'TRUE'
                //                WHEN ek.employeecode in ('4488','4594','2179') and  instr('#WEST#NORTH#GULF#CORP#',um.zone)<>0 then 'TRUE'
                //                WHEN ek.employeecode in ('8000','4237') and  instr('#SOUTH#',um.zone)<>0 then 'TRUE'
                //                WHEN ek.employeecode in ('2372','4471','1073') and  instr('#EAST#NORTH#CORP#',um.zone)<>0 then 'TRUE'
                //                WHEN cu.emp_role='ZH' then um.zone
                //                WHEN cu.emp_role='RH' then um.zone||um.region_code
                //                WHEN cu.emp_role='CM' then um.zone||um.region_code||um.ufc_code
                //                else null end order by um.UFC_NAME";

                //return @"SELECT um.UFC_NAME,um.zone,cu.zone zzz FROM --EMPLOYEE_LIST_FOR_KYC EK
                //                (select ufc_code,ufc_name,region_code,zone from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um
                //                inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                //                left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.MIS_CUSER_LOGS_KYC where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id
                //                left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code
                //                WHERE CASE
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','2372','4471','1073','4488','4594','2179','8000','4237') then 'TRUE'
                //                WHEN cu.emp_role='ZH' then cu.zone
                //                WHEN cu.emp_role='RH' then cu.zone||cu.region_code
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                //                else null end = CASE
                //                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488') then 'TRUE'
                //                WHEN ek.employeecode in ('4488','4594','2179') and  instr('#WEST#NORTH#GULF#CORP#',um.zone)<>0 then 'TRUE'
                //                WHEN ek.employeecode in ('8000','4237') and  instr('#SOUTH#',um.zone)<>0 then 'TRUE'
                //                WHEN ek.employeecode in ('2372','4471','1073') and  instr('#EAST#NORTH#CORP#WEST#GULF#',um.zone)<>0 then 'TRUE'
                //                WHEN cu.emp_role='ZH' then um.zone
                //                WHEN cu.emp_role='RH' then um.zone||um.region_code
                //                WHEN cu.emp_role='CM' then um.zone||um.region_code||um.ufc_code
                //                else null end order by um.UFC_NAME";

                return @"SELECT um.UFC_NAME  FROM --EMPLOYEE_LIST_FOR_KYC EK
                                (select ufc_code,ufc_name,region_code,zone from mis0910.ufc_mast where '30-DEC-9999' between valid_from and valid_upto) um
                                inner join EMPLOYEE_LIST_FOR_KYC EK on :p_employee_code = EK.EMPLOYEECODE
                                left outer join (select emp_id,emp_role,location,region_code,zone from dynamic.mis_cuser_logs_kyc where '30-DEC-9999' between valid_from and valid_upto) cu on ek.employeecode=cu.emp_id
                                left outer join (select region_code,region_name from mis0910.region_mast where '30-DEC-9999' between valid_from and valid_upto) rmz on cu.region_code=rmz.region_code
                                WHERE
                                CASE
                                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488','2372') then 'TRUE'
                                WHEN ek.employeecode in ('4488','4594','2179') and cu.zone in ('WEST','NORTH','GULF') then 'TRUE' -- regexp_like(cu.zone,'WEST|NORTH') then 'TRUE'
                                WHEN ek.employeecode in ('8000','4237') then 'SOUTH'
                                WHEN ek.employeecode in ('4471','1073') then 'EAST'
                                WHEN cu.emp_role='ZH' then cu.zone
                                WHEN cu.emp_role='RH' then cu.zone||cu.region_code
                                WHEN cu.emp_role='CM' then cu.zone||cu.region_code||cu.location
                                WHEN cu.emp_role='RM' then cu.zone||cu.region_code||cu.location
                                else null end =
                                CASE
                                WHEN ek.employeecode in ('2824','1287','3010','3195','8500','4426','4517','4228','4488','2372') then 'TRUE'
                                WHEN ek.employeecode in ('4488','4594','2179') and  um.zone in ('WEST','NORTH','GULF') then 'TRUE'
                                WHEN ek.employeecode in ('8000','4237') then um.zone
                                WHEN ek.employeecode in ('4471','1073') then um.zone
                                WHEN cu.emp_role='ZH' then um.zone
                                WHEN cu.emp_role='RH' then um.zone||um.region_code
                                WHEN cu.emp_role='CM' then um.zone||um.region_code||um.ufc_code
                                else null end order by um.UFC_NAME";
            }

        }

        //Abridged Report
        public static string Emp_list_ufcwise
        {
            get
            {
                //return @"select EMPLOYEECODE Code, NAME Name FROM mistest.EMPLOYEE_LIST_FOR_KYC where replace(replace(upper(Trim(locationname_orig)),'UFC',''),' ','')=upper(Trim(:p_ufc_name))";
                return @"select EMPLOYEECODE Code, NAME Name FROM
                        mistest.EMPLOYEE_LIST_FOR_KYC where replace(replace(upper(Trim(locationname_orig)),'UFC',''),' ','')= replace(replace(upper(Trim(:p_ufc_name)),'UFC',''),' ','')";
            }
        }

        public static string PanSearchQuery
        {
            get
            {
                return @"select * from mistest.ALL_KYC_PAN_SEARCH where PANNO = :P_PanNo";
            }
        }
        public static string PanSearchAddLog
        {
            get
            {
                return @"insert into mistest.ALL_KYC_PAN_SEARCH_LOG (EMP_ID, PANNO, DT_OF_SEARCH) VALUES(:p_employee_code, :p_panno, :p_DateOfSearch)";
            }
        }


        //Contest Leadership Report
        public static string GetGridContestLeadershipRpt_Query
        {
            get
            {
                return @"select * from mistest.KYC_CNTST_LEADERBOARD";
            }
        }

    }
}